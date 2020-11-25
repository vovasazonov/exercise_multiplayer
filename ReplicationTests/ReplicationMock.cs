using System;
using Replications;
using ReplicationTests;

namespace ReplicationTest
{
    public sealed class ReplicationMock : Replication
    {
        private readonly IDataMock _dataMock;

        public ReplicationMock(ICustomCastObject castObject, IDataMock dataMock) : base(castObject)
        {
            _dataMock = dataMock;
            _dataMock.IntValueUpdated += OnIntValueUpdated;

            _getterDic.Add(nameof(_dataMock.IntValue), () => _dataMock.IntValue);
            _setterDic.Add(nameof(_dataMock.IntValue), obj => _dataMock.IntValue = _castObject.To<int>(obj));
        }

        private void OnIntValueUpdated(object? sender, EventArgs e) => _diffDic[nameof(_dataMock.IntValue)] = _dataMock.IntValue;
    }
}