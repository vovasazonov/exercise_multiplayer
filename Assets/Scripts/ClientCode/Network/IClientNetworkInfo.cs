using System.Collections.Generic;

namespace Network
{
    public interface IClientNetworkInfo
    {
        ClientNetworkState ClientNetworkState { get; set; }
        Queue<byte> NotSentCommandsToServer { get; }
        int Id { get; set; }
    }
}