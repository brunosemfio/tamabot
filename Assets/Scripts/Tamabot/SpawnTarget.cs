using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tamabot
{
    public class SpawnTarget : MonoBehaviour
    {
        #region Private

        private Camera _cam;

        #endregion
        
        #region Inspector

        [SerializeField] private Pool pool;

        [SerializeField] private ConfigPreset margin;

        [SerializeField] private ConfigPreset rate;
        
        #endregion

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Start()
        {
            InvokeRepeating(nameof(Spawn), 0f, 1 / rate.value);
        }

        private void Spawn()
        {
            var height = _cam.orthographicSize;

            var width = height * _cam.aspect;

            var position = new Vector2(Random.Range(-width + margin.value, width - margin.value), -height);

            var target = pool.Get();
            target.transform.position = position;
        }
    }
}