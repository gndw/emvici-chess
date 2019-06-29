using Agate.Chess.Chessman.Utility;
using Agate.MVC.Core;
namespace Agate.Chess.Chessman.Model
{
    public class ChessmanDataModel : BaseModel
    {
        public ChessmanType Type;
        public ChessmanColorType ColorType;
        public ChessmanDataModel(ChessmanType type, ChessmanColorType colorType)
        {
            Type = type;
            ColorType = colorType;
        }
    }
}