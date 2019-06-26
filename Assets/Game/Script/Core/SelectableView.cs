using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Game
{
    public abstract class SelectableView<T> : BaseView where T : Collider
    {
        protected T _collider;

        private void Awake ()
        {
            _collider = GetComponent<T>();
        }

        private void Update ()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == _collider)
                    {
                        OnColliderSelected(hit);
                    }
                }
            }
        }

        protected abstract void OnColliderSelected (RaycastHit hit);
    }
}