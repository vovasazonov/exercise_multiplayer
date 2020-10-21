using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Server.Network
{
    public class HttpServer : IServer
    {
        private readonly IDictionary<int, IClientProxy> _clientProxyDic;
        public event Action<Queue<byte>, Queue<byte>> ClientPacketCame;

        private readonly HttpListener _httpListener = new HttpListener {Prefixes = {"http://localhost:8888/"}};
        private bool _keepGoing = true;
        private Task _mainLoopTask;
        private Task _checkClientConnectionTask;
        private readonly TimeSpan _timeBetweenClientRequestFail = new TimeSpan(0, 0, 10);

        public HttpServer(IDictionary<int, IClientProxy> clientProxyDic)
        {
            _clientProxyDic = clientProxyDic;
        }

        public void Start()
        {
            bool isPossibleStartMainLoop = !(_mainLoopTask != null && !_mainLoopTask.IsCompleted);

            if (isPossibleStartMainLoop)
            {
                _mainLoopTask = MainLoop();
                _checkClientConnectionTask = CheckClientConnection();
            }
        }

        public void Stop()
        {
            _keepGoing = false;
            lock (_httpListener)
            {
                _httpListener.Stop();
            }

            _mainLoopTask.Wait();
        }

        private async Task CheckClientConnection()
        {
            List<IClientProxy> clientProxyConnectFailList = new List<IClientProxy>();

            while (_keepGoing)
            {
                var timeOfDay = DateTime.Now.TimeOfDay;

                foreach (var clientProxy in _clientProxyDic.Values)
                {
                    var timeBetweenClientRequest = timeOfDay - clientProxy.LastTimeRequest.TimeOfDay;
                    bool isConnectFailed = timeBetweenClientRequest > _timeBetweenClientRequestFail;

                    if (isConnectFailed)
                    {
                        clientProxyConnectFailList.Add(clientProxy);
                    }
                }

                RemoveConnectFailedClients(clientProxyConnectFailList);

                await Task.Delay((int) _timeBetweenClientRequestFail.TotalMilliseconds);
            }
        }

        private void RemoveConnectFailedClients(List<IClientProxy> clientProxyForRemoveList)
        {
            for (int i = 0; i < clientProxyForRemoveList.Count; i++)
            {
                _clientProxyDic.Remove(clientProxyForRemoveList[i].IdClient);
            }

            clientProxyForRemoveList.Clear();
        }

        private async Task MainLoop()
        {
            _httpListener.Start();
            Console.WriteLine("Listening...");
            while (_keepGoing)
            {
                var context = await _httpListener.GetContextAsync();

                lock (_httpListener)
                {
                    if (_keepGoing)
                    {
                        ResponseToRequest(context);
                    }
                }
            }
        }

        private void ResponseToRequest(HttpListenerContext context)
        {
            Queue<byte> packetCame = new Queue<byte>();
            Queue<byte> packetResponse = new Queue<byte>();
            var inputStream = context.Request.InputStream;
            int next = inputStream.ReadByte();
            while (next != -1)
            {
                packetCame.Enqueue((byte) next);
                next = inputStream.ReadByte();
            }

            OnClientPacketCame(packetCame, packetResponse);
            byte[] bufferResponse = packetResponse.ToArray();

            using var response = context.Response;
            response.ContentLength64 = bufferResponse.Length;
            using Stream output = response.OutputStream;
            output.Write(bufferResponse, 0, bufferResponse.Length);
        }

        private void OnClientPacketCame(Queue<byte> packetCame, Queue<byte> packetResponse)
        {
            ClientPacketCame?.Invoke(packetCame, packetResponse);
        }
    }
}