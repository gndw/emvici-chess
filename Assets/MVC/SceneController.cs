namespace Agate.MVC.Core
{
    public abstract class SceneController : BaseMonoBehaviourController
    {
        public event Function OnSceneLoadingStart;
        public event ProgressFunction OnSceneLoadingProgress;
        public event Function OnSceneLoadingFinished;

        private Sequence _sceneLoadingProcess = new Sequence();

        public void Init()
        {
            OnSceneLoadingStart?.Invoke();
            Load(_sceneLoadingProcess);
            _sceneLoadingProcess.OnProgressSequence += OnSceneLoadingProgress;
            _sceneLoadingProcess.OnFinishSequence += OnSceneLoadingFinished;
            _sceneLoadingProcess.Execute();
        }

        protected abstract void Load(Sequence createLoadingSequence);
        public abstract void StartScene();
    }
}

