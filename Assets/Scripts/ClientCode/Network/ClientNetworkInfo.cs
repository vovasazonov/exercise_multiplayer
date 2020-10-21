using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Network
{
    public class ClientNetworkInfo : IClientNetworkInfo
    {
        private int? _id = null;

        public ClientNetworkState ClientNetworkState { get; set; } = ClientNetworkState.SayingHello;
        public Queue<byte> NotSentCommandsToServer { get; } = new Queue<byte>();

        public int Id
        {
            get => _id.Value;
            set
            {
                if (_id == null)
                {
                    _id = value;
                                    
#if UNITY_EDITOR
                    Debug.Log($"Was welcomed as client id: {Id}");
#endif
                }
            }
        }
    }
}