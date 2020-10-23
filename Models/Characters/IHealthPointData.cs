namespace Models.Characters
{
    public interface IHealthPointData
    {
        uint MaxPoints { get; set; }
        uint Points { get; set; }
    }
}