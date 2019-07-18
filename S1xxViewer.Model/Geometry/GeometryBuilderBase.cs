using S1xxViewer.Model.Interfaces;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public abstract class GeometryBuilderBase : IGeometryBuilder
    {
        protected static int _spatialReferenceSystem;

        public abstract Esri.ArcGISRuntime.Geometry.Geometry FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
