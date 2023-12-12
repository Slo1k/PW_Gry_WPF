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
using KrakowiakKozlowski.GamesCatalog.CORE;

namespace KrakowiakKozlowski.GamesCatalog.UI
{
    /// <summary>
    /// Logika interakcji dla klasy GameDialog.xaml
    /// </summary>
    public partial class GameDialog : Window
    {
        public GameDialog(IEnumerable<string> producers)
        {
            InitializeComponent();
            producers.ToList().ForEach(f => producer.Items.Add(f));
            if (producer.Items.Count > 0)
            {
                producer.SelectedIndex = 0;
            }
            gameGenre.ItemsSource = Enum.GetNames(typeof(GameGenre));
            if (gameGenre.Items.Count > 0)
            {
                gameGenre.SelectedIndex = 0;
            }
        }

        public GameDialog(IEnumerable<string> producers, INTERFACES.IGame game)
        {
            InitializeComponent();
            producers.ToList().ForEach(f => producer.Items.Add(f));

            for (int i = 0; i < producers.Count(); i++)
            {
                if (producers.ElementAt(i).Equals(game.Producer.Name))
                {
                    producer.SelectedIndex = i;
                    break;
                }
            }
            gameGenre.ItemsSource = Enum.GetNames(typeof(GameGenre));
            if (gameGenre.Items.Count > 0)
            {
                gameGenre.SelectedIndex = 0;
            }
            gameTitle.Text = game.Title;
            gameGenre.SelectedIndex = (int)game.Genre;
            gameScore.Text = game.ReleaseYear.ToString();
            gameReleaseYear.Text = game.ReleaseYear.ToString();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            gameTitle.SelectAll();
            gameTitle.Focus();
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

        public string Producer
        {
            get
            {
                return producer.Text;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
