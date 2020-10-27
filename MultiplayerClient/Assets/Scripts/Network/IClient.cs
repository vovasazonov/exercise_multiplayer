using System;

namespace Network
{
    public interface IClient : IDisposable
    {
        event EventHandler<PacketReceivedEventArgs> ClientConnect; 
        event EventHandler<PacketReceivedEventArgs> ClientDisconnect; 
        event EventHandler<PacketReceivedEventArgs> PacketReceived;
    
        void SendPacket(byte[] packetBytes);
    }
}
