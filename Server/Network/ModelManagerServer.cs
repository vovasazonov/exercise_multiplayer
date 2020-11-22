using System;
using System.Collections.Generic;
using Models;
using Models.Characters;

namespace Network
{
    public class ModelManagerServer : IModelManagerServer, IDisposable
    {
        private readonly ITrackableDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly IWorldData _worldData;

        public IModelManager ModelManager { get; }

        public ModelManagerServer(ITrackableDictionary<uint, IClientProxy> clientProxyDic, IModelManager modelManager, IWorldData worldData)
        {
            _clientProxyDic = clientProxyDic;
            _worldData = worldData;
            ModelManager = modelManager;

            AddClientProxyDicListeners();
        }

        private void AddClientProxyDicListeners()
        {
            _clientProxyDic.Added += OnClientAdded;
            _clientProxyDic.Removed += OnClientRemoved;
        }

        private void RemoveClientProxyDicListeners()
        {
            _clientProxyDic.Added -= OnClientAdded;
            _clientProxyDic.Removed -= OnClientRemoved;
        }

        private void OnClientAdded(uint id, IClientProxy client)
        {
            int playerId = (int) id;
            var characterData = new CharacterData();
            characterData.HealthPointData.MaxPoints = 100;
            characterData.HealthPointData.Points = 100;
            int characterExemplarId = _worldData.CharacterData.ExemplarDic.Add(characterData);
            var playerData = new PlayerData
            {
                ControllableCharacterExemplarId = characterExemplarId
            };
            _worldData.PlayersData.ExemplarDic.Add(playerId, playerData);
            
            client.NotSentToClientPacket.MutablePacketDic[DataType.Command].Fill(GameCommandType.SetControllablePlayer);
            client.NotSentToClientPacket.MutablePacketDic[DataType.Command].Fill(playerId);
        }

        private void OnClientRemoved(uint id, IClientProxy client)
        {
            int playerId = (int) id;
            _worldData.PlayersData.ExemplarDic.Remove(playerId);
        }

        public void Dispose()
        {
            RemoveClientProxyDicListeners();
        }
    }
}