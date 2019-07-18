using Esri.ArcGISRuntime.Geometry;

namespace S1xxViewer.Types.Interfaces
{
    public interface IGeoFeature : IFeature
    {
        Geometry Geometry { get; set; }
    }
}