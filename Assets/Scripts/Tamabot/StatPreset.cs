using UnityEngine;

namespace Tamabot
{
    [CreateAssetMenu(menuName = "Tamabot/Stat")]
    public class StatPreset : ScriptableObject
    {
        public float maxValue;

        public float increaseInterval;

        public float increaseAmount;

        public float increaseRate;
    }
}