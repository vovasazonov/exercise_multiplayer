namespace Serialization
{
    public interface ICustomCastObject
    {
        TResult To<TResult>(object obj);
    }
}