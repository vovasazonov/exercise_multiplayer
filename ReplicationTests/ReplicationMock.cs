using System;
using Replications;
using ReplicationTests;
using Serialization;

namespace ReplicationTest
{
    public sealed class ReplicationMock : Replications.Replication
    {
        private readonly IDataMock _dataMock;
        private int _oldIntValue;

        public ReplicationMock(ICustomCastObject castObject, IDataMock dataMock) : base(castObject)
        {
            _dataMock = dataMock;
            
            InstantiateProperty("int_value", new Property(GetValue,SetValue,ContainsDiffValue,ResetDiffValue));
        }

        private object GetValue()
        {
            return _dataMock.IntValue;
        }

        private void SetValue(object obj)
        {
            _dataMock.IntValue = _castObject.To<int>(obj);
        }

        private bool ContainsDiffValue()
        {
            return _oldIntValue != _dataMock.IntValue;
        }

        private void ResetDiffValue()
        {
            _oldIntValue = _dataMock.IntValue;
        }
    }
}