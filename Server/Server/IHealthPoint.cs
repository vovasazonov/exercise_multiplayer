namespace ServerHP
{
    public interface IHealthPoint
    {
        uint Points { get; }
        void Take(uint amount);
    }
}