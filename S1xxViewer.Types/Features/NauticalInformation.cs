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
    public class NauticalInformation : InformationFeatureBase, INauticalInformation
    {
        public IPeriodicDateRange PeriodicDataRange { get; set; }
        public string CategoryOfAuthority { get; set; }
        public ITextContent TextContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new NauticalInformation
            {
                PeriodicDataRange = PeriodicDataRange == null 
                    ? new PeriodicDateRange()
                    : PeriodicDataRange.DeepClone() as IPeriodicDateRange,
                CategoryOfAuthority = CategoryOfAuthority,
                TextContent = TextContent == null 
                    ? new TextContent()
                    : TextContent.DeepClone() as ITextContent
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
