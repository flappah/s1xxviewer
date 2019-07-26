using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class FrequencyPair : ComplexTypeBase, IFrequencyPair
    {
        public int FrequencyShoreStationReceives { get; set; }
        public int FrequencyShoreStationTransmits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new FrequencyPair
            {
                FrequencyShoreStationReceives = FrequencyShoreStationReceives,
                FrequencyShoreStationTransmits = FrequencyShoreStationTransmits
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
            var frequencyShoreStationReceivesNode = node.FirstChild.SelectSingleNode("frequencyShoreStationReceives");
            if (frequencyShoreStationReceivesNode != null && frequencyShoreStationReceivesNode.HasChildNodes)
            {
                int frequencyShoreStationReceives;
                if (!int.TryParse(frequencyShoreStationReceivesNode.FirstChild.InnerText, out frequencyShoreStationReceives))
                {
                    frequencyShoreStationReceives = 0;
                }
                FrequencyShoreStationReceives = frequencyShoreStationReceives;
            }

            var frequencyShoreStationTransmitsNode = node.FirstChild.SelectSingleNode("frequencyShoreStationTransmits");
            if (frequencyShoreStationTransmitsNode != null && frequencyShoreStationTransmitsNode.HasChildNodes)
            {
                int frequencyShoreStationTransmits;
                if (!int.TryParse(frequencyShoreStationTransmitsNode.FirstChild.InnerText, out frequencyShoreStationTransmits))
                {
                    frequencyShoreStationTransmits = 0;
                }
                FrequencyShoreStationTransmits = frequencyShoreStationTransmits;
            }

            return this;
        }
    }
}
