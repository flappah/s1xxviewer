using S1xxViewer.Model.Interfaces;
using S1xxViewer.Storage.Interfaces;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public abstract class GeometryBuilderBase : IGeometryBuilder
    {
        protected static int _spatialReferenceSystem;
        protected IOptionsStorage _optionsStorage;

        public abstract Esri.ArcGISRuntime.Geometry.Geometry FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
