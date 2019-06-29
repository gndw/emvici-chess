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

        private PrefabController _prefabController;
        private BoardView _view;
        private List<IChessmanController> _chessmans = new List<IChessmanController>();

        public void Init (PrefabController prefabController)
        {
            _prefabController = prefabController;

            _view = _prefabController.GetObject<BoardView>(PrefabConstant.PathBoardView, Vector3.zero, Quaternion.identity, null);
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
                
                icc.Init(_prefabController, _view.GetBoardPosition, _view.GetFacingDirection, data.Key, data.Value.ColorType);
                icc.OnChessmanSelected += () => OnChessmanSelected?.Invoke(icc.GetChessmanType(), icc.GetChessmanColorType(), icc.GetBoardCoord());
                _chessmans.Add(icc);
            }
        }

        public void Move(BoardCoord originCoord, BoardCoord targetCoord, Action onFinish)
        {
            IChessmanController icc;
            if (TryGetChessman(originCoord, out icc))
            {
                IChessmanController targeticc;
                if (TryGetChessman(targetCoord, out targeticc))
                {
                    icc.Move(targetCoord, () => {
                        targeticc.Destroy();
                        _chessmans.Remove(targeticc);
                        onFinish();
                    });
                }
                else
                {
                    icc.Move(targetCoord, onFinish);
                }
            }
            else onFinish();
        }

        public void SetHighlight (List<BoardCoord> coordinates)
        {
            _view.SetHighlight(coordinates);
        }

        public void ClearHighlight ()
        {
            _view.SetHighlight(new List<BoardCoord>());
        }

        public List<BoardCoord> GetPossibleMoves (BoardCoord coord)
        {
            IChessmanController icc;
            if (TryGetChessman(coord, out icc))
            {
                BoardDataModel bdm = GetBoardDataModel();
                return icc.GetPossibleMoves(bdm);
            }
            else return new List<BoardCoord>();
        }

        public BoardDataModel GetBoardDataModel ()
        {
            BoardDataModel result = new BoardDataModel();
            _chessmans.ForEach((icc) => {
                result.Data.Add(icc.GetBoardCoord(), new Chessman.Model.ChessmanDataModel(icc.GetChessmanType(), icc.GetChessmanColorType()));
            });
            return result;
        }

        private bool TryGetChessman (BoardCoord coord, out IChessmanController icc)
        {
            icc = _chessmans.Find(chessman => chessman.GetBoardCoord() == coord);
            if (icc != null) return true;
            else return false;
        }
    }
}