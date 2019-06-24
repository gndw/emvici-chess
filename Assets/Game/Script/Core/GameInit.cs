using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Game
{
    public class GameInit : MonoBehaviour
    {
        public void Start ()
        {
            GameStart();
        }

        private void GameStart ()
        {
            SceneController scenecontroller = FindObjectOfType<SceneController>();
            scenecontroller.Load();
        }
    }
}