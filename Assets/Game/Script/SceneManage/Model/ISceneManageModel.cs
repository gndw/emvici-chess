using Agate.Chess.SceneManage.Utility;
using Agate.MVC.Core;

namespace Agate.Chess.SceneManage.Model
{
    public interface ISceneManageModel : IBaseUIModel
    {
        float LoadingProgress { get; }
        SceneLoadingType LoadingType { get; }
    }
}