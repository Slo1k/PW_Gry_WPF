using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.UI.ViewModels
{
    public class ProducerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IProducer producer;

        public ProducerViewModel(IProducer producer)
        {
            this.producer = producer;
        }

        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int ProducerID
        {
            get => producer.Id;
            set
            {
                producer.Id = value;
                RaisePropertyChanged(nameof(ProducerID));
            }
        }

        public string ProducerName
        {
            get => producer.Name;
            set
            {
                producer.Name = value;
                RaisePropertyChanged(nameof(ProducerName));
            }
        }

        public string ProducerCountry
        {
            get => producer.Country;
            set
            {
                producer.Country = value;
                RaisePropertyChanged(nameof(ProducerCountry));
            }

        }
    }
}
