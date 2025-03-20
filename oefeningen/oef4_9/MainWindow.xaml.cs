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

namespace oef4_9
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
            int cents = Convert.ToInt32(amountTextBox.Text) - Convert.ToInt32(costTextBox.Text);
            string output = "";
            int[] centen = { 200, 100, 50, 20, 10, 5, 2, 1 };
            foreach (int i in centen) {
                int amount = cents / i;
                if (amount <= 0) continue;
                string format = (i >= 100) ? i / 100 + " euro" : i + " cent";
                output += "Number of " + format + " coins is " + amount.ToString() + "\n";
                cents %= i;
            }
            MessageBox.Show(output);
        }
    }
}