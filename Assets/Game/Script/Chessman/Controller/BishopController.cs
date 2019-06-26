using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Utility;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using System.Collections.Generic;

namespace Agate.Chess.Chessman.Controller
{
    public class BishopController : ChessmanController<BishopView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Bishop;
        }

        public override List<BoardCoord> GetPossibleMoves(BoardDataModel boardDataModel)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light : return PrefabConstant.PathBishopLightView;
                case ChessmanColorType.Dark : return PrefabConstant.PathBishopDarkView;
                default: return null;
            }
        }
    }
}