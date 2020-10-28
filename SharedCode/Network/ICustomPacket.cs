namespace Network
{
    public interface ICustomPacket
    {
        byte[] Data { get; }
        
        void Fill<T>(T obj);
        T Pull<T>();
        void Clear();
    }
}