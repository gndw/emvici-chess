namespace Agate.Chess.Match.Utility
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
    }
}