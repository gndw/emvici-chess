using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class KnightController : ChessmanController<KnightView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Knight;
        }

        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light : return PrefabConstant.PathKnightLightView;
                case ChessmanColorType.Dark : return PrefabConstant.PathKnightDarkView;
                default: return null;
            }
        }
    }
}