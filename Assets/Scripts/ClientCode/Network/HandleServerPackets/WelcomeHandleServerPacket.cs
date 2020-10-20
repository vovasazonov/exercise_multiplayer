using System.Collections.Generic;
using Serialization;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Network.HandlePackets
{
    public readonly struct WelcomeHandleServerPacket : IHandleServerPacket
    {
        private readonly Queue<byte> _packetCame;
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private readonly ISerializer _serializer;

        public WelcomeHandleServerPacket(Queue<byte> packetCame, ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            _packetCame = packetCame;
            _clientNetworkInfo = clientNetworkInfo;
            _serializer = serializer;
        }
        
        public void HandlePacket()
        {
            _clientNetworkInfo.Id = _serializer.Deserialize<int>(_packetCame);
                
#if UNITY_EDITOR
            Debug.Log($"Was welcomed as client id: {_clientNetworkInfo.Id}");
#endif
        }
    }
}