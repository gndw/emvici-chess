namespace Agate.MVC.Core
{
    public abstract class BaseUIView<T> : BaseView where T : IBaseUIModel
    {
        protected T _model;

        public void Set(T model)
        {
            _model = model;
            _model.Refresh += UpdateView;
        }

        public void ForceUpdateView()
        {
            UpdateView();
        }

        protected abstract void UpdateView();
    }
}