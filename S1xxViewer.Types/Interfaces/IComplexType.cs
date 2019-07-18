using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IComplexType
    {
        IComplexType DeepClone();
        IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
