using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class CustomHttpClient : IClient
    {
        private readonly string _url = "http://localhost:8888/";
        
        public async Task<string> PostRequest(string request)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(request,Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_url, stringContent);
                
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}