using System;
using Agate.Chess.Match.Chessman.View;
using Agate.Chess.Match.Utility;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Match.Chessman.Controller
{
    public abstract class ChessmanController : BaseController, IChessmanController
    {
        public event Function OnChessmanSelected;

        private ChessmanView _viewPrefab;

        protected ChessmanView _view;
        protected Func<BoardCoord,Vector3> _getBoardPosition;
        protected BoardCoord _currentBoardCoordinate;
        protected ChessmanColorType _colorType;

        public ChessmanController(ChessmanView view)
        {
            _viewPrefab = view;
        }

        public void Init (Func<BoardCoord,Vector3> getBoardPosition, BoardCoord coordinate, ChessmanColorType colorType)
        {
            _getBoardPosition = getBoardPosition;
            _currentBoardCoordinate = coordinate;
            _colorType = colorType;

            _view = UnityEngine.Object.Instantiate(_viewPrefab, _getBoardPosition(_currentBoardCoordinate), Quaternion.identity);
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

        public void Destroy ()
        {
            if (_view != null) UnityEngine.Object.Destroy(_view);
        }
        
    }
}