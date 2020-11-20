using System;

namespace Network
{
    public interface IServer : IDisposable
    {
        event EventHandler<PacketReceivedEventArgs> ClientConnect; 
        event EventHandler<PacketReceivedEventArgs> ClientDisconnect; 
        event EventHandler<PacketReceivedEventArgs> PacketReceived;

        uint GetMtu(uint clientId);
        void SendPacket(uint clientId, byte[] packetBytes, bool isReliable);
    }
}