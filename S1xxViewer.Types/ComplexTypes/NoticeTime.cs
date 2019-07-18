using S1xxViewer.Types.Interfaces;
using System;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class NoticeTime : INoticeTime
    {
        public int NoticeTimeHours { get; set; }
        public string NoticeTimeText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
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
        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
