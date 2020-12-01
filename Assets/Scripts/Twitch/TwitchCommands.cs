using System;
using Events;
using UnityEngine;

namespace Twitch
{
    public class TwitchCommands : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private TwitchCommand[] commands;

        #endregion

        public void Check(string commandText)
        {
            foreach (var command in commands)
            {
                if (command.text == commandText)
                {
                    command.gameEvent.Raise();
                }
            }
        }
    }

    [Serializable]
    public struct TwitchCommand
    {
        public string text;
        public GameEvent gameEvent;
    }
}