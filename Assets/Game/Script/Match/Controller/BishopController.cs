using Agate.Chess.Match.Chessman.View;
using Agate.Chess.Match.Utility;

namespace Agate.Chess.Match.Chessman.Controller
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