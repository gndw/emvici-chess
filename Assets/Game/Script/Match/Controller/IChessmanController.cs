using System;
using Agate.Chess.Match.Utility;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Match.Chessman
{
    public interface IChessmanController
    {
        event Function OnChessmanSelected;
        void Init(Func<BoardCoord,Vector3> getBoardPosition, BoardCoord coordinate, ChessmanColorType colorType);
        ChessmanType GetChessmanType();
        ChessmanColorType GetChessmanColorType();
        BoardCoord GetBoardCoord();
        void Destroy();
    }
}