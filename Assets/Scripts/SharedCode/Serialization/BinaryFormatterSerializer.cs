using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
    public class BinaryFormatterSerializer : ISerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            Queue<byte> bytes = new Queue<byte>();
            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Binder = new CustomizedBinder();
                binaryFormatter.Serialize(memoryStream, obj);
                if (memoryStream.Length > ushort.MaxValue)
                {
                    throw new NotSupportedException("Not supported serialization bigger than ushort.Maxvalue bytes.");
                }
                
                bytes.Enqueue(memoryStream.ToArray());
            }

            return bytes.ToArray();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            return Deserialize<T>(new Queue<byte>(bytes));
        }

        public T Deserialize<T>(Queue<byte> bytes)
        {
            T obj;
            
            using (MemoryStream memoryStream = new MemoryStream(bytes.ToArray()))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Binder = new CustomizedBinder();
                obj = (T)binaryFormatter.Deserialize(memoryStream);
                bytes.Dequeue(memoryStream.Position);
            }
            
            return obj;
        }
    }
}