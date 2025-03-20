using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Some_Shapes
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

        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
            Rectangle upperLeftRectangle = new()
            {
                Width = 100,
                Height = 50,
                Margin = new Thickness(10, 10, 0, 0),
                Stroke = new SolidColorBrush(Colors.Black)
            };
            paperCanvas.Children.Add(upperLeftRectangle);

            Line lineInRectangle = new()
            {
                X1 = 10,
                Y1 = 10,
                X2 = 110,
                Y2 = 60,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            paperCanvas.Children.Add(lineInRectangle);

            Rectangle middleLeftRectangle = new()
            {
                Width = 100,
                Height = 50,
                Margin = new Thickness(10, 80, 0, 0),
                Stroke = new SolidColorBrush(Colors.Black)
            };
            paperCanvas.Children.Add(middleLeftRectangle);

            Ellipse ellipseInRectangle = new()
            {
                Width = 100,
                Height = 50,
                Margin = new Thickness(10, 80, 0, 0),
                Stroke = new SolidColorBrush(Colors.Black)
            };
            paperCanvas.Children.Add(ellipseInRectangle);

            Ellipse lowerLeftEllipse = new()
            {
                Width = 100,
                Height = 50,
                Margin = new Thickness(10, 150, 0, 0),
                Fill = new SolidColorBrush(Colors.Gray)
            };
            paperCanvas.Children.Add(lowerLeftEllipse);

            //BitmapImage demoBitmapImage = new();
            //demoBitmapImage.BeginInit();
            // hard gecodeerd
            //demoBitmapImage.UriSource = new Uri(@"images\logo_pxl_digital.png",
            //                       UriKind.RelativeOrAbsolute);
            // image bevindt zich in het project => vergeet de properties (Content, Build Action) van de image niet aan te passen anders zie je niets!
            // demoBitmapImage.UriSource = new Uri("logo_pxl_digital.png", UriKind.RelativeOrAbsolute);
            // image bevindt zich in een map images in het project
            // demoBitmapImage.UriSource = new Uri(@"images\logo_pxl_digital.png", UriKind.RelativeOrAbsolute);

            Image rightImage = new()
            {
                Width = 150,
                Height = 150,
                Margin = new Thickness(120, 10, 0, 0),
                Source = new BitmapImage(new Uri(@"images\logo_pxl_digital.png", UriKind.RelativeOrAbsolute))
            };

            paperCanvas.Children.Add(rightImage);

            Ellipse ellipse = new()
            {
                Height = 100,
                Width = 100,
                Margin = new Thickness(10, 10, 0, 0),
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.Red)

            };
            paperCanvas.Children.Add(ellipse);

            Line lineStroke1 = new()
            {
                X1 = 10,
                X2 = 110,
                Y1 = 210,
                Y2 = 210,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 3
            };
            paperCanvas.Children.Add(lineStroke1);

            Line lineStroke2 = new()
            {
                X1 = 60,
                X2 = 60,
                Y1 = 210,
                Y2 = 260,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 3
            };
            paperCanvas.Children.Add(lineStroke2);
        }
    }
}
