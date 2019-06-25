using Agate.Chess.Match.View;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Match.Chessman.View
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