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
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.UI
{
    /// <summary>
    /// Logika interakcji dla klasy ProducerDialog.xaml
    /// </summary>
    public partial class ProducerDialog : Window
    {
        public ProducerDialog()
        {
            InitializeComponent();
        }

        public ProducerDialog(IProducer producer)
        {
            InitializeComponent();
            producerName.Text = producer.Name;
            producerCountry.Text = producer.Country;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
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
            get { return producerCountry.Text; }
        }
    }
}
