using System.Collections.Generic;
using Models;
using Models.Characters;
using Models.Weapons;
using Network;
using NUnit.Framework;
using Serialization;
using Serialization.BinaryFormatterSerialization;
using Serialization.JsonNetSerialization;

namespace TestProject1
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            ISerializer serializer = new JsonNetSerializer();
            IDataMutablePacket dataMutablePacket = new DataMutablePacket(serializer);
            dataMutablePacket.MutablePacketDic[DataType.Command].Fill(GameCommandType.SetControllablePlayer);

            var bytes = dataMutablePacket.PullCombinedData();
            dataMutablePacket.FillCombinedData(bytes);

            int f = 0;
        }

        [Test]
        public void Test2()
        {
            ISerializer serializer = new JsonNetSerializer();
            WorldData worldData1 = new WorldData();
            worldData1.SetCustomCast(new JsonCastObject());
            WorldData worldData2 = new WorldData();
            worldData2.SetCustomCast(new JsonCastObject());

            InstantiateWeapons(worldData1);
            InstantiatePlayers(worldData1);

            var whole = worldData1.Write(ReplicationType.Diff);
            byte[] bytes = serializer.Serialize(whole);
            serializer.Deserialize(bytes, out object deserialized);
            worldData2.Read(deserialized);
            int end = 0;
        }
        
        private static void InstantiateWeapons(IWorldData worldData)
        {
            IWeaponData axeWeaponData = new WeaponData()
            {
                Id = "axe",
                Damage = 3
            };
            IWeaponData spellWeaponData = new WeaponData()
            {
                Id = "spell",
                Damage = 1
            };
            worldData.WeaponsData.ExemplarDic.Add(axeWeaponData);
            worldData.WeaponsData.ExemplarDic.Add(spellWeaponData);
        }

        private static void InstantiatePlayers(IWorldData worldData)
        {
            int playerId = 5;
            int characterExemplarId = worldData.CharacterData.ExemplarDic.Add(new CharacterData());
            var playerData = new PlayerData
            {
                ControllableCharacterExemplarId = characterExemplarId
            };
            worldData.PlayersData.ExemplarDic.Add(playerId, playerData);
        }
    }
}