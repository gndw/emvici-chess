using System;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Chessman.Controller
{
    public interface IChessmanController
    {
        event Function OnChessmanSelected;
        void Init(Func<BoardCoord,Vector3> getBoardPosition, Func<ChessmanColorType,Vector3> getFacingDirection, BoardCoord coordinate, ChessmanColorType colorType);
        ChessmanType GetChessmanType();
        ChessmanColorType GetChessmanColorType();
        BoardCoord GetBoardCoord();
        void Destroy();
    }
}