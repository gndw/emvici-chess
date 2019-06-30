using Agate.MVC.Core;

namespace Agate.Chess.Menu.Model
{
    public interface IMenuModel : IBaseUIModel
    {
        int Point { get; }
    }
}