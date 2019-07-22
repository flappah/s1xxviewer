using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types
{
    public abstract class GeoFeatureBase : IGeoFeature
    {
        public InternationalString[] FeatureName { get; set; }
        public IDateRange FixedDateRange { get; set; }
        public string Id { get; set; }
        public IDateRange PeriodicDateRange { get; set; }
        public ISourceIndication SourceIndication { get; set; }
        public ITextContent TextContent { get; set; }

        public Geometry Geometry { get; set; }

        // linkages
        public ILink[] Links { get; set; }

        public abstract IFeature DeepClone();
        public abstract IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
