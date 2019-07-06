using System;
using System.Collections.Generic;
using UnityEngine;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Chessman.View;
using Agate.MVC.Core;

namespace Agate.Chess.Chessman.Controller
{
    public abstract class ChessmanController<T> : BaseController, IChessmanController where T : ChessmanView
    {
        public event Function OnChessmanSelected;
        protected PrefabController _prefabController;
        protected T _view;
        protected Func<BoardCoord, Vector3> _getBoardPosition;
        protected Func<ChessmanColorType, Vector3> _getFacingDirection;
        protected BoardCoord _currentBoardCoordinate;
        protected ChessmanColorType _colorType;
        public void Init(PrefabController prefabController, Func<BoardCoord, Vector3> getBoardPosition, Func<ChessmanColorType, Vector3> getFacingDirection, BoardCoord coordinate, ChessmanColorType colorType)
        {
            _prefabController = prefabController;
            _getBoardPosition = getBoardPosition;
            _getFacingDirection = getFacingDirection;
            _currentBoardCoordinate = coordinate;
            _colorType = colorType;
            _view = _prefabController.GetObject<T>(GetViewPrefabPath(_colorType), _getBoardPosition(_currentBoardCoordinate), Quaternion.LookRotation(getFacingDirection(_colorType)), null);
            _view.OnChessmanSelected += () => OnChessmanSelected?.Invoke();
        }
        public ChessmanColorType GetChessmanColorType()
        {
            return _colorType;
        }
        public BoardCoord GetBoardCoord()
        {
            return _currentBoardCoordinate;
        }
        public abstract ChessmanType GetChessmanType();
        public void Destroy()
        {
            if (_view != null) UnityEngine.Object.Destroy(_view.gameObject);
        }
        protected abstract string GetViewPrefabPath(ChessmanColorType colorType);
        public abstract List<BoardCoord> GetPossibleMoves(BoardDataModel boardDataModel);
        public void Move(BoardCoord targetCoord, Action onFinish)
        {
            _currentBoardCoordinate = targetCoord;
            _view.Move(_getBoardPosition(targetCoord), onFinish);
        }

        protected bool IsAbleToMoveHere(BoardCoord target, BoardDataModel bdm, Action<BoardCoord> onCanMove)
        {
            if (target.IsValid())
            {
                if (bdm.IsBoardCoordinateOccupied(target))
                {
                    if (bdm.IsBoardCoordinateOccupiedByEnemy(_colorType, target))
                    {
                        onCanMove(target);
                        return false;
                    }
                    else return false;
                }
                else
                {
                    onCanMove(target);
                    return true;
                }
            }
            else return false;
        }

        protected void CalculateContinousMoves(List<Func<int, BoardCoord>> continousMethods, BoardDataModel bdm, Action<BoardCoord> onCanMove)
        {
            continousMethods.ForEach((method) =>
            {
                int counter = 0;
                BoardCoord step;
                do
                {
                    counter += 1;
                    step = method(counter);
                }
                while (IsAbleToMoveHere(step, bdm, onCanMove));
            });
        }
    }
}