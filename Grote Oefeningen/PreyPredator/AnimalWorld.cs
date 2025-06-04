using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using PreyPredator.Contracts;

namespace PreyPredator
{
    public class AnimalWorld : IAnimalWorld
    {
        private IList<IAnimal> _animals = new List<IAnimal>();
        private Canvas _canvas;
        private int _round = 0;

        public AnimalWorld(Canvas canvas)
        {
            _canvas = canvas;
        }
        
        public IList<IAnimal> AllAnimals => _animals;
        public int CurrentRoundNumber => _round;
        public void AddAnimal(IAnimal animal)
        {
            animal.DisplayOn(_canvas);
            _animals.Add(animal);
        }
        public void ProcessRound()
        {
            _round++;
            var newborns = new List<IAnimal>();
            var dead = new List<IAnimal>();
            foreach (var animal in _animals)
            {
                animal.Move();
                var baby = animal.TryBreed();
                if (baby != null)
                    newborns.Add(baby);
                if (animal.IsDead)
                    dead.Add(animal);
            }
            foreach (var predator in _animals.OfType<IPredator>())
            {
                predator.Hunt(_animals.Where(a => !a.IsDead).ToList());
            }
            dead.AddRange(_animals.Where(a => a.IsDead && !dead.Contains(a)));
            foreach (var d in dead.Distinct().ToList())
            {
                d.StopDisplaying();
                _animals.Remove(d);
            }
            foreach (var n in newborns)
            {
                AddAnimal(n);
            }
        }
    }
} 