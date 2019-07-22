using S1xxViewer.Types.Interfaces;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Telecommunications : ITelecommunications
    {
        public string TelecommunicationsIdentifier { get; set; }
        public string[] TelecommunicationsService { get; set; }
        public string CategoryOfCommPref { get; set; }
        public string ContactInstructions { get; set; }
        public string TelcomCarrier { get; set; }
        public IScheduleByDoW ScheduleByDoW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new Telecommunications
            {
                CategoryOfCommPref = CategoryOfCommPref,
                ContactInstructions = ContactInstructions,
                ScheduleByDoW = ScheduleByDoW == null   
                    ? new ScheduleByDoW()
                    : ScheduleByDoW.DeepClone() as IScheduleByDoW,
                TelcomCarrier = TelcomCarrier,
                TelecommunicationsIdentifier = TelecommunicationsIdentifier,
                TelecommunicationsService = TelecommunicationsService
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
            var telecommunicationsIdentifierNode = node.FirstChild.SelectSingleNode("telecommunicationsIdentifier", mgr);
            if (telecommunicationsIdentifierNode != null && telecommunicationsIdentifierNode.HasChildNodes)
            {
                TelecommunicationsIdentifier = telecommunicationsIdentifierNode.FirstChild.InnerText;
            }

            var telecommunicationsServiceNodes = node.FirstChild.SelectNodes("telecommunicationsService", mgr);
            if (telecommunicationsServiceNodes != null && telecommunicationsServiceNodes.Count > 0)
            {
                var services = new List<string>();
                foreach (XmlNode telecommunicationsServiceNode in telecommunicationsServiceNodes)
                {
                    if (telecommunicationsServiceNode != null && telecommunicationsServiceNode.HasChildNodes)
                    {
                        services.Add(telecommunicationsServiceNode.InnerText);
                    }
                }
                TelecommunicationsService = services.ToArray();
            }

            var categoryOfCommPrefNode = node.FirstChild.SelectSingleNode("categoryOfCommPref", mgr);
            if (categoryOfCommPrefNode != null && categoryOfCommPrefNode.HasChildNodes)
            {
                CategoryOfCommPref = categoryOfCommPrefNode.FirstChild.InnerText;
            }

            var contactInstructionsNode = node.FirstChild.SelectSingleNode("contactInstructions", mgr);
            if (contactInstructionsNode != null && contactInstructionsNode.HasChildNodes)
            {
                ContactInstructions = contactInstructionsNode.FirstChild.InnerText;
            }

            var telcomCarrierNode = node.FirstChild.SelectSingleNode("telcomCarrier", mgr);
            if (telcomCarrierNode != null && telcomCarrierNode.HasChildNodes)
            {
                TelcomCarrier = telcomCarrierNode.FirstChild.InnerText;
            }

            var scheduleByDoWNode = node.FirstChild.SelectSingleNode("scheduleByDoW", mgr);
            if (scheduleByDoWNode != null && scheduleByDoWNode.HasChildNodes)
            {
                ScheduleByDoW = new ScheduleByDoW();
                ScheduleByDoW.FromXml(node.FirstChild, mgr);
            }

            return this;
        }
    }
}
