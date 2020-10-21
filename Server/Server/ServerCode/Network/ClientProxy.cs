using System.Collections.Generic;

namespace Server.Network
{
    public class ClientProxy
    {
        public int IdClient { get; }
        public readonly Queue<byte> UnprocessedCommands = new Queue<byte>();
        public readonly Queue<byte> NotSentPacketCommands = new Queue<byte>();
        
        public ClientProxy(int idClient)
        {
            IdClient = idClient;
        }
    }
}