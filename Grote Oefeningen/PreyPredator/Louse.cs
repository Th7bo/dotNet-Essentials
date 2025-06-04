using System.Windows.Media;
using PreyPredator.Contracts;

namespace PreyPredator
{
    public class Louse : Animal
    {
        public Louse() : base(6, Colors.GreenYellow) { }
        public Louse(Position pos) : base(6, Colors.GreenYellow, pos) { }

        public override IAnimal TryBreed()
        {
            if (_age > 0 && _age % 2 == 0 && !IsDead)
            {
                return new Louse(new Position(Position.X, Position.Y));
            }
            return null;
        }
    }
} 