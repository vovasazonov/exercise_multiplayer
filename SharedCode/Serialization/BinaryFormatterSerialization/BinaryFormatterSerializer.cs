using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SharedCode.Serialization;

namespace Serialization.BinaryFormatterSerialization
{
    public class BinaryFormatterSerializer : ISerializer
    {
        public ICustomCastObject GetCaster() => new ClassicCastObject();

        public byte[] Serialize<T>(T obj)
        {
            Queue<byte> bytes = new Queue<byte>();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Binder = new CustomizedBinder();
                binaryFormatter.Serialize(memoryStream, obj);

                bytes.Enqueue(memoryStream.ToArray());
            }

            return bytes.ToArray();
        }

        public long Deserialize<T>(byte[] bytes, out T resultObj)
        {
            long bytesDeserialized;

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Binder = new CustomizedBinder();
                resultObj = (T) binaryFormatter.Deserialize(memoryStream);
                bytesDeserialized = memoryStream.Position;
            }

            return bytesDeserialized;
        }
    }
}