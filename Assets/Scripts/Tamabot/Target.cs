using System.Collections.Generic;
using UnityEngine;

namespace Tamabot
{
    public class Target : MonoBehaviour
    {
        private static readonly List<Target> All = new List<Target>();

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

            for (var i = All.Count - 1; i >= 0; i--)
            {
                var distance = Vector2.Distance(All[i].transform.position, position);
            
                if (distance < closest)
                {
                    closest = distance; 
                
                    target = All[i];
                }
            }

            return target;
        }
    }
}