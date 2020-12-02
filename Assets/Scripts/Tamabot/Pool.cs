using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tamabot
{
    public class Pool : MonoBehaviour
    {
        #region Private

        private List<GameObject> _list; 

        #endregion

        #region Inspector
        
        [SerializeField] private GameObject prefab;

        [SerializeField] private int amount;

        #endregion

        private void Awake()
        {
            _list = new List<GameObject>();
        }

        private void Start()
        {
            for (var i = 0; i < amount; i++)
            {
                Create();
            }
        }

        public GameObject Get()
        {
            foreach (var item in _list.Where(item => !item.activeInHierarchy))
            {
                item.SetActive(true);
                    
                return item;
            }

            var newItem = Create();
            newItem.SetActive(true);

            return newItem;
        }

        private GameObject Create()
        {
            var item = Instantiate(prefab, transform.position, Quaternion.identity);
            item.SetActive(false);
                
            _list.Add(item);

            return item;
        }
    }
}