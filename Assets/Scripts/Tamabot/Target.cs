using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tamabot
{
    public class Target : MonoBehaviour
    {
        #region Private

        private static readonly List<Target> All = new List<Target>();

        #endregion

        #region Inspector

        [SerializeField] private bool priority;

        #endregion

        private void OnEnable()
        {
            All.Add(this);
        }

        private void OnDisable()
        {
            All.Remove(this);
        }

        public static Target Closest(Vector3 position)
        {
            var closest = float.MaxValue;

            Target target = null;

            var priorities = All.Where(t => t.priority);
            
            foreach (var priority in priorities)
            {
                var distance = Vector2.Distance(priority.transform.position, position);

                if (distance > closest) continue;
                
                closest = distance;

                target = priority;
            }

            return target;
        }

        public static Target Rand()
        {
            return All.Count > 0 ? All[Random.Range(0, All.Count)] : null;
        }
    }
}