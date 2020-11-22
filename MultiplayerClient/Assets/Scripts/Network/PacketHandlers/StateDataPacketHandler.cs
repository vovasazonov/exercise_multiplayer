using Models;

namespace Network.PacketHandlers
{
    public readonly struct StateDataPacketHandler : IPacketHandler
    {
        private readonly IMutablePacket _packet;
        private readonly IReplication _worldDataReplication;

        public StateDataPacketHandler(IMutablePacket packet, IReplication worldDataReplication)
        {
            _packet = packet;
            _worldDataReplication = worldDataReplication;
        }

        public void HandlePacket()
        {
            while (_packet.Data.Length > 0)
            {
                var stateData = _packet.Pull<object>();
                _worldDataReplication.Read(stateData);
            }
        }
    }
}