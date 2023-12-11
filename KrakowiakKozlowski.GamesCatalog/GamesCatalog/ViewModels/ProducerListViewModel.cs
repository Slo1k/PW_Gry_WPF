using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.UI.ViewModels
{
    public class ProducerListViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();

        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void RefreshList(IEnumerable<IProducer> producers)
        {
            Producers.Clear();

            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }

            RaisePropertyChanged(nameof(Producers));
        }
    }
}
