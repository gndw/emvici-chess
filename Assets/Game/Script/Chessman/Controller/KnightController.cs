using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class KnightController : ChessmanController
    {
        public KnightController (KnightView view) : base(view)
        {
            
        }

        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Knight;
        }
    }
}