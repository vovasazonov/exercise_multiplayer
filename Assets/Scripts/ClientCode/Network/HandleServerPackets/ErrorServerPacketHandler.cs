#if UNITY_EDITOR
using UnityEngine;

#endif

namespace Network.HandleServerPackets
{
    public struct ErrorServerPacketHandler : IServerPacketHandler
    {
        public void HandlePacket()
        {
#if UNITY_EDITOR
            Debug.LogError($"Invalid packet from server");
#endif
        }
    }
}