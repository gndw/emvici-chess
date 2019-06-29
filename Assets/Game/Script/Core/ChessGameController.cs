using Agate.Chess.Prefab.Controller;
using Agate.Chess.Progress.Controller;
using Agate.Chess.Request.Controller;
using Agate.Chess.Schedule.Controller;
using Agate.MVC.Core;
namespace Agate.Chess.Game
{
    public class ChessGameController : BaseGameController
    {
        protected override void GameControllerInit()
        {
            RegisterController<ScheduleController>();
            RegisterController<PrefabController>();
            RegisterController<RequestController>();
            RegisterController<ProgressController>();
        }
        protected override void GameStart()
        {
            StartSceneControllerInThisScene();
        }
    }
}