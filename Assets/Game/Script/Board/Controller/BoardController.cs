using System;
using System.Collections.Generic;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Board.View;
using Agate.Chess.Chessman.Controller;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Chessman.View;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Board.Controller
{
    [Serializable]
    public class BoardController : BaseController
    {
        public event BoardFunction OnBoardSelected;
        public event ChessmanFunction OnChessmanSelected;

        [SerializeField]
        private BoardView _viewPrefab;
        [SerializeField]
        private ChessmanViewPrefab _whitePrefabs;
        [SerializeField]
        private ChessmanViewPrefab _blackPrefabs;

        [Serializable]
        public class ChessmanViewPrefab
        {
            public PawnView PawnView;
            public RookView RookView;
            public KnightView KnightView;
            public BishopView BishopView;
            public QueenView QueenView;
            public KingView KingView;
        }


        private BoardView _view;
        private List<IChessmanController> _chessmans = new List<IChessmanController>();

        public void Init ()
        {
            _view = UnityEngine.Object.Instantiate(_viewPrefab, Vector3.zero, Quaternion.identity);
            _view.OnBoardSelected += (boardcoord) => OnBoardSelected?.Invoke(boardcoord);

            SetChessmanOnBoard(BoardDataModel.CreateAsNormalGame());
        }

        public void SetChessmanOnBoard (BoardDataModel boardDataModel)
        {
            _chessmans.ForEach((cc) => cc.Destroy());
            _chessmans.Clear();
            foreach (var data in boardDataModel.Data)
            {
                IChessmanController icc;
                switch (data.Value.Type)
                {
                    case ChessmanType.Pawn      : icc = new PawnController(data.Value.ColorType == ChessmanColorType.White ? _whitePrefabs.PawnView : _blackPrefabs.PawnView); break;
                    case ChessmanType.Rook      : icc = new RookController(data.Value.ColorType == ChessmanColorType.White ? _whitePrefabs.RookView : _blackPrefabs.RookView); break;
                    case ChessmanType.Knight    : icc = new KnightController(data.Value.ColorType == ChessmanColorType.White ? _whitePrefabs.KnightView : _blackPrefabs.KnightView); break;
                    case ChessmanType.Bishop    : icc = new BishopController(data.Value.ColorType == ChessmanColorType.White ? _whitePrefabs.BishopView : _blackPrefabs.BishopView); break;
                    case ChessmanType.Queen     : icc = new QueenController(data.Value.ColorType == ChessmanColorType.White ? _whitePrefabs.QueenView : _blackPrefabs.QueenView); break;
                    case ChessmanType.King      : icc = new KingController(data.Value.ColorType == ChessmanColorType.White ? _whitePrefabs.KingView : _blackPrefabs.KingView);break;
                    default: throw new System.NotImplementedException();
                }
                
                icc.Init(_view.GetBoardPosition, data.Key, data.Value.ColorType);
                icc.OnChessmanSelected += () => OnChessmanSelected?.Invoke(icc.GetChessmanType(), icc.GetChessmanColorType(), icc.GetBoardCoord());
                _chessmans.Add(icc);
            }
        }
    }
}