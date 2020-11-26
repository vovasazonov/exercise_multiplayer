using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
using NUnit.Framework;
using Replications;
using ReplicationTests;
using Serialization;
using Serialization.JsonNetSerialization;

namespace ReplicationTest
{
    [TestFixture]
    public class Tests
    {
        private ICustomCastObject _customCastObject;
        private IDataMock _dataMock;
        private IReplication _replicationMock;
        
        [SetUp]
        public void SetUp()
        {
            _customCastObject = new CustomCastObjectMock();
            _dataMock = new DataMock();
            _replicationMock = new ReplicationMock(_customCastObject, _dataMock);
        }

        [Test]
        public void WriteDiff_CountWroteDataMore0_True()
        {
            _dataMock.IntValue = 10;

            Assert.IsTrue(((Dictionary<string, object>) _replicationMock.WriteDiff()).Count > 0);
        }
        
        [Test]
        public void WriteDiff_CountWroteDataIs0_True()
        {
            Assert.IsTrue(((Dictionary<string, object>) _replicationMock.WriteDiff()).Count == 0);
        }
        
        [Test]
        public void WriteWhole_CountWroteDataMore0_True()
        {
            Assert.IsTrue(((Dictionary<string, object>) _replicationMock.WriteWhole()).Count > 0);
        }
        
        [Test]
        public void Read_ValueSet_True()
        {
            _dataMock.IntValue = 1;
            var dataRead = new Dictionary<string,object>
            {
                {nameof(_dataMock.IntValue), 5}
            };
            
            _replicationMock.Read(dataRead);

            Assert.IsTrue(_dataMock.IntValue == 5);
        }
    }
}