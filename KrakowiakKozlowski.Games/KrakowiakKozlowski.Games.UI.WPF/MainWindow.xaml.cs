using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.UI.WPF
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<IProducer> producers = new ObservableCollection<IProducer>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeProducers();
            ProducerComboBox.ItemsSource = producers;
            Tc.SelectionChanged += Tabcontrol_SelectionChanged;
        }

        private void InitializeProducers()
        {
            foreach (var producerViewModel in Glvm.Producers)
            {
                producers.Add(producerViewModel.Producer);
            }
        }

        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                UpdateProducers();
            }
        }

        private void UpdateProducers()
        {
            Glvm.GetAllGames();
            producers.Clear();
            InitializeProducers();
        }
    }
}
