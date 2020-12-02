using System.Collections.Generic;
using UnityEngine;

namespace Tamabot
{
    public class Target : MonoBehaviour
    {
        #region Private

        private static readonly List<Target> All = new List<Target>();

        #endregion

        private void OnEnable()
        {
            All.Add(this);
        }

        private void OnDisable()
        {
            All.Remove(this);
        }

        public static Target Closest(MoveToTarget pet)
        {
            var closest = float.MaxValue;

            Target target = null;

            for (var i = All.Count - 1; i >= 0; i--)
            {
                var item = All[i];

                var distance = Vector2.Distance(item.transform.position, pet.transform.position);

                if (distance < closest)
                {
                    closest = distance;

                    target = item;
                }
            }

            return target;
        }

        public static Target Rand()
        {
            return All.Count > 0 ? All[Random.Range(0, All.Count)] : null;
        }
    }
}