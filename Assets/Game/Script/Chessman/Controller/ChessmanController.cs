using System;
using System.Collections.Generic;
using UnityEngine;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Chessman.View;
using Agate.Chess.Prefab.Controller;
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
    }
}