using Agate.Chess.Menu.Model;
using Agate.Chess.Menu.View;
using Agate.Chess.Prefab.Utility;
using Agate.Chess.Progress.Controller;
using Agate.Chess.SceneManage.Utility;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Menu.Controller
{
    public class MenuController : SceneController
    {
        [Inject]
        private SceneManageController _sceneManageController { get; set; }
        [Inject]
        private ProgressController _progressController { get; set; }
        [Inject]
        private PrefabController _prefabController { get; set; }

        private RectTransform _canvas;
        private MenuModel _model;
        private MenuView _view;

        public override void StartScene()
        {

        }

        protected override void Load(Sequence createLoadingSequence)
        {
            createLoadingSequence.AddSequence((next) =>
            {
                _canvas = GetComponentInChildren<Canvas>().gameObject.GetComponent<RectTransform>();

                _model = new MenuModel();
                _model.SetPoint(_progressController.SavedNumber);
                _view = _prefabController.GetUIObject<MenuView>(PrefabConstant.PathMenuView, _canvas);
                _view.Init(StartPlayerVsPlayer);
                _view.Set(_model);
                _view.Activate();

                next();
            });
        }

        private void StartPlayerVsPlayer()
        {
            _sceneManageController.LoadScene((int)SceneState.Match);
        }
    }
}