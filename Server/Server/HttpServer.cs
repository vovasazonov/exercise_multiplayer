using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerHP
{
    public class HttpServer : IServer
    {
        private readonly HttpListener _httpListener = new HttpListener {Prefixes = {"http://localhost:8888/"}};
        private bool _keepGoing = true;
        private Task _mainLoop;
        public Func<string, string> ProcessRequest { private get; set; }

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
            while (_keepGoing)
            {
                Console.WriteLine("Listening...");
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
            var request = context.Request;
            using var response = context.Response;
            using StreamReader requestStreamReader = new StreamReader(request.InputStream, Encoding.UTF8);

            var responseString = ProcessRequest?.Invoke(requestStreamReader.ReadToEnd()) ?? "";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }
    }
}