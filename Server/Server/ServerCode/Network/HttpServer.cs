using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Server.Network
{
    public class HttpServer : IServer
    {
        public event Action<Queue<byte>, Queue<byte>> ClientPacketCame;
        
        private readonly HttpListener _httpListener = new HttpListener {Prefixes = {"http://localhost:8888/"}};
        private bool _keepGoing = true;
        private Task _mainLoop;

        public void Start()
        {
            bool isPossibleStartMainLoop = !(_mainLoop != null && !_mainLoop.IsCompleted);

            if (isPossibleStartMainLoop)
            {
                _mainLoop = MainLoop();
            }
        }

        public void Stop()
        {
            _keepGoing = false;
            lock (_httpListener)
            {
                _httpListener.Stop();
            }

            _mainLoop.Wait();
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
                packetCame.Enqueue((byte)next);
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