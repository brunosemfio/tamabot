using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tamabot
{
    public class SpawnFood : MonoBehaviour
    {
        #region Private

        private Camera _cam;

        #endregion

        #region Inspector

        [SerializeField] private Pool pool;

        [SerializeField] private ConfigPreset margin;

        [SerializeField] private bool rain;

        #endregion

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Start()
        {
            if (rain) InvokeRepeating(nameof(Spawn), 0f, .2f);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            var height = _cam.orthographicSize;

            var width = height * _cam.aspect;

            var position = new Vector2(Random.Range(-width + margin.value, width - margin.value), height + margin.value);

            var food = pool.Get();
            food.transform.position = position;
        }
    }
}