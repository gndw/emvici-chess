using System;
using Agate.Chess.Prefab.Controller;
using Agate.MVC.Core;
namespace Agate.Chess.Game
{
    public class ChessGameController : BaseGameController
    {
        protected override void GameControllerInit(Action onInitializeCompleted)
        {
            RegisterController<PrefabController>();
            onInitializeCompleted();
        }
        protected override void GameStart()
        {
            StartSceneControllerInThisScene();
        }
    }
}