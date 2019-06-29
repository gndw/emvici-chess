using System;
using System.Collections.Generic;
using Agate.Chess.Board.Model;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Controller;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Chessman.Controller
{
    public interface IChessmanController
    {
        event Function OnChessmanSelected;
        void Init(PrefabController prefabController, Func<BoardCoord,Vector3> getBoardPosition, Func<ChessmanColorType,Vector3> getFacingDirection, BoardCoord coordinate, ChessmanColorType colorType);
        void Move(BoardCoord targetCoord, Action onFinish);
        ChessmanType GetChessmanType();
        ChessmanColorType GetChessmanColorType();
        BoardCoord GetBoardCoord();
        List<BoardCoord> GetPossibleMoves(BoardDataModel boardDataModel);
        void Destroy();
    }
}