using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PreyPredator.Contracts;

namespace PreyPredator
{
    public abstract class Animal : IAnimal
    {
        private static Random _randomGenerator = new Random();
        protected Ellipse _ellipse;
        protected int _age;
        protected int _maxAge;
        protected Color _color;
        protected Canvas _canvas;
        public Position Position { get; set; }
        public bool IsDead { get; set; }

        protected Animal(int maxAge, Color color)
            : this(maxAge, color, new Position(_randomGenerator.Next(16), _randomGenerator.Next(16))) {}

        protected Animal(int maxAge, Color color, Position position)
        {
            _maxAge = maxAge;
            _color = color;
            Position = new Position(position.X, position.Y);
            _ellipse = new Ellipse
            {
                Width = 5,
                Height = 5,
                Fill = new SolidColorBrush(_color)
            };
            IsDead = false;
            _age = 0;
        }

        public virtual void DisplayOn(Canvas canvas)
        {
            _canvas = canvas;
            if (!_canvas.Children.Contains(_ellipse))
                _canvas.Children.Add(_ellipse);
            UpdateDisplay();
        }

        public virtual void StopDisplaying()
        {
            _canvas?.Children.Remove(_ellipse);
        }

        public virtual void UpdateDisplay()
        {
            if (_ellipse != null)
            {
                _ellipse.Margin = new System.Windows.Thickness(Position.X * 10, Position.Y * 10, 0, 0);
            }
        }

        public virtual void Move()
        {
            if (IsDead) return;
            _age++;
            if (_age > _maxAge)
            {
                IsDead = true;
                StopDisplaying();
                return;
            }
            int dir = _randomGenerator.Next(4);
            switch (dir)
            {
                case 0: Position.MoveUp(); break;
                case 1: Position.MoveDown(); break;
                case 2: Position.MoveLeft(); break;
                case 3: Position.MoveRight(); break;
            }
            UpdateDisplay();
        }

        public abstract IAnimal TryBreed();
    }
} 