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
        
        public static void DiscardFirst(this Queue<byte> queueBytes, long amountBytes)
        {
            for (int i = 0; i < amountBytes; i++)
            {
                queueBytes.Dequeue();
            }
        }
    }
}