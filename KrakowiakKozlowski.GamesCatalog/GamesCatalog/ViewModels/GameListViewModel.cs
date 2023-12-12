using KrakowiakKozlowski.GamesCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakowiakKozlowski.GamesCatalog.UI.ViewModels
{
    public class GameListViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<GameViewModel> Games { get; set; } = new ObservableCollection<GameViewModel>();

        public void RefreshList(IEnumerable<IGame> games)
        {
            Games.Clear();

            foreach (var game in games)
            {
                Games.Add(new GameViewModel(game));
            }

            RaisePropertyChanged(nameof(Games));
        }

        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
