using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class PawnController : ChessmanController
    {
        public PawnController(PawnView view) : base(view)
        {
            
        }

        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Pawn;
        }
    }
}