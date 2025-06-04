namespace Reservation
{
    public abstract class Hall 
    {
        public string Name { get; }
        public int MaxCapacity { get; } // maximum number of people
        
        public abstract bool HasChairs { get; }

        public Hall(string name, int maxCapacity)
        {
            Name = name;
            MaxCapacity = maxCapacity;
        }
    }
}
