using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agate.MVC.Core
{
    public class SceneManageController : GlobalController<SceneManageController>
    {
        public event Function OnSceneLoadingStart;
        public event ProgressFunction OnSceneLoadingProgress;
        public event Function OnSceneLoadingFinished;

        [Inject]
        protected BaseGameController _baseGameController { get; set; }
        [Inject]
        protected ScheduleController _scheduleController { get; set; }

        public void LoadScene()
        {
            SetupSceneLoading();
            Debug.Log(_scheduleController);
            _scheduleController.StartCoroutine(IE_LoadScene(() => OnSceneLoadingStart?.Invoke(), (prog) => OnSceneLoadingProgress?.Invoke(prog), () => OnSceneLoadingFinished?.Invoke()));
        }

        public void LoadScene(int sceneBuildIndex)
        {
            SetupSceneLoading();
            _scheduleController.StartCoroutine(IE_LoadScene(sceneBuildIndex));
        }

        protected virtual void SetupSceneLoading() { }

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