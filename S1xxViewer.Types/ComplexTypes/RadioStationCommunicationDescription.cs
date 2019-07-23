using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S1xxViewer.Types.Interfaces;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class RadioStationCommunicationDescription : IRadioStationCommunicationDescription
    {
        public string[] CategoryOfMaritimeBroadcast { get; set; }
        public string[] CommunicationChannel { get; set; }
        public int SignalFrequency { get; set; }
        public string TransmissionContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new RadioStationCommunicationDescription
            {
                CategoryOfMaritimeBroadcast = CategoryOfMaritimeBroadcast == null
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfMaritimeBroadcast, s => s),
                CommunicationChannel = CommunicationChannel == null
                    ? new string[0]
                    : Array.ConvertAll(CommunicationChannel, s => s),
                SignalFrequency = SignalFrequency,
                TransmissionContent = TransmissionContent
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var categoryOfMaritimeBroadcastNodes = node.FirstChild.SelectNodes("categoryOfMaritimeBroadcast", mgr);
            if (categoryOfMaritimeBroadcastNodes != null && categoryOfMaritimeBroadcastNodes.Count > 0)
            {
                var categories = new List<string>();
                foreach (XmlNode categoryOfMaritimeBroadcastNode in categoryOfMaritimeBroadcastNodes)
                {
                    if (categoryOfMaritimeBroadcastNode != null && categoryOfMaritimeBroadcastNode.HasChildNodes)
                    {
                        categories.Add(categoryOfMaritimeBroadcastNode.FirstChild.InnerText);
                    }
                }
                CategoryOfMaritimeBroadcast = categories.ToArray();
            }

            var communicationChannelNodes = node.FirstChild.SelectNodes("communicationChannel", mgr);
            if (communicationChannelNodes != null && communicationChannelNodes.Count > 0)
            {
                var channels = new List<string>();
                foreach (XmlNode communicationChannelNode in communicationChannelNodes)
                {
                    if (communicationChannelNode != null && communicationChannelNode.HasChildNodes)
                    {
                        channels.Add(communicationChannelNode.FirstChild.InnerText);
                    }
                }
                CategoryOfMaritimeBroadcast = channels.ToArray();
            }

            var signalFrequencyNode = node.FirstChild.SelectSingleNode("signalFrequency");
            if (signalFrequencyNode != null && signalFrequencyNode.HasChildNodes)
            {
                int frequency;
                if (!int.TryParse(signalFrequencyNode.FirstChild.InnerText, out frequency))
                {
                    frequency = 0;
                }
                SignalFrequency = frequency;
            }

            var transmissionContentNode = node.FirstChild.SelectSingleNode("transmissionContent");
            if (transmissionContentNode != null && transmissionContentNode.HasChildNodes)
            {
                TransmissionContent = transmissionContentNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
