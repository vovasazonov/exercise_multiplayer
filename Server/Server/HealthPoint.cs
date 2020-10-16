namespace ServerHP
{
    public class HealthPoint : IHealthPoint
    {
        private uint _points;
        public uint Points => _points;

        public HealthPoint(uint amount)
        {
            _points = amount;
        }
        
        public void Take(uint amount)
        {
            _points = _points > amount ? _points - amount : 0;
        }
    }
}