using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class FrequencyPair : ComplexTypeBase, IFrequencyPair
    {
        public int[] FrequencyShoreStationReceives { get; set; }
        public int[] FrequencyShoreStationTransmits { get; set; }
        public string[] ContactInstructions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new FrequencyPair
            {
                FrequencyShoreStationReceives = FrequencyShoreStationReceives == null
                    ? new int[0]
                    : Array.ConvertAll(FrequencyShoreStationReceives, i => i),
                FrequencyShoreStationTransmits = FrequencyShoreStationTransmits == null
                    ? new int[0]
                    : Array.ConvertAll(FrequencyShoreStationTransmits, i => i),
                ContactInstructions = ContactInstructions == null
                    ? new string[0]
                    : Array.ConvertAll(ContactInstructions, s => s)
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
            var frequencyShoreStationReceivesNodes = node.SelectNodes("frequencyShoreStationReceives");
            if (frequencyShoreStationReceivesNodes != null && frequencyShoreStationReceivesNodes.Count > 0)
            {
                var frequencies = new List<int>();
                foreach(XmlNode frequencyShoreStationReceivesNode in frequencyShoreStationReceivesNodes)
                {
                    if (frequencyShoreStationReceivesNode != null && frequencyShoreStationReceivesNode.HasChildNodes)
                    {
                        int frequencyShoreStationReceives;
                        if (!int.TryParse(frequencyShoreStationReceivesNode.FirstChild.InnerText, out frequencyShoreStationReceives))
                        {
                            frequencyShoreStationReceives = 0;
                        }
                        frequencies.Add(frequencyShoreStationReceives);
                    }
                }

                FrequencyShoreStationReceives = frequencies.ToArray();
            }

            var frequencyShoreStationTransmitsNodes = node.SelectNodes("frequencyShoreStationTransmits");
            if (frequencyShoreStationTransmitsNodes != null && frequencyShoreStationTransmitsNodes.Count > 0)
            {
                var frequencies = new List<int>();
                foreach (XmlNode frequencyShoreStationTransmitsNode in frequencyShoreStationTransmitsNodes)
                {
                    if (frequencyShoreStationTransmitsNode != null && frequencyShoreStationTransmitsNode.HasChildNodes)
                    {
                        int frequencyShoreStationTransmits;
                        if (!int.TryParse(frequencyShoreStationTransmitsNode.FirstChild.InnerText, out frequencyShoreStationTransmits))
                        {
                            frequencyShoreStationTransmits = 0;
                        }
                        frequencies.Add(frequencyShoreStationTransmits);
                    }
                }

                FrequencyShoreStationTransmits = frequencies.ToArray();
            }

            return this;
        }
    }
}
