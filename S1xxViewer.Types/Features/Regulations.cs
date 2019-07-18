using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Features
{
    public class Regulations : InformationFeatureBase, IRegulations
    {
        public string CategoryOfAuthority { get; set; }
        public IOnlineResources OnlineResources { get; set; }
        public IPeriodicDateRange PeriodicDateRange { get; set; }
        public IRxnCode[] RxnCode { get; set; }
        public ITextContent TextContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new Regulations
            {
                CategoryOfAuthority = CategoryOfAuthority ?? "",
                OnlineResources = OnlineResources == null 
                    ? new OnlineResources()
                    : OnlineResources.DeepClone() as IOnlineResources,
                PeriodicDateRange = PeriodicDateRange == null
                    ? new PeriodicDateRange()
                    : PeriodicDateRange.DeepClone() as IPeriodicDateRange,
                RxnCode = RxnCode == null
                    ? new[] { new RxnCode() }
                    : Array.ConvertAll(RxnCode, r => r.DeepClone() as IRxnCode),
                TextContent = TextContent.DeepClone() as ITextContent,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                if (node.FirstChild.Attributes.Count > 0)
                {
                    Id = node.FirstChild.Attributes["gml:id"].InnerText;
                }
            }

            return this;
        }
    }
}
