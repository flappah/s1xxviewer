using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class OnlineResources : IOnlineResources
    {
        public string Linkage { get; set; }
        public string NameOfResource { get; set; }
        public string OnlineDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new OnlineResources
            {
                Linkage = Linkage,
                NameOfResource = NameOfResource,
                OnlineDescription = OnlineDescription
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
