using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;

namespace Agate.Chess.Chessman.Controller
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