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
            BoardCoord oneStep;
            BoardCoord twoStep;
            int twoStepCoordinate;
            BoardCoord eatRightStep;
            BoardCoord eatLeftStep;
            List<BoardCoord> possibleMoves = new List<BoardCoord>();
            switch (_colorType)
            {
                case ChessmanColorType.Light:
                    {
                        oneStep = new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y + 1);
                        twoStep = new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y + 2);
                        twoStepCoordinate = 2;
                        eatRightStep = new BoardCoord(_currentBoardCoordinate.X + 1, _currentBoardCoordinate.Y + 1);
                        eatLeftStep = new BoardCoord(_currentBoardCoordinate.X - 1, _currentBoardCoordinate.Y + 1);
                        break;
                    }
                case ChessmanColorType.Dark:
                    {
                        oneStep = new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y - 1);
                        twoStep = new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y - 2);
                        twoStepCoordinate = 7;
                        eatRightStep = new BoardCoord(_currentBoardCoordinate.X - 1, _currentBoardCoordinate.Y - 1);
                        eatLeftStep = new BoardCoord(_currentBoardCoordinate.X + 1, _currentBoardCoordinate.Y - 1);
                        break;
                    }
                default:
                    return null;
            }
            if (oneStep.IsValid() && !boardDataModel.IsBoardCoordinateOccupied(oneStep))
            {
                possibleMoves.Add(oneStep);
            }
            if (_currentBoardCoordinate.Y == twoStepCoordinate && twoStep.IsValid() && !boardDataModel.IsBoardCoordinateOccupied(twoStep))
            {
                possibleMoves.Add(twoStep);
            }
            if (eatRightStep.IsValid() && boardDataModel.IsBoardCoordinateOccupiedByEnemy(_colorType, eatRightStep))
            {
                possibleMoves.Add(eatRightStep);
            }
            if (eatLeftStep.IsValid() && boardDataModel.IsBoardCoordinateOccupiedByEnemy(_colorType, eatLeftStep))
            {
                possibleMoves.Add(eatLeftStep);
            }
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