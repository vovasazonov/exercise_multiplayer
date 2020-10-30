#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Network.PacketHandlers
{
    public struct ClientConnectedPacketHandler : IPacketHandler
    {
        public void HandlePacket()
        {
#if UNITY_EDITOR
            Debug.Log("Client connected");
#endif
        }
    }
}