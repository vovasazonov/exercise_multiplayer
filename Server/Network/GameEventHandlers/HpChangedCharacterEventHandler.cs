using System;
using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class HpChangedCharacterEventHandler : IGameEventHandler
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly ICharacterModel _characterModel;
        private readonly int _exemplarId;

        public HpChangedCharacterEventHandler(IDictionary<uint, IClientProxy> clientProxyDic, int exemplarId, ICharacterModel characterModel)
        {
            _clientProxyDic = clientProxyDic;
            _exemplarId = exemplarId;
            _characterModel = characterModel;
        }

        public void Activate()
        {
            _characterModel.HealthPoint.PointsChanged += OnPointsChanged;
        }

        public void Deactivate()
        {
            _characterModel.HealthPoint.PointsChanged -= OnPointsChanged;
        }

        private void OnPointsChanged(object sender, EventArgs e)
        {
            NotifyClients();
        }

        private void NotifyClients()
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                clientProxy.NotSentToClientPacket.Fill(GameCommandType.CharacterHpChanged);
                clientProxy.NotSentToClientPacket.Fill(_exemplarId);
                clientProxy.NotSentToClientPacket.Fill(_characterModel.HealthPoint.Points);
            }
        }
    }
}