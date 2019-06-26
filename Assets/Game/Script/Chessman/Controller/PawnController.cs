using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Utility;

namespace Agate.Chess.Chessman.Controller
{
    public class PawnController : ChessmanController<PawnView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Pawn;
        }

        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light : return PrefabConstant.PathPawnLightView;
                case ChessmanColorType.Dark : return PrefabConstant.PathPawnDarkView;
                default: return null;
            }
        }
    }
}