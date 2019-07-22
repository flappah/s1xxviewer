using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class FrequencyPair : IFrequencyPair
    {
        public int FrequencyShoreStationReceives { get; set; }
        public int FrequencyShoreStationTransmits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new FrequencyPair
            {
                FrequencyShoreStationReceives = FrequencyShoreStationReceives,
                FrequencyShoreStationTransmits = FrequencyShoreStationTransmits
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
