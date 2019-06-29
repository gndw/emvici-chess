namespace Agate.Chess.Board.Utility
{
    public class BoardCoord
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public BoardCoord(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool IsValid()
        {
            return X >= 1 && X <= 8 && Y >= 1 && Y <= 8;
        }
        public static bool operator ==(BoardCoord coord1, BoardCoord coord2)
        {
            return coord1.X == coord2.X && coord1.Y == coord2.Y;
        }
        public override bool Equals(object obj)
        {
            if (obj != null && obj is BoardCoord)
            {
                BoardCoord bdObj = obj as BoardCoord;
                return this == bdObj;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hash = 7;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            return hash;
        }
        public static bool operator !=(BoardCoord coord1, BoardCoord coord2)
        {
            return !(coord1 == coord2);
        }
    }
}