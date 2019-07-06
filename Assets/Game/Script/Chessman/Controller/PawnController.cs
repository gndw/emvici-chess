using System.Collections.Generic;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Chessman.View;
using Agate.Chess.Prefab.Utility;
namespace Agate.Chess.Chessman.Controller
{
    public class PawnController : ChessmanController<PawnView>
    {
        public override ChessmanType GetChessmanType()
        {
            return ChessmanType.Pawn;
        }
        public override List<BoardCoord> GetPossibleMoves(BoardDataModel boardDataModel)
        {
            List<BoardCoord> possibleMoves = new List<BoardCoord>();
            List<BoardCoord> normalMove = new List<BoardCoord>();
            List<BoardCoord> eatMove = new List<BoardCoord>();

            switch (_colorType)
            {
                case ChessmanColorType.Light:
                    {
                        normalMove.Add(_currentBoardCoordinate.MoveUp());
                        if (_currentBoardCoordinate.Y == 2) normalMove.Add(_currentBoardCoordinate.MoveUp(2));
                        eatMove.Add(_currentBoardCoordinate.MoveCustom(1, 1));
                        eatMove.Add(_currentBoardCoordinate.MoveCustom(-1, 1));
                        break;
                    }
                case ChessmanColorType.Dark:
                    {
                        normalMove.Add(_currentBoardCoordinate.MoveDown());
                        if (_currentBoardCoordinate.Y == 7) normalMove.Add(_currentBoardCoordinate.MoveDown(2));
                        eatMove.Add(_currentBoardCoordinate.MoveCustom(-1, -1));
                        eatMove.Add(_currentBoardCoordinate.MoveCustom(1, -1));
                        break;
                    }
                default:
                    return null;
            }

            normalMove.ForEach((c) =>
            {
                if (c.IsValid() && !boardDataModel.IsBoardCoordinateOccupied(c))
                    possibleMoves.Add(c);
            });

            eatMove.ForEach((c) =>
            {
                if (c.IsValid() && boardDataModel.IsBoardCoordinateOccupiedByEnemy(_colorType, c))
                    possibleMoves.Add(c);
            });

            return possibleMoves;
        }
        protected override string GetViewPrefabPath(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light:
                    return PrefabConstant.PathPawnLightView;
                case ChessmanColorType.Dark:
                    return PrefabConstant.PathPawnDarkView;
                default:
                    return null;
            }
        }
    }
}