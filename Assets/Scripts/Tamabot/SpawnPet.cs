using UnityEngine;

namespace Tamabot
{
    public class SpawnPet : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Pool pool;

        [SerializeField] private ConfigPreset maxChildren;
        
        [SerializeField] private ConfigPreset multiplyForce;

        #endregion

        public static SpawnPet Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            pool.Get();
        }

        public void Multiply(Vector3 position)
        {
            for (var i = 0; i < maxChildren.value; i++)
            {
                var direction = Quaternion.Euler(0, 0, Random.Range(-45f, 45f)) * Vector2.up;

                var child = pool.Get();
                child.transform.position = position;
                child.transform.localScale = Vector3.one;
                child.GetComponent<StatsManager>().ResetStats();
                child.GetComponent<Rigidbody2D>().AddForce(direction * multiplyForce.value);
            }
        }
    }
}