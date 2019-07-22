using Esri.ArcGISRuntime.Geometry;

namespace S1xxViewer.Types.Interfaces
{
    public interface IMetaFeature : IFeature
    {
        Geometry Geometry { get; set; }
    }
}
