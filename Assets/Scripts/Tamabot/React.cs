using DG.Tweening;
using UnityEngine;

namespace Tamabot
{
    public class React : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private SpriteRenderer sprite;

        #endregion

        private void Start()
        {
            sprite.enabled = false;
        }

        public void Play()
        {
            sprite.transform.DORewind();
            sprite.transform.DOScale(1.5f, .2f).SetLoops(4, LoopType.Yoyo)
                .OnStart(() => sprite.enabled = true)
                .OnComplete(() => sprite.enabled = false);
        }
    }
}