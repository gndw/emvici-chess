using System;
using Agate.Chess.Prefab.Utility;
using Agate.Chess.SceneManage.Model;
using Agate.Chess.SceneManage.Utility;
using Agate.Chess.SceneManage.View;
using Agate.MVC.Core;

namespace Agate.Chess.SceneManage.Controller
{
    public class ChessSceneManageController : SceneManageController
    {
        [Inject]
        protected PrefabController _prefabController { get; set; }

        private SceneManageModel _model;
        private SceneManageView _view;

        public override void Init(Action onInitialized)
        {
            _model = new SceneManageModel(SceneLoadingType.Initial);
            _view = _prefabController.GetUIObject<SceneManageView>(PrefabConstant.PathSceneManageView, _baseGameController.Canvas);
            _view.Set(_model);
            _view.Activate();

            _baseGameController.OnInitialLoadingStart += () => _model.SetProgress(0);
            _baseGameController.OnInitialLoadingProgress += (prog) => _model.SetProgress(prog);
            _baseGameController.OnInitialLoadingFinished += () =>
            {
                _model.SetProgress(1);
                // _view.Deactivate();
            };

            base.Init(onInitialized);
        }

        protected override void SetupSceneLoading()
        {
            Function start = () =>
            {
                _model.SetType(SceneLoadingType.Scene, 0);
                _view.Activate();
            };
            ProgressFunction progress = (prog) => _model.SetProgress(prog);
            Function finish = null;
            finish = () =>
            {
                OnSceneLoadingStart -= start;
                OnSceneLoadingProgress -= progress;
                OnSceneLoadingFinished -= finish;
                _view.Deactivate();
            };

            OnSceneLoadingStart += start;
            OnSceneLoadingProgress += progress;
            OnSceneLoadingFinished += finish;
        }
    }
}