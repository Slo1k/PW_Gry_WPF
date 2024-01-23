using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using KrakowiakKozlowski.Games.BL;
using KrakowiakKozlowski.Games.INTERFACES;
using Microsoft.EntityFrameworkCore;

namespace KrakowiakKozlowski.Games.UI.WPF.ViewModel
{
    public class GameListViewModel : ViewModelBase
    {
        public ObservableCollection<GameViewModel> Games { get; set; } = new ObservableCollection<GameViewModel>();
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();
        private ListCollectionView _view;
        private RelayCommand _filterDataCommand;
        public RelayCommand FilterDataCommand
        {
            get => _filterDataCommand;
        }
        public string FilterValue { get; set; }
        private RelayCommand _filterTypeCommand;
        public RelayCommand FilterTypeCommand
        {
            get => _filterTypeCommand;
        }
        public string FilterTypeValue { get; set; }

        private BLC dataAccess = null;
        public GameListViewModel()
        {
            try
            {
                dataAccess = BLC.Instance;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Creating DAO Failed!");
            }
            GetAllGames();
            OnPropertyChanged("Games");
            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Games);
            _filterDataCommand = new RelayCommand(param => this.FilterData());
            _filterTypeCommand = new RelayCommand(param => this.FilterType());
            _addNewGameCommand = new RelayCommand(param => this.AddNewGame());
            _saveGameCommand = new RelayCommand(param => this.SaveGame(), param => this.CanSaveGame());
            _deleteGameCommand = new RelayCommand(param => this.DeleteGame(), param => this.CanDeleteGame());
            EditedGame = null;
            SelectedGame = EditedGame;
        }
        public void GetAllGames()
        {
            Games.Clear();
            Producers.Clear();
			List<IGame> games = dataAccess.DAO.GetAllGames().ToList();
            List<IProducer> producers = dataAccess.DAO.GetAllProducers().ToList();
            foreach (var game in games)
            {
                Games.Add(new GameViewModel(game));
            }
            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }
        private void FilterData()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                _view.Filter = null;
            }
            else
            {
                _view.Filter = (g) => ((GameViewModel)g).Title.Contains(FilterValue);
            }
        }
        private void FilterType()
        {
            if (string.IsNullOrEmpty(FilterTypeValue))
            {
                _view.Filter = null;
            }
            else
            {
                _view.Filter = (g) => ((GameViewModel)g).Genre.ToString().Contains(FilterTypeValue);
            }
        }
        private GameViewModel _editedGame;
        public GameViewModel EditedGame
        {
            get => _editedGame;
            set
            {
                _editedGame = value;
                OnPropertyChanged(nameof(EditedGame));
            }
        }
        private GameViewModel _selectedGame;
        public GameViewModel SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                EditedGame = value;
                OnPropertyChanged(nameof(SelectedGame));
            }
        }

        private RelayCommand _saveGameCommand;
        public RelayCommand SaveGameCommand
        {
            get => _saveGameCommand;
        }
        private void SaveGame()
        {
            if (!Games.Contains(EditedGame))
            {
                dataAccess.UpdateGame(EditedGame.Game);
                SelectedGame = null;
                EditedGame = null;
            }
        }
        private bool CanSaveGame()
        {
            if (EditedGame != null && !EditedGame.HasErrors)
            {
                return true;
            }
            return false;
        }
        private RelayCommand _addNewGameCommand;
        public RelayCommand AddNewGameCommand
        {
            get => _addNewGameCommand;
        }
        private void AddNewGame()
        {
            var producers = dataAccess.DAO.GetAllProducers();

            {
                int maxGameId;
                try
                {
                    maxGameId = dataAccess.DAO.GetAllGames().Max(x => x.Id) + 1;
                }
                catch
                {
                    maxGameId = 1;
                }

                AddGame addGameDialog = new AddGame(producers);
                addGameDialog.Owner = Application.Current.MainWindow;

                if (addGameDialog.ShowDialog() == true)
                {
                    var newGame = dataAccess.DAO.AddNewGame(maxGameId, addGameDialog.GameTitle, addGameDialog.GameReleaseYear, addGameDialog.GameGenre, addGameDialog.GameScore, addGameDialog.GameProducerId);
                    Games.Add(new GameViewModel(newGame));
                }
            }
        }

        private RelayCommand _deleteGameCommand;
        public RelayCommand DeleteGameCommand
        {
            get => _deleteGameCommand;
        }
        private void DeleteGame()
        {

            if (Games.Contains(SelectedGame))
            {

                dataAccess.RemoveGame(SelectedGame.Id);
                Games.Remove(SelectedGame);
                SelectedGame = null;
                EditedGame = null;
            }
            SelectedGame = null;
            EditedGame = null;
        }
        private bool CanDeleteGame()
        {
            if (EditedGame != null && !EditedGame.HasErrors)
            {
                return true;
            }
            return false;
        }
    }
}

