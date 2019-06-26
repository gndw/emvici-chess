using Agate.Chess.Prefab.Controller;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Game
{
    public class GameInit : MonoBehaviour
    {
        public void Start ()
        {
            PrefabController.Instance.Init();
            GameStart();
        }

        private void GameStart ()
        {
            SceneController scenecontroller = FindObjectOfType<SceneController>();
            scenecontroller.Load();
        }
    }
}