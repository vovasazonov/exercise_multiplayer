using System.Collections.Generic;
using Serialization;

namespace Network
{
    public class DataMutablePacket : IDataMutablePacket
    {
        private readonly ISerializer _serializer;
        private readonly Dictionary<DataType, IMutablePacket> _mutablePacketDic = new Dictionary<DataType, IMutablePacket>();
        public IReadOnlyDictionary<DataType, IMutablePacket> MutablePacketDic => _mutablePacketDic;

        public DataMutablePacket(ISerializer serializer)
        {
            _serializer = serializer;
            _mutablePacketDic.Add(DataType.Command, new MutablePacket(_serializer));
            _mutablePacketDic.Add(DataType.State, new MutablePacket(_serializer));
        }

        public void FillCombinedData(byte[] data)
        {
            var inPacketTemp = new MutablePacket(_serializer, data);

            while (inPacketTemp.Data.Length > 0)
            {
                var dataType = inPacketTemp.Pull<DataType>();
                var dataPacket = new MutablePacket(_serializer,inPacketTemp.Pull<byte[]>());
                _mutablePacketDic[dataType].Combine(dataPacket);
            }
        }

        public byte[] PullCombinedData()
        {
            var mutablePacketTemp = new MutablePacket(_serializer);
            foreach (var dataType in _mutablePacketDic.Keys)
            {
                mutablePacketTemp.Fill(dataType);
                mutablePacketTemp.Fill(_mutablePacketDic[dataType].Data);

                _mutablePacketDic[dataType].Clear();
            }

            return mutablePacketTemp.Data;
        }

        public void Clear()
        {
            foreach (var mutablePacket in _mutablePacketDic.Values)
            {
                mutablePacket.Clear();
            }
        }
    }
}