using S1xxViewer.Types.Interfaces;
using System.Xml;
using S1xxViewer.Types;
using Esri.ArcGISRuntime.Geometry;

namespace S1xxViewer.Types
{
    public class S1xxDataPackage : IS1xxDataPackage
    {
        public S1xxTypes Type { get; set; }
        public IMetaFeature[] MetaFeatures { get; set; }
        public IInformationFeature[] InformationFeatures { get; set; }
        public IGeoFeature[] GeoFeatures { get; set; }
        public Geometry BoundingBox { get; set; }
        public XmlDocument RawData { get; set; }
    }
}
