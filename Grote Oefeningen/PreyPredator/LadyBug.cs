using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using PreyPredator.Contracts;

namespace PreyPredator
{
    public class LadyBug : Animal, IPredator
    {
        private int _roundsWithoutFood = 0;
        public LadyBug() : base(16, Colors.Red) { }
        public LadyBug(Position pos) : base(16, Colors.Red, pos) { }

        public override IAnimal TryBreed()
        {
            if (_age > 0 && _age % 4 == 0 && !IsDead)
            {
                return new LadyBug(new Position(Position.X, Position.Y));
            }
            return null;
        }

        public bool CanEat(IAnimal animal)
        {
            return animal is Louse && !animal.IsDead && DistanceTo(animal) <= 3;
        }

        public void Hunt(IList<IAnimal> possibleVictims)
        {
            var victims = possibleVictims.Where(CanEat).ToList();
            if (victims.Count > 0)
            {
                foreach (var victim in victims)
                {
                    victim.IsDead = true;
                    victim.StopDisplaying();
                }
                _roundsWithoutFood = 0;
            }
            else
            {
                _roundsWithoutFood++;
                if (_roundsWithoutFood >= 3)
                {
                    IsDead = true;
                    StopDisplaying();
                }
            }
        }

        private double DistanceTo(IAnimal animal)
        {
            int dx = this.Position.X - animal.Position.X;
            int dy = this.Position.Y - animal.Position.Y;
            return System.Math.Sqrt(dx * dx + dy * dy);
        }
    }
} 