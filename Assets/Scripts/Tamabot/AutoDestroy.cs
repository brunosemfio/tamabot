using UnityEngine;

namespace Tamabot
{
    public class AutoDestroy : MonoBehaviour
    {
        #region Private

        private Camera _cam;

        #endregion

        #region Inspector

        [SerializeField] private ConfigPreset lifeTime;

        #endregion

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            if (lifeTime.value > 0) Invoke(nameof(Disable), lifeTime.value);
        }

        private void FixedUpdate()
        {
            if (transform.position.y + transform.localScale.y < -_cam.orthographicSize)
            {
                Disable();
            }
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}