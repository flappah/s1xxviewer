using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Model.Interfaces
{
    public interface IFeatureFactory
    {
        IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);        
    }
}