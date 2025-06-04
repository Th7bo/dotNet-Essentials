using System.Windows.Controls;

namespace PreyPredator.Contracts
{
    public interface IDisplayable
    {
        void DisplayOn(Canvas canvas);
        void StopDisplaying();
        void UpdateDisplay();
    }
} 