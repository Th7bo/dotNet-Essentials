using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BeetleGame
{
    public class Beetle
    {
        private int _size;
        private int _x;
        private int _y;
        private Canvas _canvas;
        private Ellipse _ellipse;

        public Beetle(Canvas canvas, int x, int y, int size) 
        {
            _canvas = canvas;
            _x = x;
            _y = y;
            _size = size;

            Right = true;
            Up = true;

            CreateBeetle();
        }

        public bool IsVisible
        {
            get => _ellipse.Visibility == Visibility.Visible;
            set 
            {
                _ellipse.Visibility = value == true ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool Up { get; set; }
        public bool Right { get; set; }
        public double Speed { get; set; }
        public int Size { 
            get => _size; 
            set
            {
                _size = value;
                UpdateSizeEllipse();
            }
        }

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                UpdatePositionEllipse();
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                UpdatePositionEllipse();
            }
        }

        private void UpdatePositionEllipse()
        {
            _ellipse.Margin = new Thickness(X - Size / 2, Y - Size / 2, 0, 0);
            DetectCollission();
        }

        private void UpdateSizeEllipse()
        {
            _ellipse.Width = Size;
            _ellipse.Height = Size;
            X = (int) Math.Clamp(X, Size / 2, _canvas.Width - Size / 2);
            Y = (int) Math.Clamp(Y, Size / 2, _canvas.Height - Size / 2);
            UpdatePositionEllipse();
        }

        public void ChangePosition()
        {
            if (Speed <= 0) return;
            Y += Up     ? -1 :  1;
            X += Right  ?  1 : -1;
            //Debug.WriteLine($"X: {X} Y: {Y}");
            //Debug.WriteLine($"Canvas X: {_canvas.Width} Canvas Y: {_canvas.Height}");
            DetectCollission();
        }

        private void DetectCollission()
        {
            if ((X + Size / 2 >= _canvas.Width && Right) || (X - Size / 2 <= 0 && !Right))
            {
                Right = !Right;
                X = (int) Math.Clamp(X, Size / 2, _canvas.Width - Size / 2);
            }

            if ((Y + Size / 2 >= _canvas.Height && !Up) || (Y - Size / 2 <= 0 && Up))
            {
                Up = !Up;
                Y = (int) Math.Clamp(Y, Size / 2, _canvas.Height - Size / 2);
            }
        }


        public double ComputeDistance(double seconds)
        {
            return Speed * (seconds);
        }

        public double ComputeDistance(DateTime first, DateTime second)
        {
            double elapsedSeconds = (second - first).TotalSeconds / 100;
            return ComputeDistance(elapsedSeconds);
        }

        private void CreateBeetle()
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Red);
            _ellipse = new()
            {
                Stroke = brush,
                Margin = new Thickness(_x - _size / 2, _y - _size / 2, 0, 0),
                Width = _size,
                Height = _size,
                Fill = brush
            };
            _canvas.Children.Add(_ellipse);
        }

    }
}
