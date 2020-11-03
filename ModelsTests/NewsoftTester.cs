using Models.Characters;
using Network;
using NUnit.Framework;
using Serialization;
using Serialization.JsonNetSerialization;

namespace ModelsTests
{
    [TestFixture]
    public class NewsoftTester
    {
        [Test]
        public void SerializeTest()
        {
            ISerializer serializer = new JsonNetSerializer();
            IMutablePacket mutablePacket = new MutablePacket(serializer);
            var characterData = new SerializableCharacterData()
            {
                HealthPointData = new SerializableHealthPointData(),
                Id = "player_character",
                HoldWeaponId = "axe"
            };
            int exemplarId = 324324234;
            
            mutablePacket.Fill(exemplarId);
            mutablePacket.Fill(characterData);
            var resultInt = mutablePacket.Pull<int>();
            var resultData = mutablePacket.Pull<SerializableCharacterData>();
        }
    }
}