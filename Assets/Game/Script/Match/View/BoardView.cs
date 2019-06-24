using Agate.Chess.Match.Utility;
using Agate.Chess.Match.View;
using UnityEngine;

namespace Agate.Chess.Match.Board.View
{
    public class BoardView : SelectableView<BoxCollider>
    {
        public event BoardFunction OnBoardSelected;

        protected override void OnColliderSelected(RaycastHit hit)
        {
            Vector3 preciseLocation = hit.point - _collider.center - _collider.transform.position + _collider.size / 2;
            Vector3 normalizedPreciseLocation = new Vector3 (preciseLocation.x / _collider.size.x, 0, preciseLocation.z / _collider.size.z);
            
            int x = Mathf.Clamp(Mathf.FloorToInt(normalizedPreciseLocation.x * 8) + 1, 1, 8);
            int y = Mathf.Clamp(Mathf.FloorToInt(normalizedPreciseLocation.z * 8) + 1, 1, 8);

            OnBoardSelected?.Invoke(new BoardCoord(x,y));
        }
    }
}