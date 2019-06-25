using Agate.Chess.Match.Chessman.View;
using Agate.Chess.Match.Utility;

namespace Agate.Chess.Match.Chessman.Controller
{
    public class QueenController : ChessmanController
    {
        public QueenController (QueenView view) : base (view)
        {
            
        }

        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Queen;
        }
    }
}