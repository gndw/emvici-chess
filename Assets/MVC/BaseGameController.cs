using System;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public abstract class BaseGameController : BaseMonoBehaviourController, IGlobalController
    {
        private Dictionary<Type, IGlobalController> _initializedControllers = new Dictionary<Type, IGlobalController>();
        private Sequence _controllerInitializeProcess = new Sequence();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Init(() =>
            {
                _initializedControllers.Add(typeof(BaseGameController), this);
                GameControllerInit();
                _controllerInitializeProcess.OnFinishSequence += GameStart;
                _controllerInitializeProcess.Execute();
            });
        }

        public void Init(Action onInitialized)
        {
            onInitialized();
        }

        protected abstract void GameControllerInit();
        protected abstract void GameStart();

        protected void RegisterController<T>() where T : GlobalController<T>, new()
        {
            T controller = new T();
            _controllerInitializeProcess.AddSequence((onInitialized) =>
            {
                controller.InjectControllers(_initializedControllers);
                controller.Init(() =>
                {
                    _initializedControllers.Add(typeof(T), controller);
                    onInitialized();
                });
            });
        }

        protected void StartSceneControllerInThisScene()
        {
            SceneController scenecontroller = FindObjectOfType<SceneController>();
            if (scenecontroller != null)
            {
                scenecontroller.InjectControllers(_initializedControllers);
                scenecontroller.Load();
            }
        }
    }
}