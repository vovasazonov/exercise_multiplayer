using System;

namespace Network
{
    public class PacketReceivedEventArgs : EventArgs
    {
        public byte[] Packet { get; }
        
        public PacketReceivedEventArgs(byte[] packet = null)
        {
            Packet = packet ?? new byte[0];
        }
    }
}