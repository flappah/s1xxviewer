using System.Data;
using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IFeature
    {
        IFeatureObjectIdentifier FeatureObjectIdentifier { get; set; }
        string Id { get; set; }

        ILink[] Links { get; set; }

        IFeature DeepClone();
        IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
        DataTable GetData();
    }
}
