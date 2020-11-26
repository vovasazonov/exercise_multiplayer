using Serialization;

namespace SharedCode.Serialization
{
    public class ClassicCastObject : ICustomCastObject
    {
        public TResult To<TResult>(object obj)
        {
            return (TResult) obj;
        }
    }
}