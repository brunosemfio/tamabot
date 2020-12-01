using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        #region Inspector

        [SerializeField] private GameEvent gameEvent;
        
        [SerializeField] private UnityEvent response;
        
        #endregion
        
        private void OnEnable()
        {
            gameEvent.Register(this);
        }

        private void OnDisable()
        {
            gameEvent.Unregister(this);
        }

        public void Invoke()
        {
            response?.Invoke();
        }
    }
}