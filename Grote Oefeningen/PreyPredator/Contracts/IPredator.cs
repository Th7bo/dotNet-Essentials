using System.Collections.Generic;

namespace PreyPredator.Contracts
{
    public interface IPredator : IAnimal
    {
        bool CanEat(IAnimal animal);
        void Hunt(IList<IAnimal> possibleVictims);
    }
} 