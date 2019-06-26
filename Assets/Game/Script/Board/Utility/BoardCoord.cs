namespace Agate.Chess.Board.Utility
{
    public class BoardCoord
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public BoardCoord (int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsValid ()
        {
            return X >= 1 && X <= 8 && Y >= 1 && Y <= 8;
        }
    }
}