using S1xxViewer.Types.Interfaces;
using System;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TmIntervalsByDoW : ComplexTypeBase, ITmIntervalsByDoW
    {
        public int DayOfWeek { get; set; }
        public bool DayOfWeekIsRange { get; set; }
        public string TimeReference { get; set; }
        public DateTime TimeOfDayStart { get; set; }
        public DateTime TimeOfDayEnd { get; set; }

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
                TimeOfDayEnd = TimeOfDayEnd,
                TimeOfDayStart = TimeOfDayStart
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

            var timeOfDayStartNode = node.SelectSingleNode("timeOfDayStart", mgr);
            if (timeOfDayStartNode != null && timeOfDayStartNode.HasChildNodes)
            {
                DateTime timeOfDayStart;
                if (!DateTime.TryParse(timeOfDayStartNode.FirstChild.InnerText, out timeOfDayStart))
                {
                    timeOfDayStart = DateTime.MinValue;
                }
                TimeOfDayStart = timeOfDayStart;
            }

            var timeOfDayEndNode = node.SelectSingleNode("timeOfDayEnd", mgr);
            if (timeOfDayEndNode != null && timeOfDayEndNode.HasChildNodes)
            {
                DateTime timeOfDayEnd;
                if (!DateTime.TryParse(timeOfDayEndNode.FirstChild.InnerText, out timeOfDayEnd))
                {
                    timeOfDayEnd = DateTime.MinValue;
                }
                TimeOfDayEnd = timeOfDayEnd;
            }

            return this;
        }
    }
}
