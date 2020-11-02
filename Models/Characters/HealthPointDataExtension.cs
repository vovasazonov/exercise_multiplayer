namespace Models.Characters
{
    public static class HealthPointDataExtension
    {
        public static void Set(this IHealthPointData data, IHealthPointModel model)
        {
            data.Points = model.Points;
            data.MaxPoints = model.MaxPoints;
        }
    }
}