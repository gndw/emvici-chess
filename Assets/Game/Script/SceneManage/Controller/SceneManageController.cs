using System;
using System.Collections;
using Agate.Chess.Prefab.Controller;
using Agate.Chess.Prefab.Utility;
using Agate.Chess.SceneManage.Model;
using Agate.Chess.SceneManage.Utility;
using Agate.Chess.SceneManage.View;
using Agate.Chess.Schedule.Controller;
using Agate.MVC.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agate.Chess.SceneManage.Controller
{
    public class SceneManageController : GlobalController<SceneManageController>
    {
        public event Function OnSceneLoadingStart;
        public event ProgressFunction OnSceneLoadingProgress;
        public event Function OnSceneLoadingFinished;

        [Inject]
        private BaseGameController _baseGameController { get; set; }
        [Inject]
        private PrefabController _prefabController { get; set; }
        [Inject]
        private ScheduleController _scheduleController { get; set; }

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

            onInitialized();
        }

        public void LoadScene()
        {
            SetupSceneLoading();
            _scheduleController.StartCoroutine(IE_LoadScene(() => OnSceneLoadingStart?.Invoke(), (prog) => OnSceneLoadingProgress?.Invoke(prog), () => OnSceneLoadingFinished?.Invoke()));
        }

        public void LoadScene(int sceneBuildIndex)
        {
            SetupSceneLoading();
            _scheduleController.StartCoroutine(IE_LoadScene(sceneBuildIndex));
        }

        private void SetupSceneLoading()
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

        private IEnumerator IE_LoadScene(int sceneBuildIndex)
        {
            OnSceneLoadingStart?.Invoke();
            AsyncOperation asy = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
            while (!asy.isDone)
            {
                OnSceneLoadingProgress?.Invoke(asy.progress / 2);
                yield return null;
            }

            yield return IE_LoadScene(() => { }, (prog) => OnSceneLoadingProgress?.Invoke(0.5f + prog / 2), () => OnSceneLoadingFinished?.Invoke());
        }

        private IEnumerator IE_LoadScene(Function onStart, ProgressFunction onProgress, Function onFinish)
        {
            yield return null;
            onStart();
            SceneController scenecontroller = UnityEngine.Object.FindObjectOfType<SceneController>();
            if (scenecontroller != null)
            {
                ProgressFunction targetSceneLoading = (prog) => onProgress(0.5f + prog / 2);
                Function targetSceneFinishedLoading = null;
                targetSceneFinishedLoading = () =>
                {
                    scenecontroller.OnSceneLoadingProgress -= targetSceneLoading;
                    scenecontroller.OnSceneLoadingFinished -= targetSceneFinishedLoading;
                    onFinish();
                    scenecontroller.StartScene();
                };

                _baseGameController.InjectMonoBehaviourController(scenecontroller);
                scenecontroller.OnSceneLoadingProgress += targetSceneLoading;
                scenecontroller.OnSceneLoadingFinished += targetSceneFinishedLoading;
                scenecontroller.Init();
            }
        }
    }
}