using UnityEngine;

namespace Twitch
{
    [CreateAssetMenu(menuName = "Twitch")]
    public class TwitchConfig : ScriptableObject
    {
        public string token;
        public string channel;
    }
}