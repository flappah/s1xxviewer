using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class RadioCommunications : IRadioCommunications
    {
        public string CategoryOfCommPref { get; set; }
        public string CommunicationChannel { get; set; }
        public string ContactInstructions { get; set; }
        public IFrequencyPair[] FrequencyPair { get; set; }
        public ITmIntervalsByDoW[] TmIntervalsByDoW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new RadioCommunications
            {
                CategoryOfCommPref = CategoryOfCommPref,
                CommunicationChannel = CommunicationChannel,
                ContactInstructions = ContactInstructions,
                FrequencyPair = FrequencyPair == null
                    ? new FrequencyPair[0]
                    : Array.ConvertAll(FrequencyPair, f => f.DeepClone() as IFrequencyPair),
                TmIntervalsByDoW = TmIntervalsByDoW == null
                    ? new TmIntervalsByDoW[0]
                    : Array.ConvertAll(TmIntervalsByDoW, t => t.DeepClone() as ITmIntervalsByDoW)
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
