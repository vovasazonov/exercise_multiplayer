using System.Threading.Tasks;

namespace Client
{
    public interface IClient
    {
        Task<string> PostRequest(string request);
    }
}