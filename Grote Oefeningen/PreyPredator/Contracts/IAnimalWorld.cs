using System.Collections.Generic;

namespace PreyPredator.Contracts
{
    public interface IAnimalWorld
    {
        IList<IAnimal> AllAnimals { get; }
        int CurrentRoundNumber { get; }
        void AddAnimal(IAnimal animal);
        void ProcessRound();
    }
} 