using System;
using System.Collections.Generic;
using Models;
using Models.Weapons;
using Network;
using Serialization;
using Serialization.JsonNetSerialization;

class Program
{
    static void Main(string[] args)
    {
        WorldData worldData = new WorldData();
        InstantiateWeapons(worldData);
        IModelManager modelManager = new ModelManager(worldData);
        ISerializer serializer = new JsonNetSerializer();
        UdpServerInfo udpServerInfo = new UdpServerInfo
        {
            Port = 3000,
            ChannelId = 0,
            MaxClients = 100,
            PeerTimeOutLimit = 32,
            PeerTimeOutMinimum = 1000,
            PeerTimeOutMaximum = 4000
        };
        using IServer server = new UdpServer(udpServerInfo);
        NetworkManager networkManager = new NetworkManager(server, serializer, modelManager, worldData, 500);

        networkManager.Start();
        Console.WriteLine("Press 'q' to stop server");
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
        }

        networkManager.Stop();
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
        worldData.WeaponsData.Add(axeWeaponData);
        worldData.WeaponsData.Add(spellWeaponData);
    }
}