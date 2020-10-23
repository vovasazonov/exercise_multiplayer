using Descriptions;
using Network;
using Network.Clients;
using Serialization;
using Serialization.BinaryFormatterSerialization;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private protected ViewManager _viewManager;
        private DescriptionManagerClient _descriptionManagerClient;
        private NetworkManagerClient _networkManagerClient;
        private ModelManagerClient _modelManager;
        private PresenterManager _presenterManager;
        private readonly ClientNetworkInfo _networkInfoClient = new ClientNetworkInfo();
        private readonly ISerializer _serializer = new BinaryFormatterSerializer();

        private void Awake()
        {
            _descriptionManagerClient = new DescriptionManagerClient();
            _modelManager = new ModelManagerClient(_networkInfoClient, _serializer);
            _networkManagerClient = new NetworkManagerClient(new CustomHttpClient(), _networkInfoClient, _serializer, _modelManager);
            _presenterManager = new PresenterManager(_viewManager, _modelManager);

            _presenterManager.Activate();
        }

        private void OnDestroy()
        {
            _presenterManager.Deactivate();
            _networkManagerClient.Dispose();
        }
    }
}