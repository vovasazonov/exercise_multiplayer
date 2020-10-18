using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

namespace Network.Clients
{
    public class CustomHttpClient : IClient
    {
        public event Action<Queue<byte>> ServerPacketCame;
        
        private readonly string _url = "http://localhost:8888/";

        public async void SendPacket(Queue<byte> packet)
        {
            using (var client = new HttpClient())
            {
                Debug.Log("1");
                var byteContent = new ByteArrayContent(packet.ToArray());
                var response = await client.PostAsync(_url, byteContent);
                Debug.Log("2");

                var packetCame = new Queue<byte>(response.Content.ReadAsByteArrayAsync().Result);

                if (packetCame.Count > 0)
                {
                    OnServerPacketCame(packetCame);
                }
            }
        }

        private void OnServerPacketCame(Queue<byte> packet)
        {
            ServerPacketCame?.Invoke(packet);
        }
    }
}