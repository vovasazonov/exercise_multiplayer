using System.Linq;

namespace System.Collections.Generic
{
    internal static class QueueExtensions
    {
        public static void Enqueue(this Queue<byte> queueBytes, in byte[] bytes)
        {
            foreach (var @byte in bytes)
            {
                queueBytes.Enqueue(@byte);
            }
        }

        public static byte[] Dequeue(this Queue<byte> queueBytes, in int amountBytes)
        {
            var listBytes = new List<byte>();

            for (int i = 0; i < amountBytes && queueBytes.Count > 0; i++)
            {
                listBytes.Add(queueBytes.Dequeue());
            }

            return listBytes.ToArray();
        }

        public static void DiscardFirst(this Queue<byte> queueBytes, long amountBytes)
        {
            for (int i = 0; i < amountBytes; i++)
            {
                queueBytes.Dequeue();
            }
        }
    }
}