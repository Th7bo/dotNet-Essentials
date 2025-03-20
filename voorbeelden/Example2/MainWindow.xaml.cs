using System.Diagnostics;
using System.Windows;

namespace Example2
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            outputLabel.Content = "Hello 1TINa";
            Debug.WriteLine("Dit is een tekst");
        }

        private void messageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello 1TINa");
        }
    }
}