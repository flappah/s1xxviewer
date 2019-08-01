﻿using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class NoticeTime : ComplexTypeBase, INoticeTime
    {
        public double[] NoticeTimeHours { get; set; }
        public string NoticeTimeText { get; set; }
        public string Operation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new NoticeTime
            {
                NoticeTimeHours = NoticeTimeHours == null 
                ? new double[0]
                : Array.ConvertAll(NoticeTimeHours, n => n),
                NoticeTimeText = NoticeTimeText ?? "",
                Operation= Operation
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
            var noticeTimeHoursNodes = node.SelectNodes("noticeTimeHours");
            if (noticeTimeHoursNodes != null && noticeTimeHoursNodes.Count > 0)
            {
                var noticeHours = new List<double>();
                foreach (XmlNode noticeTimeHoursNode in noticeTimeHoursNodes)
                {
                    if (noticeTimeHoursNode != null && noticeTimeHoursNode.HasChildNodes)
                    {
                        double noticeTimeHours;
                        if (!double.TryParse(noticeTimeHoursNode.FirstChild.InnerText, out noticeTimeHours))
                        {
                            noticeTimeHours = 0.0;
                        }
                        noticeHours.Add(noticeTimeHours);                        
                    }
                }
                NoticeTimeHours = noticeHours.ToArray();
            }

            var noticeTimeTextNode = node.SelectSingleNode("noticeTimeText", mgr);
            if (noticeTimeTextNode != null && noticeTimeTextNode.HasChildNodes)
            {
                NoticeTimeText = noticeTimeTextNode.FirstChild.InnerText;
            }

            var operationNode = node.SelectSingleNode("operation", mgr);
            if (operationNode != null && operationNode.HasChildNodes)
            {
                Operation = operationNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
