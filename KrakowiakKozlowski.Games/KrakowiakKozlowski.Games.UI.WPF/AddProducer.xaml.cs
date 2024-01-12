using System;
using System.Collections.Generic;
using System.Linq;
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
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.UI.WPF
{
    /// <summary>
    /// Interaction logic for AddProducer.xaml
    /// </summary>
    public partial class AddProducer : Window
    {
        public AddProducer()
        {
            InitializeComponent();
        }

        public AddProducer(IProducer producer)
        {
            InitializeComponent();
            producerName.Text = producer.Name;
            producerCountry.Text = producer.Country;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(producerName.Text) || string.IsNullOrWhiteSpace(producerCountry.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            producerName.SelectAll();
            producerName.Focus();
        }

        public string ProducerName
        {
            get { return producerName.Text; }
        }

        public string ProducerCountry
        {
            get { return producerCountry.Text;  }
        }
    }
}
