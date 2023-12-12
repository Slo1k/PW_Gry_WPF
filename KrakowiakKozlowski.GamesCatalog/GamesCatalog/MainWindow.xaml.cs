using KrakowiakKozlowski.GamesCatalog.CORE;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL = KrakowiakKozlowski.GamesCatalog.BLC;
using KrakowiakKozlowski.GamesCatalog.DAOMock;
using System.Data.Common;

namespace KrakowiakKozlowski.GamesCatalog.UI
{
    public partial class MainWindow : Window
    {
        public ViewModels.ProducerListViewModel ProducerLVM { get; } = new ViewModels.ProducerListViewModel();
        private ViewModels.ProducerViewModel selectedProducer = null;

        public ViewModels.GameListViewModel GameLVM { get; } = new ViewModels.GameListViewModel();
        private ViewModels.GameViewModel selectedGame = null;

        private readonly BL.BLC blc;

        private string selectedDataSource = "KrakowiakKozlowski.GamesCatalog.DAOMock.dll";

        public MainWindow()
        {
            blc = new BL.BLC(selectedDataSource);

            ProducerLVM.RefreshList(blc.GetAllProducers().Distinct());;
            GameLVM.RefreshList(blc.GetAllGames());

            InitializeComponent();
        }
        private void ApplyNewDataSource(object sender, RoutedEventArgs e)
        {
            try
            {
                blc.LoadDatasource(datasource.Text);
                ProducerLVM.RefreshList(blc.GetAllProducers());
                GameLVM.RefreshList(blc.GetAllGames());
                selectedDataSource = datasource.Text;
            }
            catch
            {
                MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                blc.LoadDatasource(selectedDataSource);
            }
        }

        #region Filters
        private void ApplyGameSearch(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = searchTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                GameLVM.RefreshList(blc.GetAllGames());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = gameSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                GameLVM.RefreshList(blc.GetAllGames());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {

                case "Name":
                    FilterByName(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }

            if (GameList.Items.Count > 0)
            {
                GameList.SelectedItem = GameList.Items[0];

            }
        }

        private void ApplyProducerSearch(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = producersearchTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                ProducerLVM.RefreshList(blc.GetAllProducers());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = producerSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                ProducerLVM.RefreshList(blc.GetAllProducers());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {
                case "Name":
                    FilterProducerByName(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }

            if (ProducerList.Items.Count > 0)
            {
                ProducerList.SelectedItem = ProducerList.Items[0];

            }
        }

        private void ProducerApplyFilter(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = producerfilterTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                ProducerLVM.RefreshList(blc.GetAllProducers());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = producerfilterValueTextBox.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                ProducerLVM.RefreshList(blc.GetAllProducers());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {

                case "Country":
                    FilterProducerByCountry(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = filterTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                GameLVM.RefreshList(blc.GetAllGames());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = filterValueTextBox.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                GameLVM.RefreshList(blc.GetAllGames());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {
                case "Genre":
                    FilterByGenre(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }
        }


        private void FilterByName(string gameName)
        {
            if (gameName == "")
            {
                GameLVM.RefreshList(blc.GetAllGames());
            }
            else
            {
                GameLVM.RefreshList(blc.GetGamesByTitle(gameName));
            }
        }
        private void FilterByGenre(string gameGenre)
        {
            if (gameGenre == "")
            {
                GameLVM.RefreshList(blc.GetAllGames());
            }
            else
            {
                GameGenre genre;
                Enum.TryParse<GameGenre>(gameGenre, out genre);
                GameLVM.RefreshList(blc.GetGamesByGenre(genre));
            }
        }

        private void FilterProducerByName (string producerName)
        {
            if (producerName == "")
            {
                ProducerLVM.RefreshList(blc.GetAllProducers());
            }
            else
            {
                ProducerLVM.RefreshList(blc.GetProducerByName(producerName));
            }
        }

        private void FilterProducerByCountry(string producerCountry)
        {
            if (producerCountry == "")
            {
                ProducerLVM.RefreshList(blc.GetAllProducers());
            }
            else
            {
                ProducerLVM.RefreshList(blc.GetProducerByCountry(producerCountry));
            }
        }
        #endregion

        #region Game operations

        private void GameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedGame((ViewModels.GameViewModel)e.AddedItems[0]);
            }
        }

        private void EditGame(object sender, RoutedEventArgs e)
        {
            if (selectedGame != null)
            {

                GameDialog gameEditDialog = new(
                    blc.GetAllGamesNames(),
                    blc.GetGame(selectedGame.GameId).First()
                );

                if (gameEditDialog.ShowDialog() == true)
                {
                    blc.CreateOrUpdateGame(new GameDAOMock()
                    {
                        Id = selectedGame.GameId,
                        Title = gameEditDialog.GameTitle,
                        Genre = gameEditDialog.GameGenre,
                        Producer = blc.GetProducerByName(gameEditDialog.Producer).First(),
                        ReleaseYear = gameEditDialog.GameReleaseYear,
                        Score = gameEditDialog.GameScore
                    });

                    GameLVM.RefreshList(blc.GetAllGames());
                    ChangeSelectedGame(null);
                }
            }
            else
            {
                MessageBox.Show("Game is not selected!");
            }
        }

        private void RemoveGame(object sender, RoutedEventArgs e)
        {
            if (selectedGame != null)
            {
                blc.RemoveGame(selectedGame.GameId);
                GameLVM.RefreshList(blc.GetAllGames());
                selectedGame = null;
            }
            else
            {
                MessageBox.Show("Game is not selected!");
            }
        }

        private void AddGame(object sender, RoutedEventArgs e)
        {
            var allGamesNames = blc.GetAllGamesNames();
            GameDialog gameInputDialog = new(allGamesNames);

            if (gameInputDialog.ShowDialog() == true)
            {
                DAOMock.GameDAOMock game;
                try
                {
                    game = new GameDAOMock()
                    {
                        Title = gameInputDialog.GameTitle,
                        Genre = gameInputDialog.GameGenre,
                        ReleaseYear = gameInputDialog.GameReleaseYear,
                        Score = gameInputDialog.GameScore,
                        Producer = blc.GetProducerByName(gameInputDialog.Producer).First(),
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                blc.CreateOrUpdateGame(game);
                GameLVM.RefreshList(blc.GetAllGames());
            }
        }

        private void ChangeSelectedGame(ViewModels.GameViewModel gameViewModel)
        {
            selectedGame = gameViewModel;
            DataContext = selectedGame;
        }

        #endregion

        #region Airbase operations
        private void ChangeSelectedProducer(ViewModels.ProducerViewModel producerViewModel)
        {
            selectedProducer = producerViewModel;
            DataContext = selectedProducer;
        }

        private void ProducerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedProducer((ViewModels.ProducerViewModel)e.AddedItems[0]);
            }
        }



        private void AddProducer(object sender, RoutedEventArgs e)
        {
            var allProduersNames = blc.GetAllProducersNames();
            ProducerDialog producerDialog = new();

            if (producerDialog.ShowDialog() == true)
            {
                DAOMock.ProducerDAOMock producer;
                try
                {
                    producer = new ProducerDAOMock()
                    {
                        Name = producerDialog.ProducerName,
                        Country = producerDialog.ProducerCountry
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                blc.CreateOrUpdateProducer(producer);
                ProducerLVM.RefreshList(blc.GetAllProducers());
            }
        }

        private void RemoveProducer(object sender, RoutedEventArgs e)
        {
            if (selectedProducer != null)
            {
                blc.RemoveProducer(selectedProducer.ProducerID);
                ProducerLVM.RefreshList(blc.GetAllProducers());
                selectedProducer = null;
            }
            else
            {
                MessageBox.Show("Airbase is not selected!");
            }
        }

        private void EditProducer(object sender, RoutedEventArgs e)
        {
            if (selectedProducer != null)
            {
                ProducerDialog producerDialog = new(
                    blc.GetProducer(selectedProducer.ProducerID).First()
                );

                if (producerDialog.ShowDialog() == true)
                {
                    blc.CreateOrUpdateProducer(new ProducerDAOMock()
                    {
                        Id = selectedProducer.ProducerID,
                        Name = producerDialog.ProducerName,
                        Country = producerDialog.ProducerCountry
                    });

                    ProducerLVM.RefreshList(blc.GetAllProducers());
                    ChangeSelectedProducer(null);
                }
            }
            else
            {
                MessageBox.Show("Producer is not selected!");
            }
        }
        #endregion
    }
}
