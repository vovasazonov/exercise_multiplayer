﻿using System.Collections.Generic;

namespace Server.Network
{
    public class ClientProxy : IClientProxy
    {
        public int IdClient { get; }
        public Queue<byte> UnprocessedCommands { get; } = new Queue<byte>();
        public Queue<byte> NotSentPacketCommands { get; } = new Queue<byte>();
        
        public ClientProxy(int idClient)
        {
            IdClient = idClient;
        }
    }
}