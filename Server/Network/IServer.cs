using System;

namespace Server.Network
{
    public interface IServer : IDisposable
    {
        event EventHandler<ClientEventArgs> ClientConnect; 
        event EventHandler<ClientEventArgs> ClientDisconnect; 
        event EventHandler<ClientEventArgs> PacketReceived;

        void SendPacket(uint clientId, byte[] packetBytes);
    }
}