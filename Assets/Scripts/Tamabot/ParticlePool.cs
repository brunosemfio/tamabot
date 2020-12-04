using UnityEngine;

namespace Tamabot
{
    public class ParticlePool : MonoBehaviour
    {
        #region Private

        private ParticleSystem[] _particles;

        private int _index;

        #endregion

        #region Inspector

        [SerializeField] private ParticleSystem prefab;

        [SerializeField] private int amount;

        #endregion

        private void Start()
        {
            _particles = new ParticleSystem[amount];
            
            for (var i = 0; i < amount; i++)
            {
                _particles[i] = Instantiate(prefab);
            }
        }

        public void Play(Vector3 position)
        {
            var particle = _particles[_index++ % amount];
            particle.transform.position = position;
            
            particle.Emit(1);
        }
    }
}