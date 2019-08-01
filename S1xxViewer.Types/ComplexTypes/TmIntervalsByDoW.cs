using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TmIntervalsByDoW : ComplexTypeBase, ITmIntervalsByDoW
    {
        public int DayOfWeek { get; set; }
        public bool DayOfWeekIsRange { get; set; }
        public string TimeReference { get; set; }
        public DateTime[] TimeOfDayStart { get; set; }
        public DateTime[] TimeOfDayEnd { get; set; }

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
                    ? new DateTime[0]
                    : Array.ConvertAll(TimeOfDayEnd, t => t),
                TimeOfDayStart = TimeOfDayStart == null
                    ? new DateTime[0]
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
                int dayOfWeek;
                if (!int.TryParse(dayOfWeekNode.FirstChild.InnerText, out dayOfWeek))
                {
                    dayOfWeek = 0;
                }
                DayOfWeek = dayOfWeek;
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
                var times = new List<DateTime>();
                foreach (XmlNode timeOfDayStartNode in timeOfDayStartNodes)
                {
                    if (timeOfDayStartNode != null && timeOfDayStartNode.HasChildNodes)
                    {
                        DateTime timeOfDayStart;
                        if (!DateTime.TryParse(timeOfDayStartNode.FirstChild.InnerText, out timeOfDayStart))
                        {
                            timeOfDayStart = DateTime.MinValue;
                        }
                        times.Add(timeOfDayStart);
                    }
                }
                TimeOfDayStart = times.ToArray(); 
            }

            var timeOfDayEndNodes = node.SelectNodes("timeOfDayEnd", mgr);
            if (timeOfDayEndNodes != null && timeOfDayEndNodes.Count > 0)
            {
                var times = new List<DateTime>();
                foreach (XmlNode timeOfDayEndNode in timeOfDayStartNodes)
                {
                    if (timeOfDayEndNode != null && timeOfDayEndNode.HasChildNodes)
                    {
                        DateTime timeOfDayEnd;
                        if (!DateTime.TryParse(timeOfDayEndNode.FirstChild.InnerText, out timeOfDayEnd))
                        {
                            timeOfDayEnd = DateTime.MinValue;
                        }
                        times.Add(timeOfDayEnd);
                    }
                }
                TimeOfDayEnd = times.ToArray(); 
            }

            return this;
        }
    }
}
