using DG.Tweening;
using UnityEngine;

namespace Tamabot
{
    [RequireComponent(typeof(StatsManager))]
    public class PetBehaviour : MonoBehaviour
    {
        #region Private

        private StatsManager _stats;

        [SerializeField] private ConfigPreset overweight;

        #endregion

        private void Awake()
        {
            _stats = GetComponent<StatsManager>();
        }

        private void FixedUpdate()
        {
            Grow();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Eatable food))
            {
                Eat(food);
            }
        }

        private void Grow()
        {
            transform.DOScale(Vector3.one * Mathf.Sqrt(_stats.Size), 1f);
            
            if (_stats.Size > overweight.value)
            {
                SpawnPet.Instance.Multiply(transform.position);

                gameObject.SetActive(false);
            }
        }

        private void Eat(Eatable food)
        {
            _stats.DecreaseHunger(food.RecoveryAmount);

            food.gameObject.SetActive(false);
        }
    }
}