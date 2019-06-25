using System.Collections.Generic;
using Agate.Chess.Match.Utility;
using Agate.MVC.Core;

namespace Agate.Chess.Match.Board.Model
{
    public class BoardDataModel : BaseModel
    {
        public Dictionary<BoardCoord,ChessmanDataModel> Data = new Dictionary<BoardCoord, ChessmanDataModel>();

        public static BoardDataModel CreateAsNormalGame ()
        {
            BoardDataModel result = new BoardDataModel();

            for (int i = 1; i <= 8; i++)
            {
                result.Data.Add(new BoardCoord(i,2), new ChessmanDataModel(ChessmanType.Pawn, ChessmanColorType.White));   
                result.Data.Add(new BoardCoord(i,7), new ChessmanDataModel(ChessmanType.Pawn, ChessmanColorType.Black));
            }

            result.Data.Add(new BoardCoord(1,1), new ChessmanDataModel(ChessmanType.Rook, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(8,1), new ChessmanDataModel(ChessmanType.Rook, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(1,8), new ChessmanDataModel(ChessmanType.Rook, ChessmanColorType.Black));
            result.Data.Add(new BoardCoord(8,8), new ChessmanDataModel(ChessmanType.Rook, ChessmanColorType.Black));

            result.Data.Add(new BoardCoord(2,1), new ChessmanDataModel(ChessmanType.Knight, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(7,1), new ChessmanDataModel(ChessmanType.Knight, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(2,8), new ChessmanDataModel(ChessmanType.Knight, ChessmanColorType.Black));
            result.Data.Add(new BoardCoord(7,8), new ChessmanDataModel(ChessmanType.Knight, ChessmanColorType.Black));

            result.Data.Add(new BoardCoord(3,1), new ChessmanDataModel(ChessmanType.Bishop, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(6,1), new ChessmanDataModel(ChessmanType.Bishop, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(3,8), new ChessmanDataModel(ChessmanType.Bishop, ChessmanColorType.Black));
            result.Data.Add(new BoardCoord(6,8), new ChessmanDataModel(ChessmanType.Bishop, ChessmanColorType.Black));

            result.Data.Add(new BoardCoord(4,1), new ChessmanDataModel(ChessmanType.Queen, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(5,8), new ChessmanDataModel(ChessmanType.Queen, ChessmanColorType.Black));
            
            result.Data.Add(new BoardCoord(5,1), new ChessmanDataModel(ChessmanType.King, ChessmanColorType.White));
            result.Data.Add(new BoardCoord(4,8), new ChessmanDataModel(ChessmanType.King, ChessmanColorType.Black));

            return result;
        }
    }
}