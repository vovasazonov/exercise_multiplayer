using System;

namespace Server.Network
{
    public class ClientEventArgs : EventArgs
    {
        public uint ClientId { get; }
        public byte[] PacketReceived { get; }

        public ClientEventArgs(uint clientId, byte[] packetReceived = null)
        {
            ClientId = clientId;
            PacketReceived = packetReceived ?? new byte[0];
        }
    }
}