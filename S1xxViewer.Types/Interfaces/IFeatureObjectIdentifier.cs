using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IFeatureObjectIdentifier
    {
        string Agency { get; set; }
        int FeatureIdentificationNumber { get; set; }
        int FeatureIdentificationSubdivision { get; set; }

        IFeatureObjectIdentifier DeepClone();
        IFeatureObjectIdentifier FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
