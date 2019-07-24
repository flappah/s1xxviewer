using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IRadarRange : IGeoFeature
    {
        string[] Status { get; set; }

        IFeature DeepClone();
        IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}