using System;
using Agate.Chess.Match.Board.View;
using Agate.Chess.Match.Utility;
using UnityEngine;

namespace Agate.Chess.Match.Board.Controller
{
    [Serializable]
    public class BoardController
    {
        public event BoardFunction OnBoardSelected;

        [SerializeField]
        private BoardView _viewPrefab;
        private BoardView _view;

        public void Init ()
        {
            _view = UnityEngine.Object.Instantiate(_viewPrefab, Vector3.zero, Quaternion.identity);
            _view.OnBoardSelected += (boardcoord) => OnBoardSelected?.Invoke(boardcoord);
        }
    }
}