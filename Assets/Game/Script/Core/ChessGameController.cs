using Agate.Chess.Progress.Controller;
using Agate.Chess.Request.Controller;
using Agate.Chess.SceneManage.Controller;
using Agate.MVC.Core;

namespace Agate.Chess.Game
{
    public class ChessGameController : BaseGameController
    {
        protected override void FrameworkControllerInit(FrameworkControllerConfig config)
        {
            config.Override<ChessSceneManageController, SceneManageController>();
        }

        protected override void GameControllerInit()
        {
            RegisterController<RequestController>();
            RegisterController<ProgressController>();
        }

    }
}