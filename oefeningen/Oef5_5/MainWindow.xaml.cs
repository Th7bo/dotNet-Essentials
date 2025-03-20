using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Oef5_5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            Draw(paperCanvas, blackBrush, 30, 30, 80, 80);
            Draw(paperCanvas, blackBrush, 30 + 1 * 100, 30, 80, 80);
            Draw(paperCanvas, blackBrush, 30 + 2 * 100, 30, 80, 80);
            Draw(paperCanvas, blackBrush, 30 + 3 * 100, 30, 80, 80);
        }

        private void Draw(Canvas drawingArea,
                                  SolidColorBrush brushToUse,
                                  double topX,
                                  double topY,
                                  double width,
                                  double height)
        {
            DrawTriangle(drawingArea, brushToUse, topX, topY, width, height);
            DrawRectangle(drawingArea, brushToUse, topX, topY + height, width);
        }

        private void DrawTriangle(Canvas drawingArea,
                                  SolidColorBrush brushToUse,
                                  double topX,
                                  double topY,
                                  double width,
                                  double height)
        {
            Polygon myPolygon = new()
            {
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            };
            Point Point1 = new(topX, topY);
            Point Point2 = new(topX + width, topY + height);
            Point Point3 = new(topX, topY + height);
            PointCollection myPointCollection = [Point1, Point2, Point3];
            myPolygon.Points = myPointCollection;
            drawingArea.Children.Add(myPolygon);
        }

        private void DrawRectangle(Canvas drawingArea,
                                   SolidColorBrush brushToUse,
                                   double xPosition,
                                   double yPosition,
                                   double size)
        {
            Polygon myPolygon = new()
            {
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            };
            Point Point1 = new(xPosition, yPosition);
            Point Point2 = new(xPosition + size, yPosition);
            Point Point3 = new(xPosition + size, yPosition + size);
            Point Point4 = new(xPosition, yPosition + size);
            PointCollection myPointCollection = [Point1, Point2, Point3, Point4];
            myPolygon.Points = myPointCollection;
            drawingArea.Children.Add(myPolygon);
        }
    }
}
