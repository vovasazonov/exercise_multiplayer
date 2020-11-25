using System;

namespace ReplicationTests
{
    public interface IDataMock
    {
        event EventHandler IntValueUpdated;
        int IntValue { get; set; }
    }
}