using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class BishopController : ChessmanController
    {
        public BishopController (BishopView view) : base(view)
        {
            
        }

        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Bishop;
        }
    }
}