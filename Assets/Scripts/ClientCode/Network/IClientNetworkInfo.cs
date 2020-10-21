using System.Collections.Generic;

namespace Network
{
    public interface IClientNetworkInfo
    {
        public ClientNetworkState ClientNetworkState { get; set; }
        public Queue<byte> NotSentCommandsToServer { get; }
        public int Id { get; set; }
    }
}