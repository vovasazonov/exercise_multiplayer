namespace Models.Characters
{
    public class HealthPointData : IHealthPointData
    {
        public uint MaxPoints { get; set; }
        public uint Points { get; set; }
    }
}