using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface ILink
    {
        string Href { get; set; }
        string ArcRole { get; set; }
        string Name { get; set; }

        ILink DeepClone();
        ILink FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
