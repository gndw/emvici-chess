using Agate.Chess.Chessman.View;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Utility;
using System.Collections.Generic;
using Agate.Chess.Board.Utility;
using Agate.Chess.Board.Model;

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
            switch (_colorType)
            {
                case ChessmanColorType.Light :
                {
                    possibleMoves.Add(new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y + 1));
                    if (_currentBoardCoordinate.Y == 2) {
                        possibleMoves.Add(new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y + 2));
                    }
                    break;
                }
                case ChessmanColorType.Dark :
                {
                    possibleMoves.Add(new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y - 1));
                    if (_currentBoardCoordinate.Y == 7) {
                        possibleMoves.Add(new BoardCoord(_currentBoardCoordinate.X, _currentBoardCoordinate.Y - 2));
                    }
                    break;
                }
            }

            return possibleMoves;
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