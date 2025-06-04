namespace Reservation
{
    // ToDo MovieTheaterHall is a subclass of Hall. There are seats.
    public class MovieTheaterHall : Hall
    {
        public override bool HasChairs => true;
        public int NumberOfRows { get; }
        public bool[,] IsSeatReserved { get; }

        public MovieTheaterHall(string name, int maxCapacity) : base(name, maxCapacity)
        {
            NumberOfRows = maxCapacity / ReservationConstants.SeatsOnRow;
            IsSeatReserved = new bool[NumberOfRows, ReservationConstants.SeatsOnRow];
        }
    }
}
