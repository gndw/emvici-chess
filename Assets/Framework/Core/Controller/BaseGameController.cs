using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        private FrameworkControllerConfig _config = new FrameworkControllerConfig();
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
            Canvas canObj = GetComponentInChildren<Canvas>();
            if (canObj != null) Canvas = canObj.GetComponent<RectTransform>();

            OnInitialLoadingStart?.Invoke();
            _controllerInitializeProcess.OnProgressSequence += (prog) => OnInitialLoadingProgress?.Invoke(prog);
            _controllerInitializeProcess.OnFinishSequence += () => OnInitialLoadingFinished?.Invoke();

            Init(() =>
            {
                _initializedControllers.Add(typeof(BaseGameController), this);

                FrameworkControllerInit(_config);
                List<Type> types = _config.ControllerTypes;
                types.ForEach((ty) =>
                {
                    MethodInfo generic;
                    if (_config.IsOverrideType(ty))
                    {
                        MethodInfo mi = this.GetType().GetMethod(nameof(RegisterOverrideController), BindingFlags.NonPublic | BindingFlags.Instance);
                        Type originTy = _config.GetOriginType(ty);
                        generic = mi.MakeGenericMethod(ty, originTy);
                    }
                    else
                    {
                        MethodInfo mi = this.GetType().GetMethod(nameof(RegisterController), BindingFlags.NonPublic | BindingFlags.Instance);
                        generic = mi.MakeGenericMethod(ty);
                    }
                    generic.Invoke(this, null);
                });

                GameControllerInit();
                _controllerInitializeProcess.SetSeparatorSequence((next) => StartCoroutine(IE_WaitNextFrame(2, next)));
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

        protected virtual void FrameworkControllerInit(FrameworkControllerConfig config) { }

        protected virtual void GameStart()
        {
            SceneManageController sceneManageController = GetController<SceneManageController>();
            sceneManageController.LoadScene();
        }

        protected void RegisterOverrideController<T, U>() where T : U, new() where U : GlobalController<U>, new()
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

        protected T GetController<T>() where T : IGlobalController, new()
        {
            IGlobalController igc;
            if (_initializedControllers.TryGetValue(typeof(T), out igc))
            {
                return (T)igc;
            }
            else
            {
                foreach (var item in _initializedControllers)
                {
                    if (item.Key.IsSubclassOf(typeof(T))) return (T)item.Value;
                }
                return default(T);
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
            yield return IE_WaitNextFrame(1, onFinish);
        }

        private IEnumerator IE_WaitNextFrame(int frameCount, Action onFinish)
        {
            for (int i = 0; i < frameCount; i++)
            {
                yield return null;
            }
            onFinish();
        }

        private IEnumerator IE_WaitForSeconds(float time, Action onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish();
        }

        protected class FrameworkControllerConfig
        {
            public List<Type> ControllerTypes { get { return _controllerTypes; } }

            private List<Type> _controllerTypes = new List<Type>();
            private List<ChangedType> _changedTypes = new List<ChangedType>();

            private class ChangedType
            {
                public Type OriginType;
                public Type ReplacedType;
            }

            public FrameworkControllerConfig()
            {
                _controllerTypes.Add(typeof(ScheduleController));
                _controllerTypes.Add(typeof(PrefabController));
                _controllerTypes.Add(typeof(SceneManageController));
            }

            public void Override<T, U>() where T : U where U : GlobalController<U>, new()
            {
                int index = _controllerTypes.FindIndex(0, _controllerTypes.Count, (ty) => ty == typeof(U));
                if (index >= 0)
                {
                    _controllerTypes[index] = typeof(T);
                    _changedTypes.Add(new ChangedType() { OriginType = typeof(U), ReplacedType = typeof(T) });
                }
            }

            public Type GetOverridedType(Type type)
            {
                ChangedType c = _changedTypes.Find((typ) => typ.OriginType == type);
                if (c != null)
                    return c.ReplacedType;
                else
                    return null;
            }

            public Type GetOriginType(Type type)
            {
                ChangedType c = _changedTypes.Find((typ) => typ.ReplacedType == type);
                if (c != null)
                    return c.OriginType;
                else
                    return null;
            }

            public bool IsOverrideType(Type type)
            {
                ChangedType c = _changedTypes.Find((typ) => typ.ReplacedType == type || typ.OriginType == type);
                return c != null;
            }
        }
    }
}