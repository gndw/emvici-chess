using Agate.Chess.Match.Board.Controller;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Match.Controller
{
    public class MatchController : SceneController
    {
        [SerializeField]
        private BoardController _boardController;

        public override void Load()
        {
            _boardController.Init();

            
        }
    }
}