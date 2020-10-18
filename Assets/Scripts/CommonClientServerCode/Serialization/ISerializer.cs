using System.Collections.Generic;

namespace Serialization
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T obj);
        T Deserialize<T>(byte[] bytes);
        T Deserialize<T>(Queue<byte> bytes);
    }
}