using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agate.MVC.Core
{
    public abstract class BaseGameController : BaseMonoBehaviourController, IGlobalController
    {
        public bool Main { get; private set; }
        public bool Initialized { get; private set; }
        public RectTransform Canvas { get; private set; }

        public event Function OnInitialLoadingStart;
        public event ProgressFunction OnInitialLoadingProgress;
        public event Function OnInitialLoadingFinished;

        private Dictionary<Type, IGlobalController> _initializedControllers = new Dictionary<Type, IGlobalController>();
        private Sequence _controllerInitializeProcess = new Sequence();
        private Action _updates;

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
            Canvas = GetComponentInChildren<Canvas>().gameObject.GetComponent<RectTransform>();

            OnInitialLoadingStart?.Invoke();
            _controllerInitializeProcess.OnProgressSequence += (prog) => OnInitialLoadingProgress?.Invoke(prog);
            _controllerInitializeProcess.OnFinishSequence += () => OnInitialLoadingFinished?.Invoke();

            Init(() =>
            {
                _initializedControllers.Add(typeof(BaseGameController), this);
                GameControllerInit();
                _controllerInitializeProcess.SetSeparatorSequence((next) => StartCoroutine(IE_WaitNextFrame(next)));
                _controllerInitializeProcess.OnFinishSequence += () =>
                {
                    Initialized = true;
                    GameStart();
                };
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

        public void InjectMonoBehaviourController(BaseMonoBehaviourController controller)
        {
            controller.InjectControllers(_initializedControllers);
        }

        private void Update()
        {
            if (Main && Initialized) _updates();
        }

        public void RegisterUpdate(Action action)
        {
            _updates += action;
        }

        private IEnumerator IE_WaitNextFrame(Action onFinish)
        {
            yield return null;
            onFinish();
        }

        private IEnumerator IE_WaitForSeconds(float time, Action onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish();
        }
    }
}