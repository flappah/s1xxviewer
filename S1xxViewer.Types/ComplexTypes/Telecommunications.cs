using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Telecommunications : ITelecommunications
    {
        public string TelecommunicationsIdentifier { get; set; }
        public string TelecommunicationsService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new Telecommunications
            {
                TelecommunicationsIdentifier = TelecommunicationsIdentifier,
                TelecommunicationsService = TelecommunicationsService
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new System.NotImplementedException();
        }
    }
}
