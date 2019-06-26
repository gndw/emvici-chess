using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Utility;
using System.Collections.Generic;
using Agate.Chess.Board.Utility;
using Agate.Chess.Board.Model;

namespace Agate.Chess.Chessman.Controller
{
    public class RookController : ChessmanController<RookView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Rook;
        }

        public override List<BoardCoord> GetPossibleMoves(BoardDataModel boardDataModel)
        {
            throw new System.NotImplementedException();
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