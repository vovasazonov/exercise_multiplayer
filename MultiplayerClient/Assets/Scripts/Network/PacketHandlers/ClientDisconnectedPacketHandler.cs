#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Network.PacketHandlers
{
    public struct ClientDisconnectedPacketHandler : IPacketHandler
    {
        public void HandlePacket()
        {
#if UNITY_EDITOR
            Debug.LogError("client disconnected");
#endif
        }
    }
}