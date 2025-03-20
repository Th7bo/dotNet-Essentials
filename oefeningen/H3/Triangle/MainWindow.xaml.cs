using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Triangle
{
    // opgave: Hoe kan je onderstaande code leesbaarder maken?
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
            // draw a triangle
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);
            DrawTriangle(20, 80, 50, brush, paperCanvas);
        }

        private void DrawTriangle(int startX, int startY, int size, SolidColorBrush brush, Canvas canvas)
        {
            DrawLine(startX, startY, startX + size, startY - size, brush, paperCanvas);
            DrawLine(startX + size, startY - size, startX + 2 * size, startY, brush, paperCanvas);
            DrawLine(startX + 2 * size, startY, startX, startY, brush, paperCanvas);
        }

        private void DrawLine(int startX, int startY, int endX, int endY, SolidColorBrush brush, Canvas canvas)
        {
            Line line = new()
            {
                X1 = startX,
                X2 = endX,
                Y1 = startY,
                Y2 = endY,
                Stroke = brush
            };
            canvas.Children.Add(line);
        }
    }
}
