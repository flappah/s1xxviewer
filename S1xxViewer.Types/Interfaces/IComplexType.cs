using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IComplexType
    {
        IComplexType DeepClone();
        IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr);
        Dictionary<string, string> GetData();
    }
}
