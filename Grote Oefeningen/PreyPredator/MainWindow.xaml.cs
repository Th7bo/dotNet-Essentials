using System.Linq;
using System.Windows;
using PreyPredator.Contracts;

namespace PreyPredator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAnimalWorld _insectWorld;
        public MainWindow()
        {
            InitializeComponent();
            _insectWorld = new AnimalWorld(worldCanvas);
            for (int i = 0; i < 100; i++)
                _insectWorld.AddAnimal(new Louse());
            for (int i = 0; i < 10; i++)
                _insectWorld.AddAnimal(new LadyBug());
            DisplayStatistics();
        }

        private void NextRoundButton_Click(object sender, RoutedEventArgs e)
        {
            _insectWorld.ProcessRound();
            DisplayStatistics();
        }

        private void DisplayStatistics()
        {
            roundLabel.Content = _insectWorld.CurrentRoundNumber.ToString();
            ladybugLabel.Content = _insectWorld.AllAnimals.Count(a => a is LadyBug).ToString();
            louseLabel.Content = _insectWorld.AllAnimals.Count(a => a is Louse).ToString();
        }
    }
}