using System;

namespace Replications
{
    public interface IHealthData
    {
        event EventHandler CurrentPointsUpdated;
        int CurrentPoints { get; set; }
    }
}