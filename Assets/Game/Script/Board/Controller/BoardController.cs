using System;
using System.Collections.Generic;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Board.View;
using Agate.Chess.Chessman.Controller;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Controller;
using Agate.Chess.Prefab.Utility;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Board.Controller
{
    [Serializable]
    public class BoardController : BaseController
    {
        public event BoardFunction OnBoardSelected;
        public event ChessmanFunction OnChessmanSelected;

        private BoardView _view;
        private List<IChessmanController> _chessmans = new List<IChessmanController>();

        public void Init ()
        {
            _view = PrefabController.Instance.GetObject<BoardView>(PrefabConstant.PathBoardView, Vector3.zero, Quaternion.identity, null);
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
                    case ChessmanType.Pawn      : icc = new PawnController(); break;
                    case ChessmanType.Rook      : icc = new RookController(); break;
                    case ChessmanType.Knight    : icc = new KnightController(); break;
                    case ChessmanType.Bishop    : icc = new BishopController(); break;
                    case ChessmanType.Queen     : icc = new QueenController(); break;
                    case ChessmanType.King      : icc = new KingController();break;
                    default: throw new System.NotImplementedException();
                }
                
                icc.Init(_view.GetBoardPosition, _view.GetFacingDirection, data.Key, data.Value.ColorType);
                icc.OnChessmanSelected += () => OnChessmanSelected?.Invoke(icc.GetChessmanType(), icc.GetChessmanColorType(), icc.GetBoardCoord());
                _chessmans.Add(icc);
            }
        }

        public void SetHighlight (List<BoardCoord> coordinates)
        {
            _view.SetHighlight(coordinates);
        }
    }
}