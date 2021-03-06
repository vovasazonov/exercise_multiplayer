﻿namespace Network
{
    public interface IMutablePacket
    {
        byte[] Data { get; }
        
        void Fill<T>(T obj);
        T Pull<T>();
        void Combine(IMutablePacket other);
        void Clear();
    }
}