using System;

namespace Network
{
    public interface IClient : IDisposable
    {
        event EventHandler<PacketReceivedEventArgs> ClientConnected; 
        event EventHandler<PacketReceivedEventArgs> ClientDisconnected; 
        event EventHandler<PacketReceivedEventArgs> PacketReceived;
    
        void SendPacket(byte[] packetBytes);
    }
}
