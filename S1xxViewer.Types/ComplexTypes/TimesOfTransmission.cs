using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TimesOfTransmission : ComplexTypeBase, ITimesOfTransmission
    {
        public int MinutePastEvenHours { get; set; }
        public int MinutePastEveryHours { get; set; }
        public int MinutePastOddHours { get; set; }
        public string TimeReference { get; set; }
        public string[] TransmissionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new TimesOfTransmission
            {
                MinutePastEvenHours = MinutePastEvenHours,
                MinutePastEveryHours = MinutePastEveryHours,
                MinutePastOddHours = MinutePastOddHours,
                TimeReference = TimeReference,
                TransmissionTime = TransmissionTime == null
                    ? new string[0]
                    : Array.ConvertAll(TransmissionTime, s => s)
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
            var minutePastEvenHoursNode = node.SelectSingleNode("minutePastEvenHours");
            if (minutePastEvenHoursNode != null && minutePastEvenHoursNode.HasChildNodes)
            {
                int minuteValue;
                if (!int.TryParse(minutePastEvenHoursNode.FirstChild.InnerText, out minuteValue))
                {
                    minuteValue = 0;
                }
                MinutePastEvenHours = minuteValue;
            }

            var minutePastEveryHoursNode = node.SelectSingleNode("minutePastEveryHours");
            if (minutePastEveryHoursNode != null && minutePastEveryHoursNode.HasChildNodes)
            {
                int minuteValue;
                if (!int.TryParse(minutePastEveryHoursNode.FirstChild.InnerText, out minuteValue))
                {
                    minuteValue = 0;
                }
                MinutePastEveryHours = minuteValue;
            }

            var minutePastOddHoursNode = node.SelectSingleNode("minutePastOddHours");
            if (minutePastOddHoursNode != null && minutePastOddHoursNode.HasChildNodes)
            {
                int minuteValue;
                if (!int.TryParse(minutePastOddHoursNode.FirstChild.InnerText, out minuteValue))
                {
                    minuteValue = 0;
                }
                MinutePastOddHours = minuteValue;
            }

            var timeReferenceNode = node.SelectSingleNode("timeReference");
            if (timeReferenceNode != null && timeReferenceNode.HasChildNodes)
            {
                TimeReference = timeReferenceNode.FirstChild.InnerText;
            }

            var transmissionTimeNodes = node.SelectNodes("transmissionTime");
            if (transmissionTimeNodes != null && transmissionTimeNodes.Count > 0)
            {
                var transmissionTimes = new List<string>();
                foreach(XmlNode transmissionTimeNode in transmissionTimeNodes)
                {
                    if (transmissionTimeNode != null && transmissionTimeNode.HasChildNodes)
                    {
                        transmissionTimes.Add(transmissionTimeNode.FirstChild.InnerText);
                    }
                }
                TransmissionTime = transmissionTimes.ToArray();
            }

            return this;
        }
    }
}
