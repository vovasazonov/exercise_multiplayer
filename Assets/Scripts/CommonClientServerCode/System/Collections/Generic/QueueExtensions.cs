namespace System.Collections.Generic
{
    public static class QueueExtensions
    {
        public static void Enqueue(this Queue<byte> queueBytes, in byte[] bytes)
        {
            foreach (var @byte in bytes)
            {
                queueBytes.Enqueue(@byte);
            }
        }
        
        public static byte[] Dequeue(this Queue<byte> queueBytes, long amountBytes)
        {
            var listBytes = new List<byte>();
            
            for (int i = 0; i < amountBytes; i++)
            {
                listBytes.Add(queueBytes.Dequeue());
            }

            return listBytes.ToArray();
        }
    }
}