using System.Xml;

namespace S1xxViewer.Model.Interfaces
{
    public interface IGeometryBuilder
    {
        Esri.ArcGISRuntime.Geometry.Geometry FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
