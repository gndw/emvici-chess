using System;

namespace Agate.MVC.Core
{
    public interface IGlobalController
    {
        void Init(Action onInitialized);
    }
}