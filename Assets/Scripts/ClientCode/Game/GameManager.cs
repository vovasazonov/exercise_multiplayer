using System.Threading.Tasks;
using Network;
using Network.Clients;
using Serialization;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private protected ViewManager _viewManager;
        private NetworkManagerClient _networkManagerClient;
        private ModelManagerClient _modelManager;
        private PresenterManager _presenterManager;
        private ClientNetworkInfo _clientNetworkInfo = new ClientNetworkInfo();
        private readonly ISerializer _serializer = new BinaryFormatterSerializer();

        private void Awake()
        {
            _modelManager = new ModelManagerClient(_clientNetworkInfo, _serializer);
            _networkManagerClient = new NetworkManagerClient(new CustomHttpClient(), _clientNetworkInfo, _serializer, _modelManager);
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