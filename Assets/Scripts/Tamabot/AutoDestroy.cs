using UnityEngine;

namespace Tamabot
{
    public class AutoDestroy : MonoBehaviour
    {
        #region Private

        private Camera _cam;

        #endregion

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void FixedUpdate()
        {
            if (transform.position.y + transform.localScale.y < -_cam.orthographicSize)
            {
                gameObject.SetActive(false);
            }
        }
    }
}