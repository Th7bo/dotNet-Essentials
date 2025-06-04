namespace PreyPredator
{
    public class Position
    {
        private int _x;
        private int _y;
        public int X
        {
            get => _x;
            set => _x = value < 0 ? 0 : value > 15 ? 15 : value;
        }
        public int Y
        {
            get => _y;
            set => _y = value < 0 ? 0 : value > 15 ? 15 : value;
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveUp() => Y--;
        public void MoveDown() => Y++;
        public void MoveLeft() => X--;
        public void MoveRight() => X++;
    }
} 