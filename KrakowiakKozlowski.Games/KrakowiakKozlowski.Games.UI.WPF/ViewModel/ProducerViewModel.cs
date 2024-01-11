using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.UI.WPF.ViewModel
{
    public class ProducerViewModel : ViewModelBase
    {
        private IProducer _producer;
        public IProducer Producer
        {
            get => _producer;
        }
        public ProducerViewModel(IProducer producer)
        {
            _producer = producer;
        }

        [Required]
        public int Id
        {
            get => _producer.Id;
        }

        [Required]
        public string Name
        {
            get => _producer.Name;
            set
            {
                _producer.Name = value;
                OnPropertyChanged("Name");
            }
        }
        [Required]
        public string Country
        {
            get => _producer.Country;
            set
            {
                _producer.Country = value;
                OnPropertyChanged("Country");
            }
        }
    }
}
