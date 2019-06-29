using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agate.MVC.Core
{
    public abstract class BaseGameController : BaseMonoBehaviourController, IGlobalController
    {
        private Dictionary<Type, IGlobalController> _initializedControllers = new Dictionary<Type, IGlobalController>();
        private Sequence _controllerInitializeProcess = new Sequence();
        private Action _updates;

        public bool Main { get; private set; }

        private void Awake()
        {
            BaseGameController[] bgcs = FindObjectsOfType<BaseGameController>();
            int length = bgcs.Length;
            if (length == 1)
            {
                Main = true;
                DontDestroyOnLoad(this);
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    if (!bgcs[i].Main) Destroy(bgcs[i]);
                }
            }
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

        protected T GetController<T>() where T : GlobalController<T>, new()
        {
            IGlobalController igc;
            if (_initializedControllers.TryGetValue(typeof(T), out igc))
            {
                return igc as T;
            }
            else
            {
                return null;
            }
        }

        public void InjectSceneController(SceneController scenecontroller)
        {
            scenecontroller.InjectControllers(_initializedControllers);
        }

        private void Update()
        {
            _updates();
        }

        public void RegisterUpdate(Action action)
        {
            _updates += action;
        }
    }
}