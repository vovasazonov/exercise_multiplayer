using System;

namespace Network
{
    public class PacketReceivedEventArgs : EventArgs
    {
        public uint ClientId { get; }
        public byte[] Packet { get; }

        public PacketReceivedEventArgs(uint clientId, byte[] packet = null)
        {
            ClientId = clientId;
            Packet = packet ?? new byte[0];
        }
    }
}