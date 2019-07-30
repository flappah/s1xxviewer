using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class RadioCommunications : ComplexTypeBase, IRadioCommunications
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
        public override IComplexType DeepClone()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var categoryOfCommPrefNode = node.SelectSingleNode("categoryOfCommPref", mgr);
            if (categoryOfCommPrefNode != null && categoryOfCommPrefNode.HasChildNodes)
            {
                CategoryOfCommPref = categoryOfCommPrefNode.FirstChild.InnerText;
            }

            var communicationChannelNode = node.SelectSingleNode("communicationChannel", mgr);
            if (communicationChannelNode != null && communicationChannelNode.HasChildNodes)
            {
                CommunicationChannel = communicationChannelNode.FirstChild.InnerText;
            }

            var contactInstructionsNode = node.SelectSingleNode("contactInstructions", mgr);
            if (contactInstructionsNode != null && contactInstructionsNode.HasChildNodes)
            {
                ContactInstructions = contactInstructionsNode.FirstChild.InnerText;
            }

            var tmIntervalsByDoWNodes = node.SelectNodes("tmIntervalsByDoW", mgr);
            if (tmIntervalsByDoWNodes != null && tmIntervalsByDoWNodes.Count > 0)
            {
                var tmIntervals = new List<TmIntervalsByDoW>();
                foreach(XmlNode tmIntervalsByDoWNode in tmIntervalsByDoWNodes)
                {
                    if (tmIntervalsByDoWNode != null && tmIntervalsByDoWNode.HasChildNodes)
                    {
                        var newTmInterval = new TmIntervalsByDoW();
                        newTmInterval.FromXml(tmIntervalsByDoWNode, mgr);
                        tmIntervals.Add(newTmInterval);
                    }
                }
                TmIntervalsByDoW = tmIntervals.ToArray();
            }

            var frequencyPairNodes = node.SelectNodes("frequencyPair", mgr);
            if (frequencyPairNodes != null && frequencyPairNodes.Count > 0)
            {
                var frequencyPairs = new List<FrequencyPair>();
                foreach (XmlNode frequencyPairNode in frequencyPairNodes)
                {
                    if (frequencyPairNode != null && frequencyPairNode.HasChildNodes)
                    {
                        var newFrequencyPair = new FrequencyPair();
                        newFrequencyPair.FromXml(frequencyPairNode, mgr);
                        frequencyPairs.Add(newFrequencyPair);
                    }
                }
                FrequencyPair = frequencyPairs.ToArray();
            }

            return this;
        }
    }
}
