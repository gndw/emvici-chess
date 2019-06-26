using Agate.Chess.Game;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Chessman.View
{
    public abstract class ChessmanView : SelectableView<MeshCollider>
    {
        public event Function OnChessmanSelected;

        protected override void OnColliderSelected(RaycastHit hit)
        {
            OnChessmanSelected?.Invoke();
        }
    }
}