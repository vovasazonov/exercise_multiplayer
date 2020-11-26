namespace Serialization
{
    public interface ISerializer
    {
        ICustomCastObject GetCaster();
        byte[] Serialize<T>(T obj);
        long Deserialize<T>(byte[] bytes, out T resultObj);
    }
}