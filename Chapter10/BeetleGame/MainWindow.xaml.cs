using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace BeetleGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Beetle _beetle;
        private DispatcherTimer _timer = new();
        private long _startTime;
        private Stopwatch _stopwatch = new();
        private Random _random = new();
        public MainWindow()
        {
            InitializeComponent();
            int x;
            int y;
            GenerateLocation(out x, out y);
            _beetle = new Beetle(paperCanvas, x, y, 10)
            {
                Speed = speedSlider.Minimum,
                IsVisible = false
            };
            
            _timer.Tick += _timer_Tick;

            _stopwatch = new Stopwatch();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _beetle.ChangePosition();
        }

        private void GenerateLocation(out int x, out int y)
        {
            int tempX;
            int tempY;
            double centerX = paperCanvas.Width / 2;
            double centerY = paperCanvas.Height / 2;
            double radiusSquared = 100 * 100;

            do
            {
                tempX = _random.Next(30, Convert.ToInt32(paperCanvas.Width)  - 30);
                tempY = _random.Next(30, Convert.ToInt32(paperCanvas.Height) - 30);
            }
            while ((tempX - centerX) * (tempX - centerX) + (tempY - centerY) * (tempY - centerY) <= radiusSquared);
            x = tempX;
            y = tempY;
        }

        private void speedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_beetle == null) return;
            _beetle.Speed = Convert.ToDouble(speedSlider.Value);
            _timer.Interval = TimeSpan.FromMilliseconds(100 / _beetle.Speed * _beetle.Size / 10);
            speedLabel.Content = $"{_beetle.Speed:F2}";
        }

        private void sizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_beetle == null) return;
            _beetle.Size = Convert.ToInt32(sizeSlider.Value);
            _timer.Interval = TimeSpan.FromMilliseconds(100 / _beetle.Speed * _beetle.Size / 10);
            sizeLabel.Content = $"{_beetle.Size:F2}";
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
                _timer.Stop();

                long elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
                double distance = _beetle.ComputeDistance(elapsedMilliseconds / 1000);

                messageLabel.Content = $"total distance in meter: {distance / 100:F2} cm";
                startButton.Content = "Start";

                sizeSlider.IsEnabled = true;
                speedSlider.IsEnabled = true;
            }
            else
            {
                _timer.Interval = TimeSpan.FromMilliseconds(100 / _beetle.Speed * _beetle.Size / 10);
                _timer.Start();
                _stopwatch.Restart();
                _beetle.IsVisible = true;
                messageLabel.Content = "";
                startButton.Content = "Stop";

                sizeSlider.IsEnabled = false;
                speedSlider.IsEnabled = false;
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            sizeSlider.Value = sizeSlider.Minimum;
            speedSlider.Value = speedSlider.Minimum;
            _beetle.IsVisible = false;
            _timer.Stop();
            _stopwatch.Reset();
            int x;
            int y;
            GenerateLocation(out x, out y);
            _beetle.X = x;
            _beetle.Y = y;
            startButton.Content = "Start";
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            _beetle.Up = true;
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            _beetle.Up = false;
        }

        private void leftButton_Click(object sender, RoutedEventArgs e)
        {
            _beetle.Right = false;
        }

        private void rightButton_Click(object sender, RoutedEventArgs e)
        {
            _beetle.Right = true;
        }
    }
};

