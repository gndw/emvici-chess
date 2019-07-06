using System.Collections.Generic;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Chessman.View;
using Agate.Chess.Prefab.Utility;
namespace Agate.Chess.Chessman.Controller
{
    public class KingController : ChessmanController<KingView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.King;
        }
        public override List<BoardCoord> GetPossibleMoves(BoardDataModel boardDataModel)
        {
            List<BoardCoord> possibleMoves = new List<BoardCoord>();

            List<BoardCoord> kingMoves = new List<BoardCoord>()
            {
                _currentBoardCoordinate.MoveUp(),
                _currentBoardCoordinate.MoveUpRight(),
                _currentBoardCoordinate.MoveRight(),
                _currentBoardCoordinate.MoveDownRight(),
                _currentBoardCoordinate.MoveDown(),
                _currentBoardCoordinate.MoveDownLeft(),
                _currentBoardCoordinate.MoveLeft(),
                _currentBoardCoordinate.MoveUpLeft(),
            };

            kingMoves.ForEach((c) =>
            {
                if (c.IsValid() && (!boardDataModel.IsBoardCoordinateOccupied(c) || boardDataModel.IsBoardCoordinateOccupiedByEnemy(_colorType, c)))
                {
                    possibleMoves.Add(c);
                }
            });

            return possibleMoves;
        }
        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light:
                    return PrefabConstant.PathKingLightView;
                case ChessmanColorType.Dark:
                    return PrefabConstant.PathKingDarkView;
                default:
                    return null;
            }
        }
    }
}