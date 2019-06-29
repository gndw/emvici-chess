using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agate.MVC.Core
{
    public abstract class BaseGameController : MonoBehaviour
    {
        private Dictionary<Type,IGlobalController> _controllers = new Dictionary<Type, IGlobalController>();

        private void Awake ()
        {
            DontDestroyOnLoad(this);
        }

        private void Start ()
        {
            GameControllerInit(GameStart);
        }

        protected abstract void GameControllerInit (Action onInitializeCompleted);
        protected abstract void GameStart ();

        protected void RegisterController<T>() where T : GlobalController<T>, new()
        {
            T controller = new T();
            controller.Init();
            _controllers.Add(typeof(T), controller);
        }

        protected void StartSceneControllerInThisScene ()
        {
            SceneController scenecontroller = FindObjectOfType<SceneController>();
            if (scenecontroller != null)
            {
                scenecontroller.InjectControllers(_controllers);
                scenecontroller.Load();
            }
        }
    }
}