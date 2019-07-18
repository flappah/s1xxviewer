using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IRegulations
    {
        string CategoryOfAuthority { get; set; }
        IOnlineResources OnlineResources { get; set; }
        IPeriodicDateRange PeriodicDateRange { get; set; }
        IRxnCode[] RxnCode { get; set; }
        ITextContent TextContent { get; set; }

        IFeature DeepClone();
        IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}