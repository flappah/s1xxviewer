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
    public class Restrictions : InformationFeatureBase, IRestrictions
    {
        public string CategoryOfAuthority { get; set; }
        public string[] Graphic { get; set; }
        public IRxnCode[] RxnCode { get; set; }
        public ITextContent[] TextContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new Regulations
            {
                CategoryOfAuthority = CategoryOfAuthority ?? "",
                Graphic = Graphic == null
                    ? new string[0]
                    : Array.ConvertAll(Graphic, g => g),
                RxnCode = RxnCode == null
                    ? new RxnCode[0]
                    : Array.ConvertAll(RxnCode, r => r.DeepClone() as IRxnCode),
                TextContent = TextContent == null
                    ? new TextContent[0]
                    : Array.ConvertAll(TextContent, t => t.DeepClone() as ITextContent),
                FeatureName = FeatureName == null
                    ? new InternationalString[0]
                    : Array.ConvertAll(FeatureName, s => s),
                FixedDateRange = FixedDateRange == null
                    ? new DateRange()
                    : FixedDateRange.DeepClone() as IDateRange,
                PeriodicDateRange = PeriodicDateRange == null
                    ? new DateRange[0]
                    : Array.ConvertAll(PeriodicDateRange, p => p.DeepClone() as IDateRange),
                SourceIndication = SourceIndication == null
                    ? new SourceIndication[0]
                    : Array.ConvertAll(SourceIndication, s => s.DeepClone() as ISourceIndication)
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
