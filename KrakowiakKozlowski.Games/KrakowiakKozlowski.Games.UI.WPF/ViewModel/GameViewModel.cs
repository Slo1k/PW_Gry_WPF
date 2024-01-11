using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.INTERFACES;
using KrakowiakKozlowski.Games.CORE;

namespace KrakowiakKozlowski.Games.UI.WPF.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private IGame _game;
        public IGame Game
        {
            get => _game;
        }
        public GameViewModel(IGame game)
        {
            _game = game;
        }

        [Required]
        public int Id
        {
            get => _game.Id;
        }


        [Required]
        public string Title
		{
            get => _game.Title;
            set
            {
                _game.Title = value;
                OnPropertyChanged("Title");
            }
        }
        [Required]
        public IProducer Producer
        {
            get => _game.Producer;
            set
            {
                _game.Producer = value;
                OnPropertyChanged("Producer");
            }
        }
        [Required]
        public int ReleaseYear
        {
            get => _game.ReleaseYear;
            set
            {
                _game.ReleaseYear = value;
                OnPropertyChanged("ReleaseYear");
            }
        }
        [Required]
        public int Score
        {
            get => _game.Score;
            set
            {
                _game.Score = value;
                OnPropertyChanged("Score");
            }
        }

        [Required]
        public GameGenre Genre
        {
            get => _game.Genre;
            set
            {
                _game.Genre = value;
                OnPropertyChanged("Genre");
            }
        }
    }
}
