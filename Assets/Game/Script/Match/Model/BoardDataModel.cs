using System.Collections.Generic;
using Agate.Chess.Match.Utility;
using Agate.MVC.Core;

namespace Agate.Chess.Match.Board.Model
{
    public class BoardDataModel : BaseModel
    {
        public Dictionary<BoardCoord,ChessmanType> Data = new Dictionary<BoardCoord, ChessmanType>();

        public static BoardDataModel CreateAsNormalGame ()
        {
            BoardDataModel result = new BoardDataModel();
            return result;
        }
    }
}