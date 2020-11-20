using System;
using System.Collections.Generic;
using Models;
using Models.Characters;

namespace Network
{
    public class GameManagerServer : IGameManagerServer, IDisposable
    {
        private readonly ITrackableDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly IWorldData _worldData;

        public IModelManager ModelManager { get; }

        public GameManagerServer(ITrackableDictionary<uint, IClientProxy> clientProxyDic, IModelManager modelManager, IWorldData worldData)
        {
            _clientProxyDic = clientProxyDic;
            _worldData = worldData;
            ModelManager = modelManager;

            AddClientProxyDicListeners();
        }

        private void AddClientProxyDicListeners()
        {
            _clientProxyDic.Adding += OnClientAdding;
            _clientProxyDic.Removing += OnClientRemoving;
        }

        private void RemoveClientProxyDicListeners()
        {
            _clientProxyDic.Adding -= OnClientAdding;
            _clientProxyDic.Removing -= OnClientRemoving;
        }

        private void OnClientAdding(uint id, IClientProxy client)
        {
            int playerId = (int) id;
            int characterExemplarId = _worldData.CharacterData.ExemplarDic.Add(new CharacterData());
            var playerData = new PlayerData
            {
                ControllableCharacterExemplarId = characterExemplarId
            };
            _worldData.PlayersData.ExemplarDic.Add(playerId, playerData);
            
            client.NotSentToClientPacket.MutablePacketDic[DataType.Command].Fill(GameCommandType.SetControllablePlayer);
            client.NotSentToClientPacket.MutablePacketDic[DataType.Command].Fill(playerId);
        }

        private void OnClientRemoving(uint id, IClientProxy client)
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