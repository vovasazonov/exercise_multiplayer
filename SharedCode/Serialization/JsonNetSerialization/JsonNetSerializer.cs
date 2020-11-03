using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Serialization.JsonNetSerialization
{
    public class JsonNetSerializer : ISerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            Queue<byte> queueBytes = new Queue<byte>();
            
            string stringData = JsonConvert.SerializeObject(obj);
            byte[] bytesData = Encoding.ASCII.GetBytes(stringData);
            ushort amountBytesData = Convert.ToUInt16(bytesData.Length);
            queueBytes.Enqueue(BitConverter.GetBytes(amountBytesData));
            queueBytes.Enqueue(bytesData);

            return queueBytes.ToArray();
        }

        public long Deserialize<T>(byte[] bytes, out T resultObj)
        {
            Queue<byte> queueBytes = new Queue<byte>(bytes);
        
            ushort amountBytesData = BitConverter.ToUInt16(queueBytes.Dequeue(sizeof(ushort)), 0);
            string stringData = Encoding.ASCII.GetString(queueBytes.Dequeue(amountBytesData));
            resultObj = JsonConvert.DeserializeObject<T>(stringData);
            long totalDeserializedData = Convert.ToInt64(sizeof(ushort) + amountBytesData);
            return totalDeserializedData;
        }
    }
}