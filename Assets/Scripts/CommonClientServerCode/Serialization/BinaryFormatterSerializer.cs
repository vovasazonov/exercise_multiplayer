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
                byte[] amountSerializeInBytes = BitConverter.GetBytes(Convert.ToUInt16(memoryStream.Length));
                bytes.Enqueue(amountSerializeInBytes);
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
                byte[] amountSerializeInBytes = new byte[2];
                memoryStream.Read(amountSerializeInBytes, 0, 2);
                ushort amountSerializeBytes = BitConverter.ToUInt16(amountSerializeInBytes, 0);

                byte[] objectInBytes = new byte[amountSerializeBytes];
                memoryStream.Read(objectInBytes, 0, amountSerializeBytes);

                using (MemoryStream memoryStream2 = new MemoryStream(objectInBytes))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Binder = new CustomizedBinder();
                    obj = (T)binaryFormatter.Deserialize(memoryStream2);
                    bytes.Dequeue(amountSerializeBytes + sizeof(ushort));
                }
            }
            
            return obj;
        }
    }
}