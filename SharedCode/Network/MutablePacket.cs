using System.Collections.Generic;
using Serialization;

namespace Network
{
    public class MutablePacket : IMutablePacket
    {
        private readonly Queue<byte> _data = new Queue<byte>();
        private readonly ISerializer _serializer;

        public byte[] Data => _data.ToArray();

        public MutablePacket(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public MutablePacket(ISerializer serializer, byte[] data) : this(serializer)
        {
            _data = new Queue<byte>(data);
        }

        public void Fill<T>(T obj)
        {
            _data.Enqueue(_serializer.Serialize(obj));
        }

        public T Pull<T>()
        {
            var deserializedBytesAmount = _serializer.Deserialize(Data, out T deserializedObj);
            _data.DiscardFirst(deserializedBytesAmount);

            return deserializedObj;
        }

        public void Clear()
        {
            _data.Clear();
        }
    }
}