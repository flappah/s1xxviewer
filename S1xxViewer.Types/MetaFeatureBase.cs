using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types
{
    public abstract class MetaFeatureBase : FeatureBase, IMetaFeature
    {
        public Geometry Geometry { get; set; }
    }
}
