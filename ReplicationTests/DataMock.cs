using System;

namespace ReplicationTests
{
    public class DataMock : IDataMock
    {
        public event EventHandler IntValueUpdated;

        private int _intValue;

        public int IntValue
        {
            get => _intValue;
            set
            {
                _intValue = value;
                CallIntValueUpdated();
            }
        }

        private void CallIntValueUpdated() => IntValueUpdated?.Invoke(this, EventArgs.Empty);
    }
}