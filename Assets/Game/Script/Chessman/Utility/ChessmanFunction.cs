using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Controller;

namespace Agate.Chess.Chessman.Utility
{
    public delegate void ChessmanFunction (ChessmanType type, ChessmanColorType colorType, BoardCoord coord);
    public delegate void ChessmanControllerFunction (IChessmanController chessmanController);
}