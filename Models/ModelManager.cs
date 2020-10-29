using System;
using System.Collections.Generic;
using Models.Characters;

namespace Models
{
    public class ModelManager : IModelManager
    {
        public int ControllablePlayerExemplarId { get; set; }
        public ITrackableDictionary<int, IPlayerModel> PlayerModelDic { get; } = new TrackableDictionary<int, IPlayerModel>();
        public ITrackableDictionary<int, ICharacterModel> CharacterModelDic { get; } = new TrackableDictionary<int, ICharacterModel>();
    }
}