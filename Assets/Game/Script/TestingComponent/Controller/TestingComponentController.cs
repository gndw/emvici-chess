using Agate.Chess.Progress.Controller;
using Agate.MVC.Core;

namespace Agate.Chess.TestingComponent.Controller
{
    public class TestingComponentController : ComponentController
    {
        [Inject]
        private ProgressController _progressController { get; set; }

        protected override void Init()
        {
            UnityEngine.Debug.Log("communicate with global cont : " + _progressController.SavedNumber);
        }
    }
}