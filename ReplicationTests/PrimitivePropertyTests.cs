using NUnit.Framework;
using Replications;

namespace ReplicationTests
{
    [TestFixture]
    public class PrimitivePropertyTests
    {
        private readonly CustomCastObjectMock _cast = new CustomCastObjectMock();
        private int _value;
        private PrimitiveProperty<int> _property;

        [SetUp]
        public void SetUp()
        {
            _property = new PrimitiveProperty<int>(() => _value,(object obj)=>_value = _cast.To<int>(obj));
        }

        [Test]
        public void ContainsDiff_ValueChanged_False()
        {
            var buf = _value;
            _value = 6;
            _value = buf;
            
            Assert.IsFalse(_property.ContainsDiff());
        }
        
        [Test]
        public void ContainsDiff_ValueChanged_True()
        {
            _value = 6;
            
            Assert.IsTrue(_property.ContainsDiff());
        }

        [Test]
        public void ResetDiff_ValueChanged_False()
        {
            _value = 6;
            _property.ResetDiff();
            
            Assert.IsFalse(_property.ContainsDiff());
        }

        [Test]
        public void WriteDiff_ValueSameAsInput_True()
        {
            _value = 324234;
            
            Assert.AreEqual(_value,_property.WriteDiff());
        }
        
        [Test]
        public void WriteWhole_ValueSameAsInput_True()
        {
            _value = 324234;
            
            Assert.AreEqual(_value,_property.WriteWhole());
        }

        [Test]
        public void Read_ValueSameAsInput_True()
        {
            int readValue = 324342423;
            
            _property.Read(readValue);
            
            Assert.AreEqual(_value,readValue);
        }
    }
}