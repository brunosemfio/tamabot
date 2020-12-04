using DG.Tweening;
using Events;
using UnityEngine;

namespace Tamabot
{
    [RequireComponent(typeof(StatsManager))]
    public class PetBehaviour : MonoBehaviour
    {
        #region Private

        private StatsManager _stats;

        #endregion

        #region Inspector

        [SerializeField] private ConfigPreset overweight;
        
        [SerializeField] private ParticleSystem eatEffect;

        #endregion

        private void Awake()
        {
            _stats = GetComponent<StatsManager>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(Grow), 0f, .1f);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out Eatable food)) return;
            
            Eat(food);
            
            eatEffect.Emit(1);
        }

        private void Grow()
        {
            transform.DOScale(Vector3.one * Mathf.Sqrt(_stats.Size), 1f);

            if (_stats.Size <= overweight.value) return;

            SpawnPet.Instance.Multiply(transform.position);

            gameObject.SetActive(false);
        }

        private void Eat(Eatable food)
        {
            _stats.DecreaseHunger(food.RecoveryAmount);

            food.gameObject.SetActive(false);
        }
    }
}