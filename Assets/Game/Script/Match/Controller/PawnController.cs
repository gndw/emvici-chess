using Agate.Chess.Match.Chessman.View;
using Agate.Chess.Match.Utility;

namespace Agate.Chess.Match.Chessman.Controller
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