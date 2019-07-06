using System;
using Agate.Chess.Request.Controller;
using Agate.MVC.Core;

namespace Agate.Chess.Progress.Controller
{
    public class ProgressController : GlobalController<ProgressController>
    {
        public int SavedNumber;

        [Inject]
        private RequestController _requestController { get; set; }

        public override void Init(Action onInitialized)
        {
            _requestController.FetchSavedNumber((savedNumber) =>
            {
                SavedNumber = savedNumber;
                onInitialized();
            });
        }
    }
}