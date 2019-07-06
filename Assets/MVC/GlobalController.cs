using System;

namespace Agate.MVC.Core
{
    public abstract class GlobalController<T> : BaseController, IGlobalController where T : GlobalController<T>, new()
    {
        public virtual void Init(Action onInitialized)
        {
            onInitialized();
        }
    }
}