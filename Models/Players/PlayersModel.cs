using System.Collections.Generic;
using Models.Characters;

namespace Models
{
    public class PlayersModel : ExemplarsModel<IPlayerModel, IPlayerData>
    {
        private readonly IExemplarsModel<ICharacterModel> _charactersModel;

        public PlayersModel(ITrackableDictionary<int, IPlayerData> exemplarsData, IExemplarsModel<ICharacterModel> charactersModel) : base(exemplarsData)
        {
            _charactersModel = charactersModel;
        }

        protected override void AddModel(int id, IPlayerData data)
        {
            ExemplarModelDic.Add(id, new PlayerModel(data, _charactersModel.ExemplarModelDic));
        }
    }
}