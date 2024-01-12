using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KrakowiakKozlowski.Games.CORE;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.UI.WPF
{
    /// <summary>
    /// Interaction logic for AddGame.xaml
    /// </summary>
    public partial class AddGame : Window
    {
        private IEnumerable<IProducer> _producers;
        public AddGame(IEnumerable<IProducer> producers)
        {
            _producers = producers;
            InitializeComponent();

            foreach (var producer in producers)
            {
                gameProducer.Items.Add(producer);
            }

            if (gameProducer.Items.Count > 0)
            {
                gameProducer.SelectedIndex = 0;
            }

            gameGenre.ItemsSource = Enum.GetNames(typeof(GameGenre));

            if (gameGenre.Items.Count > 0)
            {
                gameGenre.SelectedIndex = 0;
            }

        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gameTitle.Text) || string.IsNullOrWhiteSpace(gameProducer.Text) ||
                string.IsNullOrWhiteSpace(gameScore.Text) || string.IsNullOrWhiteSpace(gameReleaseYear.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }   
            DialogResult = true;
        }


        public string GameTitle
        {
            get { return gameTitle.Text; }
        }

        public GameGenre GameGenre
        {
            get
            {
                return (GameGenre)gameGenre.SelectedIndex;
            }
        }

        public int GameScore
        {
            get
            {
                return int.Parse(gameScore.Text);

            }
        }

        public int GameReleaseYear
        {
            get
            {
                return int.Parse(gameReleaseYear.Text);
            }
        }

        public int GameProducerId
        {
            get
            {
                return (int)(_producers.ElementAtOrDefault(gameProducer.SelectedIndex)?.Id);
            }
        }

        private void validate(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
