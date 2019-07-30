using S1xxViewer.Types.Interfaces;
using System;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TimeOfObservation : ComplexTypeBase, ITimeOfObservation
    {
        public DateTime ObservationTime { get; set; }
        public string TimeReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new TimeOfObservation
            {
                ObservationTime = ObservationTime,
                TimeReference = TimeReference
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
            var observationTimeNode = node.SelectSingleNode("observationTime");
            if (observationTimeNode != null && observationTimeNode.HasChildNodes)
            {
                DateTime observation;
                if (!DateTime.TryParse(observationTimeNode.FirstChild.InnerText, out observation))
                {
                    observation = DateTime.MinValue;
                }
                ObservationTime = observation;
            }

            var timeReferenceNode = node.SelectSingleNode("timeReference");
            if (timeReferenceNode != null && timeReferenceNode.HasChildNodes)
            {
                TimeReference = timeReferenceNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
