using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using TwitchLib.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace Twitch
{
    public class TwitchClient : MonoBehaviour
    {
        private Client _client;

        [SerializeField] private TwitchConfig config;

        [SerializeField] private UnityEvent<string> commandReceived;
        
        private void Start()
        {
            var credentials = new ConnectionCredentials(config.channel, config.token);
            
            _client = new Client();
            _client.Initialize(credentials, config.channel);
            
            _client.OnConnected += OnConnected;
            _client.OnJoinedChannel += OnJoinedChannel;
            _client.OnChatCommandReceived += OnChatCommandReceived;
            _client.OnUserJoined += OnUserJoined;
            _client.OnError += OnError;

            _client.Connect();
        }

        private void OnConnected(object sender, OnConnectedArgs e)
        {
            Debug.Log($"OnConnected: {e.BotUsername}");
        }

        private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Debug.Log($"OnJoinedChannel: {e.Channel}");
        }

        private void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            Debug.Log($"OnChatCommandReceived: {e.Command.CommandText}");
            
            commandReceived?.Invoke(e.Command.CommandText);
        }

        private void OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            Debug.Log($"OnUserJoined: {e.Username}");
        }

        private void OnError(object sender, OnErrorEventArgs e)
        {
            Debug.Log($"OnError: {e.Exception}");
        }
    }
}