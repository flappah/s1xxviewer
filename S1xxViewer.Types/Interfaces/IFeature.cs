using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IFeature
    {
        string Id { get; set; }

        ILink[] Links { get; set; }

        IFeature DeepClone();
        IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
        Dictionary<string, string> SerializeProperties();
    }
}
