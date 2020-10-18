using System.Collections.Generic;

namespace Server.Network
{
    public class ClientProxy
    {
        public int IdClient { get; }
        public readonly Queue<byte> Packet = new Queue<byte>();
        
        public ClientProxy(int idClient)
        {
            IdClient = idClient;
        }
    }
}