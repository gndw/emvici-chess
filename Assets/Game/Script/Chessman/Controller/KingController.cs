using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class KingController : ChessmanController<KingView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.King;
        }

        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light : return PrefabConstant.PathKingLightView;
                case ChessmanColorType.Dark : return PrefabConstant.PathKingDarkView;
                default: return null;
            }
        }
    }
}