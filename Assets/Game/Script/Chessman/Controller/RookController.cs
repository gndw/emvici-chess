using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class RookController : ChessmanController<RookView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Rook;
        }

        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light : return PrefabConstant.PathRookLightView;
                case ChessmanColorType.Dark : return PrefabConstant.PathRookDarkView;
                default: return null;
            }
        }
    }
}