using Agate.Chess.Match.Chessman.View;
using Agate.Chess.Match.Utility;

namespace Agate.Chess.Match.Chessman.Controller
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