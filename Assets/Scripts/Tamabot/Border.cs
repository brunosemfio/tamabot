using UnityEngine;

namespace Tamabot
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Border : MonoBehaviour
    {
        #region Private

        private Camera _cam;

        private EdgeCollider2D _edge;

        private Vector2[] _edgePoints;

        #endregion

        private void Awake()
        {
            _cam = Camera.main;
        
            _edge = GetComponent<EdgeCollider2D>();
        
            _edgePoints = new Vector2[4];
        }

        private void Start()
        {
            var bottomLeft = _cam.ViewportToWorldPoint(new Vector3(0f, 0f, _cam.nearClipPlane));
            var topRight = _cam.ViewportToWorldPoint(new Vector3(1f, 1f, _cam.nearClipPlane));
            var topLeft = new Vector2(bottomLeft.x, topRight.y);
            var bottomRight = new Vector2(topRight.x, bottomLeft.y);
        
            _edgePoints[0] = topRight;
            _edgePoints[1] = bottomRight;
            _edgePoints[2] = bottomLeft;
            _edgePoints[3] = topLeft;

            _edge.points = _edgePoints;
        }
    }
}
