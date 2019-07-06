using System.Collections;
using UnityEngine;

namespace Agate.MVC.Core
{
    public abstract class ComponentController : BaseMonoBehaviourController
    {
        private bool _controllerInjected;
        private BaseGameController _bgc;

        private IEnumerator Start()
        {
            if (!_controllerInjected)
            {
                do
                {
                    BaseGameController[] bgcs = FindObjectsOfType<BaseGameController>();
                    if (bgcs != null)
                    {
                        for (int i = 0; i < bgcs.Length; i++)
                        {
                            if (bgcs[i].Main)
                            {
                                _bgc = bgcs[i];
                            }
                        }
                    }
                    yield return null;
                }
                while (_bgc == null);
            }

            yield return new WaitUntil(() => _bgc.Initialized);

            _bgc.InjectMonoBehaviourController(this);
            _controllerInjected = true;

            Init();
        }

        protected abstract void Init();
    }
}