using Agate.Chess.Match.Chessman.View;
using Agate.Chess.Match.Utility;

namespace Agate.Chess.Match.Chessman.Controller
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