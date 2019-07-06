using UnityEngine;

namespace Agate.MVC.Core
{
    public class BaseView : MonoBehaviour
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}