using UnityEngine;

namespace Tamabot
{
    public class StatsManager : MonoBehaviour
    {
        #region Private

        private float _hunger;

        private float _weight;

        #endregion

        #region Inspector

        [SerializeField] private StatPreset hungerStat;

        [SerializeField] private StatPreset weightStat;

        #endregion

        #region Public

        public float Size => _weight >= 1f ? _weight : 1f;

        #endregion

        private void Update()
        {
            UpdateStat(ref _hunger, hungerStat);
            UpdateStat(ref _weight, weightStat);
        }

        public void DecreaseHunger(float amount)
        {
            var over = amount - _hunger;

            _hunger -= amount;

            if (over > 0)
            {
                UpdateStat(ref _weight, over, weightStat);

                _hunger = 0;
            }
        }

        public void ResetStats()
        {
            _hunger = 0f;
            
            _weight = 0f;
        }

        private void UpdateStat(ref float stat, StatPreset preset)
        {
            stat += preset.increaseAmount * (1 / preset.increaseInterval) * Time.deltaTime;

            stat = Mathf.Clamp(stat, 0f, preset.maxValue);
        }

        private void UpdateStat(ref float stat, float amount, StatPreset preset)
        {
            stat += preset.increaseRate * amount;

            stat = Mathf.Clamp(stat, 0f, preset.maxValue);
        }
    }
}