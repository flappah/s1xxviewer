using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class OnlineResource : IOnlineResource
    {
        public string ApplicationProfile { get; set; }
        public string Linkage { get; set; }
        public string NameOfResource { get; set; }
        public string OnlineDescription { get; set; }
        public string OnlineFunction { get; set; }
        public string Protocol { get; set; }
        public string ProtocolRequest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new OnlineResource
            {
                ApplicationProfile = ApplicationProfile,
                Linkage = Linkage,
                NameOfResource = NameOfResource,
                OnlineDescription = OnlineDescription,
                OnlineFunction = OnlineFunction,
                Protocol = Protocol,
                ProtocolRequest  = ProtocolRequest
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            return this;
        }
    }
}
