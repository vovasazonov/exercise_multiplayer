﻿using System.Collections.Generic;
using System.Linq;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class CharacterEnemyAttackedEventHandler : IGameEventHandler
    {
        private readonly ICharacterModel _characterModel;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;
        private readonly IMutablePacket _recordPacket;
        private readonly int _characterExemplarId;

        public CharacterEnemyAttackedEventHandler(IMutablePacket recordPacket, int characterExemplarId, ICharacterModel characterModel, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _recordPacket = recordPacket;
            _characterExemplarId = characterExemplarId;
            _characterModel = characterModel;
            _characterModelDic = characterModelDic;
        }

        public void Activate()
        {
            _characterModel.EnemyAttacked += OnEnemyAttacked;
        }

        public void Deactivate()
        {
            _characterModel.EnemyAttacked -= OnEnemyAttacked;
        }

        private void OnEnemyAttacked(object sender, AttackEventArgs e)
        {
            var enemyExemplarId = _characterModelDic.First(keyValuePair => keyValuePair.Value == e.EnemyAttacked).Key;
            _recordPacket.Fill(GameCommandType.CharacterAttackEnemy);
            _recordPacket.Fill(_characterExemplarId);
            _recordPacket.Fill(enemyExemplarId);
        }
    }
}