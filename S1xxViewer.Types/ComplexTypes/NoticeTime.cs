using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class NoticeTime : ComplexTypeBase, INoticeTime
    {
        public int NoticeTimeHours { get; set; }
        public string NoticeTimeText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new NoticeTime
            {
                NoticeTimeHours = NoticeTimeHours,
                NoticeTimeText = NoticeTimeText ?? ""
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var noticeTimeHoursNode = node.FirstChild.SelectSingleNode("noticeTimeHours");
            if (noticeTimeHoursNode != null && noticeTimeHoursNode.HasChildNodes)
            {
                int noticeTimeHours;
                if (!int.TryParse(noticeTimeHoursNode.FirstChild.InnerText, out noticeTimeHours))
                {
                    noticeTimeHours = 0;
                }
                NoticeTimeHours = noticeTimeHours;

                var noticeTimeTextNode = node.FirstChild.SelectSingleNode("noticeTimeText", mgr);
                if (noticeTimeTextNode != null && noticeTimeTextNode.HasChildNodes)
                {
                    NoticeTimeText = noticeTimeTextNode.FirstChild.InnerText;
                }
            }

            return this;
        }
    }
}
