using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class RookController : ChessmanController
    {
        public RookController (RookView view) : base (view)
        {
            
        }

        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Rook;
        }
    }
}