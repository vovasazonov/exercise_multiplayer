using Network;
using Network.Clients;
using UnityEngine;

public class GameManager : MonoBehaviour
{
      private NetworkManagerClient _networkManagerClient;

      private void Awake()
      { 
            _networkManagerClient = new NetworkManagerClient(new CustomHttpClient());
      }

      private void OnDestroy()
      {
            _networkManagerClient.Dispose();
      }
}