namespace Serialization
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T obj);
        long Deserialize<T>(byte[] bytes, out T resultObj);
    }
}