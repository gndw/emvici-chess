using Agate.MVC.Core;
using UnityEngine.UI;
using UnityEngine;
using Agate.Chess.SceneManage.Model;
using Agate.Chess.SceneManage.Utility;

namespace Agate.Chess.SceneManage.View
{
    public class SceneManageView : BaseUIView<ISceneManageModel>
    {
        [SerializeField]
        private Text _txtLoading = null;

        protected override void UpdateView()
        {
            switch (_model.LoadingType)
            {
                case SceneLoadingType.Initial:
                    _txtLoading.text = "Initial Loading " + _model.LoadingProgress * 100 + " %";
                    break;
                case SceneLoadingType.Scene:
                    _txtLoading.text = "Scene Loading " + _model.LoadingProgress * 100 + " %";
                    break;
                default:
                    Deactivate();
                    break;
            }
        }
    }
}