using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class KingController : ChessmanController
    {
        public KingController (KingView view) : base(view)
        {
            
        }

        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.King;
        }
    }
}