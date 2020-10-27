using Server.Network;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = new UdpServer();
        }
    }
}