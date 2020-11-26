using Serialization;

namespace ReplicationTests
{
    public class CustomCastObjectMock : ICustomCastObject
    {
        public TResult To<TResult>(object obj)
        {
            return (TResult) obj;
        }
    }
}