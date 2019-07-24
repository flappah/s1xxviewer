﻿namespace S1xxViewer.Types.Interfaces
{
    public interface IRouteingMeasure : IGeoFeature
    {
        string CategoryOfNavigationLine { get; set; }
        string CategoryOfRouteingMeasure { get; set; }
        string CategoryOfTrafficSeparationScheme { get; set; }
    }
}