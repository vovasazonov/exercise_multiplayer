using System.Collections.Generic;
using Models;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class PlayerAddingEventHandler : IGameEventHandler
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly IModelManager _modelManager;

        public PlayerAddingEventHandler(IDictionary<uint, IClientProxy> clientProxyDic, IModelManager modelManager)
        {
            _clientProxyDic = clientProxyDic;
            _modelManager = modelManager;
        }

        public void Activate()
        {
            _modelManager.PlayerModelDic.Adding += OnAdding;
        }

        public void Deactivate()
        {
            _modelManager.PlayerModelDic.Adding -= OnAdding;
        }

        private void OnAdding(int playerId, IPlayerModel playerModel)
        {
            NotifyClients(playerId, playerModel);
        }

        private void NotifyClients(int playerId, IPlayerModel playerModel)
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                clientProxy.NotSentToClientPacket.Fill(GameCommandType.PlayerConnected);
                clientProxy.NotSentToClientPacket.Fill(playerId);
                clientProxy.NotSentToClientPacket.Fill(playerModel.ControllableCharacterExemplarId);

                if (clientProxy.Id == playerId)
                {
                    TransferWorldToNewPlayer(clientProxy);
                }
            }
        }
        private void TransferWorldToNewPlayer(IClientProxy newClientProxy)
        {
            var packet = newClientProxy.NotSentToClientPacket;
            
            foreach (var characterExemplarId in _modelManager.CharacterModelDic.Keys)
            {
                var characterData = new SerializableCharacterData();
                characterData.Set(_modelManager.CharacterModelDic[characterExemplarId]);
                
                packet.Fill(GameCommandType.CharacterAdd);
                packet.Fill(characterExemplarId);
                packet.Fill(characterData);
            }
        
            foreach (var playerExemplarId in _modelManager.PlayerModelDic.Keys)
            {
                packet.Fill(GameCommandType.PlayerConnected);
                packet.Fill(playerExemplarId);
                packet.Fill(_modelManager.PlayerModelDic[playerExemplarId].ControllableCharacterExemplarId);
            }
            
            _clientProxyDic[newClientProxy.Id].NotSentToClientPacket.Fill(GameCommandType.SetControllablePlayer);
            _clientProxyDic[newClientProxy.Id].NotSentToClientPacket.Fill((int)newClientProxy.Id);
        }
    }
}