using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface ICoastguardStation : IGeoFeature
    {
        string[] CommunicationsChannel { get; set; }
        bool IsMRCC { get; set; }
        string[] Status { get; set; }

        IFeature DeepClone();
        IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}