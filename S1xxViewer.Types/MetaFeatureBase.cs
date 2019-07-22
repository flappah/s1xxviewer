using S1xxViewer.Types.Interfaces;
using System.Xml;
using System;
using Esri.ArcGISRuntime.Geometry;

namespace S1xxViewer.Types
{
    public abstract class MetaFeatureBase : IMetaFeature
    {
        public string Id { get; set; }
        public Geometry Geometry { get; set; }

        // linkages
        public ILink[] Links { get; set; }

        public abstract IFeature DeepClone();
        public abstract IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
