using System;
using System.Collections;
using Network;
using UnityEngine;

namespace DefaultNamespace
{
    public class NetworkManager : MonoBehaviour
    {
        private IClient _client;
        private IEnumerator _clientCoroutine;

        private void Awake()
        {
            _client = new UdpClient();
        }


        private void OnDestroy()
        {
            _client.Dispose();
        }
    }
}