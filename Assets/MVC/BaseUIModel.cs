namespace Agate.MVC.Core
{
    public class BaseUIModel : BaseModel, IBaseUIModel
    {
        public event Function Refresh;

        protected bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                if (_isDirty) Refresh?.Invoke();
            }
        }
    }
}