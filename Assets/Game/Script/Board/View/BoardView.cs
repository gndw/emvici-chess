using Agate.Chess.Board.Utility;
using Agate.Chess.Game;
using UnityEngine;

namespace Agate.Chess.Board.View
{
    public class BoardView : SelectableView<BoxCollider>
    {
        public event BoardFunction OnBoardSelected;

        protected override void OnColliderSelected(RaycastHit hit)
        {
            Vector3 preciseLocation = hit.point - _collider.transform.position + _collider.size / 2;
            Vector3 normalizedPreciseLocation = new Vector3 (preciseLocation.x / _collider.size.x, 0, preciseLocation.z / _collider.size.z);
            
            int x = Mathf.Clamp(Mathf.FloorToInt(normalizedPreciseLocation.x * 8) + 1, 1, 8);
            int y = Mathf.Clamp(Mathf.FloorToInt(normalizedPreciseLocation.z * 8) + 1, 1, 8);

            OnBoardSelected?.Invoke(new BoardCoord(x,y));
        }

        public Vector3 GetBoardPosition (BoardCoord coord)
        {
            return new Vector3(
                _collider.transform.position.x + (((float)coord.X - 0.5f) * _collider.size.x / 8) - _collider.size.x / 2,
                0,
                _collider.transform.position.z + (((float)coord.Y - 0.5f) * _collider.size.z / 8) - _collider.size.z / 2
            ) ;
        }
    }
}