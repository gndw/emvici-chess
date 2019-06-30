namespace Agate.MVC.Core
{
    public class BaseUIModel : BaseModel
    {
        public event Function Refresh;

        protected bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            protected set
            {
                _isDirty = value;
                Refresh?.Invoke();
            }
        }
    }
}