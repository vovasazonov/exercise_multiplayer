using System;

namespace Network
{
    public interface IServer : IDisposable
    {
        event EventHandler<PacketReceivedEventArgs> ClientConnect; 
        event EventHandler<PacketReceivedEventArgs> ClientDisconnect; 
        event EventHandler<PacketReceivedEventArgs> PacketReceived;

        void SendPacket(uint clientId, byte[] packetBytes);
    }
}