using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oef4_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void computeButton_Click(object sender, RoutedEventArgs e)
        {
            double radius = Convert.ToDouble(radiusTextBox.Text);

            double circumference = 2 * Math.PI * radius;
            double area = Math.PI * Math.Pow(radius, 2);
            double volume = (4 * Math.PI / 3) * Math.Pow(radius, 3);

            circumferenceLabel.Content = circumference;
            areaLabel.Content = area;
            volumeLabel.Content = volume;
        }
    }
}