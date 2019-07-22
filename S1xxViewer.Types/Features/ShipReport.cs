using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Features
{
    public class ShipReport : InformationFeatureBase, IShipReport
    {
        public string CategoryOfShipReport { get; set; }
        public bool ImoFormatForReporting { get; set; }
        public INoticeTime NoticeTime { get; set; }
        public ITextContent TextContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new ShipReport
            {
                CategoryOfShipReport = CategoryOfShipReport,
                ImoFormatForReporting = ImoFormatForReporting,
                NoticeTime = NoticeTime == null
                    ? new NoticeTime()
                    : NoticeTime.DeepClone() as INoticeTime,
                TextContent = TextContent == null
                    ? new TextContent()
                    : TextContent.DeepClone() as ITextContent
            };
        }

        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
