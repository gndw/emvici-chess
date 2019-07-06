using Agate.Chess.SceneManage.Utility;
using Agate.MVC.Core;

namespace Agate.Chess.SceneManage.Model
{
    public class SceneManageModel : BaseUIModel, ISceneManageModel
    {
        public float LoadingProgress { get; private set; }
        public SceneLoadingType LoadingType { get; private set; }

        public SceneManageModel(SceneLoadingType loadingType)
        {
            LoadingType = loadingType;
            LoadingProgress = 0;
        }

        public void SetType(SceneLoadingType loadingType, float loadingProgress)
        {
            LoadingType = loadingType;
            LoadingProgress = loadingProgress;
            IsDirty = true;
        }

        public void SetProgress(float progressValue)
        {
            LoadingProgress = progressValue;
            IsDirty = true;
        }
    }
}