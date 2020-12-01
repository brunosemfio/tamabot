using UnityEngine;

namespace Tamabot
{
    public class Eatable : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private FoodPreset food;

        #endregion

        #region Public

        public float RecoveryAmount => food.recoveryAmount;

        #endregion
    }
}