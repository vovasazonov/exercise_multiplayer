using System.Collections.Generic;

namespace Network
{
    public class ClientNetworkInfo
    {
        private int? _id = null;
        public readonly Queue<byte> NotSentCommandsToServer = new Queue<byte>();
        
        public int Id
        {
            get => _id.Value;
            set
            {
                if (_id == null)
                {
                    _id = value;
                }
            }
        }
    }
}