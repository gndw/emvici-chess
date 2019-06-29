using System;
using UnityEngine;
using Agate.Chess.Game;
using Agate.MVC.Core;
namespace Agate.Chess.Chessman.View
{
    public abstract class ChessmanView : SelectableView<MeshCollider>
    {
        public event Function OnChessmanSelected;
        protected override void OnColliderSelected(RaycastHit hit)
        {
            OnChessmanSelected?.Invoke();
        }
        internal void Move(Vector3 targetPosition, Action onFinish)
        {
            gameObject.transform.position = targetPosition;
            onFinish();
        }
    }
}