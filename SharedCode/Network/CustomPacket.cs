using System.Collections.Generic;
using Serialization;

namespace Network
{
    public class CustomPacket : ICustomPacket
    {
        private readonly Queue<byte> _data = new Queue<byte>();
        private readonly ISerializer _serializer;

        public byte[] Data => _data.ToArray();

        public CustomPacket(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Fill<T>(T obj)
        {
            var bytes = obj as byte[];
            _data.Enqueue(bytes ?? _serializer.Serialize(obj));
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