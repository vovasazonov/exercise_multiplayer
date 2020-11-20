using System.Collections.Generic;

namespace Network
{
    public interface IDataMutablePacket
    {
        IReadOnlyDictionary<DataType,IMutablePacket> MutablePacketDic { get; }

        void FillCombinedData(byte[] data);
        byte[] PullCombinedData();
        void Clear();
    }
}