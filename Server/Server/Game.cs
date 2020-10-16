namespace ServerHP
{
    public class Game
    {
        private readonly IHealthPoint _hp = new HealthPoint(100);
        
        public string ProcessRequest(string request)
        {
            return GetResponseToRequest(request);
        }

        private string GetResponseToRequest(string request)
        {
            string response = "";

            if (request.Contains("take_hp: "))
            {
                var amountTake = uint.Parse(request.Remove(0, "take_hp: ".Length));
                _hp.Take(amountTake);
                response = _hp.Points.ToString();
            }

            return response;
        }
    }
}