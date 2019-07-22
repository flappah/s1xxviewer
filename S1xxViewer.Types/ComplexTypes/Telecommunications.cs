using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Telecommunications : ITelecommunications
    {
        public string TelecommunicationsIdentifier { get; set; }
        public string TelecommunicationsService { get; set; }
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

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new System.NotImplementedException();
        }
    }
}
