using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class VesselsMeasurement : IVesselsMeasurement
    {
        public string ComparisonOperator { get; set; }
        public string VesselsCharacteristics { get; set; }
        public string VesselsCharacteristicsValue { get; set; }
        public string VesselsCharacteristicsUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new VesselsMeasurement
            {
                ComparisonOperator = ComparisonOperator,
                VesselsCharacteristics = VesselsCharacteristics,
                VesselsCharacteristicsUnit = VesselsCharacteristicsUnit,
                VesselsCharacteristicsValue = VesselsCharacteristicsValue
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new System.NotImplementedException();
        }
    }
}
