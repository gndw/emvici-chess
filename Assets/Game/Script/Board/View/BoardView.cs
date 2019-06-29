using System.Collections.Generic;
using UnityEngine;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Game;
namespace Agate.Chess.Board.View
{
    public class BoardView : SelectableView<BoxCollider>
    {
        public event BoardFunction OnBoardSelected;
        [SerializeField]
        private GameObject _boardHighlightPrefab = null;
        private List<GameObject> _createdBoardHighlights = new List<GameObject>();
        private List<GameObject> _activeBoardHighlights = new List<GameObject>();
        protected override void OnColliderSelected(RaycastHit hit)
        {
            Vector3 preciseLocation = hit.point - _collider.transform.position + _collider.size / 2;
            Vector3 normalizedPreciseLocation = new Vector3(preciseLocation.x / _collider.size.x, 0, preciseLocation.z / _collider.size.z);
            int x = Mathf.Clamp(Mathf.FloorToInt(normalizedPreciseLocation.x * 8) + 1, 1, 8);
            int y = Mathf.Clamp(Mathf.FloorToInt(normalizedPreciseLocation.z * 8) + 1, 1, 8);
            OnBoardSelected?.Invoke(new BoardCoord(x, y));
        }
        public Vector3 GetBoardPosition(BoardCoord coord)
        {
            return new Vector3(
                _collider.transform.position.x + (((float)coord.X - 0.5f) * _collider.size.x / 8) - _collider.size.x / 2,
                0,
                _collider.transform.position.z + (((float)coord.Y - 0.5f) * _collider.size.z / 8) - _collider.size.z / 2
            );
        }
        public Vector3 GetFacingDirection(ChessmanColorType colorType)
        {
            switch (colorType)
            {
                case ChessmanColorType.Light:
                    return GetBoardPosition(new BoardCoord(1, 8)) - GetBoardPosition(new BoardCoord(1, 1));
                case ChessmanColorType.Dark:
                    return GetBoardPosition(new BoardCoord(1, 1)) - GetBoardPosition(new BoardCoord(1, 8));
                default:
                    return Vector3.zero;
            }
        }
        public void SetHighlight(List<BoardCoord> coordinates)
        {
            _activeBoardHighlights.ForEach((obj) => obj.SetActive(false));
            _activeBoardHighlights.Clear();
            int createCount = coordinates.Count - _createdBoardHighlights.Count;
            for (int i = 0; i < createCount; i++)
            {
                GameObject obj = Instantiate(_boardHighlightPrefab, transform);
                obj.SetActive(false);
                _createdBoardHighlights.Add(obj);
            }
            for (int i = 0; i < coordinates.Count; i++)
            {
                Vector3 pos = GetBoardPosition(coordinates[i]);
                _createdBoardHighlights[i].transform.localPosition = new Vector3(pos.x, 0.05f, pos.z);
                _createdBoardHighlights[i].SetActive(true);
                _activeBoardHighlights.Add(_createdBoardHighlights[i]);
            }
        }
    }
}