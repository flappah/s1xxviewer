using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TmIntervalsByDoW : ComplexTypeBase, ITmIntervalsByDoW
    {
        public string DayOfWeek { get; set; }
        public bool DayOfWeekIsRange { get; set; }
        public string TimeReference { get; set; }
        public string[] TimeOfDayStart { get; set; }
        public string[] TimeOfDayEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new TmIntervalsByDoW
            {
                DayOfWeek = DayOfWeek,
                DayOfWeekIsRange = DayOfWeekIsRange,
                TimeReference = TimeReference,
                TimeOfDayEnd = TimeOfDayEnd == null
                    ? new string[0]
                    : Array.ConvertAll(TimeOfDayEnd, t => t),
                TimeOfDayStart = TimeOfDayStart == null
                    ? new string[0]
                    : Array.ConvertAll(TimeOfDayStart, t => t)
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
            var dayOfWeekNode = node.SelectSingleNode("dayOfWeek");
            if (dayOfWeekNode != null && dayOfWeekNode.HasChildNodes)
            {
                DayOfWeek = dayOfWeekNode.FirstChild.InnerText;
            }

            var dayOfWeekIsRangeNode = node.SelectSingleNode("dayOfWeekIsRange", mgr);
            if (dayOfWeekIsRangeNode != null && dayOfWeekIsRangeNode.HasChildNodes)
            {
                bool dayOfWeekIsRange;
                if (bool.TryParse(dayOfWeekIsRangeNode.FirstChild.InnerText, out dayOfWeekIsRange))
                {
                    dayOfWeekIsRange = false;
                }
                DayOfWeekIsRange = dayOfWeekIsRange;
            }

            var timeReferenceNode = node.SelectSingleNode("timeReference", mgr);
            if (timeReferenceNode != null && timeReferenceNode.HasChildNodes)
            {
                TimeReference = timeReferenceNode.FirstChild.InnerText;
            }

            var timeOfDayStartNodes = node.SelectNodes("timeOfDayStart", mgr);
            if (timeOfDayStartNodes != null && timeOfDayStartNodes.Count > 0)
            {
                var times = new List<string>();
                foreach (XmlNode timeOfDayStartNode in timeOfDayStartNodes)
                {
                    if (timeOfDayStartNode != null && timeOfDayStartNode.HasChildNodes)
                    {
                        string timeOfDayStart = timeOfDayStartNode.FirstChild.InnerText;
                        times.Add(timeOfDayStart);
                    }
                }
                TimeOfDayStart = times.ToArray(); 
            }

            var timeOfDayEndNodes = node.SelectNodes("timeOfDayEnd", mgr);
            if (timeOfDayEndNodes != null && timeOfDayEndNodes.Count > 0)
            {
                var times = new List<string>();
                foreach (XmlNode timeOfDayEndNode in timeOfDayEndNodes)
                {
                    if (timeOfDayEndNode != null && timeOfDayEndNode.HasChildNodes)
                    {
                        string timeOfDayEnd = timeOfDayEndNode.FirstChild.InnerText;
                        times.Add(timeOfDayEnd);
                    }
                }
                TimeOfDayEnd = times.ToArray(); 
            }

            return this;
        }
    }
}
