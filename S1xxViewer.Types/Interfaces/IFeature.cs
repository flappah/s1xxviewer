using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IFeature
    {
        string Id { get; set; }

        IFeature DeepClone();
        IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
