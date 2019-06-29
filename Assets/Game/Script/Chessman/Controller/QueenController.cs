using System.Collections.Generic;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Chessman.View;
using Agate.Chess.Prefab.Utility;
namespace Agate.Chess.Chessman.Controller
{
    public class QueenController : ChessmanController<QueenView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Queen;
        }
        public override List<BoardCoord> GetPossibleMoves(BoardDataModel boardDataModel)
        {
            throw new System.NotImplementedException();
        }
        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light:
                    return PrefabConstant.PathQueenLightView;
                case ChessmanColorType.Dark:
                    return PrefabConstant.PathQueenDarkView;
                default:
                    return null;
            }
        }
    }
}