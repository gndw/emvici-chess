using Agate.MVC.Core;

namespace Agate.Chess.Menu.Model
{
    public class MenuModel : BaseUIModel, IMenuModel
    {
        public int Point { get; private set; }

        public void SetPoint(int point)
        {
            Point = point;
            IsDirty = true;
        }
    }
}