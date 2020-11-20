using System.Collections.Generic;
using Serialization;

namespace Network
{
    public class DataMutablePacket : IDataMutablePacket
    {
        private readonly ISerializer _serializer;
        private readonly Dictionary<DataType,IMutablePacket> _mutablePacketDic = new Dictionary<DataType, IMutablePacket>();
        public IReadOnlyDictionary<DataType, IMutablePacket> MutablePacketDic => _mutablePacketDic;
        
        public DataMutablePacket(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void FillCombinedData(byte[] data)
        {
            var mutablePacketTemp = new MutablePacket(_serializer,data);

            while (mutablePacketTemp.Data.Length > 0)
            {
                var dataType = mutablePacketTemp.Pull<DataType>();
                var oldArrayData = _mutablePacketDic[dataType].Data;
                var newArrayData = mutablePacketTemp.Pull<byte[]>();

                var commonArray = oldArrayData.Combine(newArrayData);
                _mutablePacketDic[dataType] = new MutablePacket(_serializer,commonArray);
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