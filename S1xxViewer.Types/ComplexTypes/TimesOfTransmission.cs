using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TimesOfTransmission : ITimesOfTransmission
    {
        public int MinutePastEvenHours { get; set; }
        public int MinutePastEveryHours { get; set; }
        public int MinutePastOddHours { get; set; }
        public string TimeReference { get; set; }
        public DateTime[] TransmissionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new TimesOfTransmission
            {
                MinutePastEvenHours = MinutePastEvenHours,
                MinutePastEveryHours = MinutePastEveryHours,
                MinutePastOddHours = MinutePastOddHours,
                TimeReference = TimeReference,
                TransmissionTime = TransmissionTime == null
                    ? new DateTime[0]
                    : Array.ConvertAll(TransmissionTime, tt => tt)
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
            var minutePastEvenHoursNode = node.FirstChild.SelectSingleNode("minutePastEvenHours");
            if (minutePastEvenHoursNode != null && minutePastEvenHoursNode.HasChildNodes)
            {
                int minuteValue;
                if (!int.TryParse(minutePastEvenHoursNode.FirstChild.InnerText, out minuteValue))
                {
                    minuteValue = 0;
                }
                MinutePastEvenHours = minuteValue;
            }

            var minutePastEveryHoursNode = node.FirstChild.SelectSingleNode("minutePastEveryHours");
            if (minutePastEveryHoursNode != null && minutePastEveryHoursNode.HasChildNodes)
            {
                int minuteValue;
                if (!int.TryParse(minutePastEveryHoursNode.FirstChild.InnerText, out minuteValue))
                {
                    minuteValue = 0;
                }
                MinutePastEveryHours = minuteValue;
            }

            var minutePastOddHoursNode = node.FirstChild.SelectSingleNode("minutePastOddHours");
            if (minutePastOddHoursNode != null && minutePastOddHoursNode.HasChildNodes)
            {
                int minuteValue;
                if (!int.TryParse(minutePastOddHoursNode.FirstChild.InnerText, out minuteValue))
                {
                    minuteValue = 0;
                }
                MinutePastOddHours = minuteValue;
            }

            var timeReferenceNode = node.FirstChild.SelectSingleNode("timeReference");
            if (timeReferenceNode != null && timeReferenceNode.HasChildNodes)
            {
                TimeReference = timeReferenceNode.FirstChild.InnerText;
            }

            var transmissionTimeNodes = node.FirstChild.SelectNodes("transmissionTime");
            if (transmissionTimeNodes != null && transmissionTimeNodes.Count > 0)
            {
                var transmissionTimes = new List<DateTime>();
                foreach(XmlNode transmissionTimeNode in transmissionTimeNodes)
                {
                    if (transmissionTimeNode != null && transmissionTimeNode.HasChildNodes)
                    {
                        DateTime transmissionTime;
                        if (!DateTime.TryParse(transmissionTimeNode.FirstChild.InnerText, out transmissionTime))
                        {
                            transmissionTime = DateTime.MinValue;
                        }
                        transmissionTimes.Add(transmissionTime);
                    }
                }
                TransmissionTime = transmissionTimes.ToArray();
            }

            return this;
        }
    }
}
