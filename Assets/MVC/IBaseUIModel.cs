namespace Agate.MVC.Core
{
    public interface IBaseUIModel
    {
        event Function Refresh;
        bool IsDirty { get; set; }
    }
}