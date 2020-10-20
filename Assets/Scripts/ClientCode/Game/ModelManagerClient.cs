using Game.Characters.Models;
using Network;
using Serialization;

namespace Game
{
    public class ModelManagerClient
    {
        public readonly ModelManager ModelManager;
        
        public ICharacterModel EnemyModel { get; }

        public ModelManagerClient(ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            ModelManager = new ModelManager();

            EnemyModel = new CharacterModelClient(ModelManager.EnemyModel, clientNetworkInfo, serializer);
        }
    }
}