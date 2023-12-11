using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;
using KrakowiakKozlowski.GamesCatalog.CORE;

namespace KrakowiakKozlows.GamesCatalog.UI.ViewModels
{
    public class GameViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IGame game;

        public GameViewModel(IGame game) {
            this.game = game;
        }

        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int GameId
        {
            get => game.Id;
            set
            {
                game.Id = value;
                RaisePropertyChanged(nameof(GameId));
            }
        }

        public string GameProducerName
        {
            get => game.Producer.Name;
            set
            {
                game.Producer.Name = value;
                RaisePropertyChanged(nameof(GameProducerName));
            }
        }

        public string GameProducerCountry
        {
            get => game.Producer.Country;
            set
            {
                game.Producer.Country = value;
                RaisePropertyChanged(nameof(GameProducerCountry));
            }
        }
        public string GameTitle
        {
            get => game.Title;
            set
            {
                game.Title = value;
                RaisePropertyChanged(nameof(GameTitle));
            }
        }

        public int GameReleaseYear
        {
            get => game.ReleaseYear;
            set
            {
                game.ReleaseYear = value;
                RaisePropertyChanged(nameof(GameReleaseYear));
            }
        }

        public int GameScore
        {
            get => game.Score;
            set
            {
                game.Score = value;
                RaisePropertyChanged(nameof(GameScore));
            }
        }

        public GameGenre GameGenre
        {
            get => game.Genre;
            set
            {
                game.Genre = value;
                RaisePropertyChanged(nameof(GameGenre));
            }
        }
    }
}
