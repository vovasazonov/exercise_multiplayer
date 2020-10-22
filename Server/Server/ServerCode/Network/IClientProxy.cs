using System;
using System.Collections.Generic;

namespace Server.Network
{
    public interface IClientProxy
    {
        int IdClient { get; }
        Queue<byte> UnprocessedCommands { get; }
        Queue<byte> NotSentCommands { get; }
        public DateTime LastTimeRequest { get; set; }
    }
}