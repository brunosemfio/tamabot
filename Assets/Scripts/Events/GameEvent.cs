using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private event UnityAction OnEvent;

        public void Raise()
        {
            OnEvent?.Invoke();
        }

        public void Register(IGameEventListener listener)
        {
            OnEvent += listener.Invoke;
        }

        public void Unregister(IGameEventListener listener)
        {
            OnEvent -= listener.Invoke;
        }
    }
}