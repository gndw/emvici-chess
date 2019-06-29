using Agate.Chess.SceneManage.Controller;
using Agate.Chess.SceneManage.Utility;
using Agate.Chess.Schedule.Controller;
using Agate.MVC.Core;

namespace Agate.Chess.Preload.Controller
{
    public class PreloadController : SceneController
    {
        [Inject]
        private SceneManageController _sceneManageController { get; set; }
        [Inject]
        private ScheduleController _scheduleController { get; set; }

        public override void StartScene()
        {
            _sceneManageController.LoadScene((int)SceneState.Match);
        }

        protected override void Load(Sequence createLoadingSequence)
        {
            createLoadingSequence.AddSequence((next) =>
            {
                _scheduleController.WaitForSeconds(1, next);
            });
            createLoadingSequence.AddSequence((next) =>
            {
                _scheduleController.WaitForSeconds(1, next);
            });
        }
    }
}