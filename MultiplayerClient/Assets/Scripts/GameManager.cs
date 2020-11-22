using Game;
using Models;
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
        ICustomCastObject customCastObject = new JsonCastObject();
        WorldData worldData = new WorldData();
        worldData.SetCustomCast(customCastObject);
        IModelManagerClient modelManagerClient = new ModelManagerClient(worldData);
        _client = new UdpClient(udpClientInfo);
        _networkManager = new NetworkManager(_client, serializer, modelManagerClient, worldData) {MillisecondsBetweenSendPacket = 1000};
        _presenterManager = new GamePresenter(_viewManager,modelManagerClient);

        _presenterManager.Activate();
    }

    private void Update()
    {
        _networkManager.Update();
    }

    private void OnDestroy()
    {
        _presenterManager.Deactivate();
        _client.Dispose();
        _networkManager.Dispose();
    }
}