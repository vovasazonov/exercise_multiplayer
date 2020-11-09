using Game;
using Network;
using Serialization;
using Serialization.JsonNetSerialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private protected ViewManager _viewManager;
    private IClient _client;
    private NetworkManager _networkManager;
    private IPresenter _presenterManager;

    private void Awake()
    {
        ISerializer serializer = new JsonNetSerializer();
        UdpClientInfo udpClientInfo = new UdpClientInfo
        {
            ServerIp = "127.0.0.1",
            ServerPort = 3000,
            ChannelId = 0
        };
        IModelManagerClient modelManagerClient = new ModelManagerClient();
        _client = new UdpClient(udpClientInfo);
        _networkManager = new NetworkManager(_client, serializer, modelManagerClient) {MillisecondsBetweenSendPacket = 100};
        _presenterManager = new PresenterManager(_viewManager,modelManagerClient);

        _presenterManager.Activate();
    }

    private void Update()
    {
        _client.Update();
        _networkManager.Update();
    }

    private void OnDestroy()
    {
        _presenterManager.Deactivate();
        _client.Dispose();
        _networkManager.Dispose();
    }
}