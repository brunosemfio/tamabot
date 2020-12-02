using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace Twitch
{
    public class TwitchClient : MonoBehaviour
    {
        #region Private

        private Client _client;

        private PubSub _pubSub;

        #endregion

        #region Inspector

        [SerializeField] private TwitchConfig config;

        [SerializeField] private UnityEvent<string> commandReceived;

        #endregion

        private void Start()
        {
            InitClient();
        }

        private void InitClient()
        {
            var credentials = new ConnectionCredentials(config.channel, config.token);

            _client = new Client();
            _client.Initialize(credentials, config.channel);

            _client.OnConnected += (sender, args) => Debug.Log($"OnConnected: {args.BotUsername}");
            _client.OnError += (sender, args) => Debug.Log($"OnError: {args.Exception}");
            _client.OnChatCommandReceived += OnChatCommandReceived;

            _client.Connect();
        }

        private void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            Debug.Log($"OnChatCommandReceived: {e.Command.CommandText}");

            commandReceived?.Invoke(e.Command.CommandText);
        }
    }
}