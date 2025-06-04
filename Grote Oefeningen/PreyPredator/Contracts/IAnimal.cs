using System.Windows.Controls;

namespace PreyPredator.Contracts
{
    public interface IAnimal : IDisplayable
    {
        Position Position { get; }
        bool IsDead { get; set; }
        void Move();
        IAnimal TryBreed();
    }
} 