using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class VesselsMeasurement : ComplexTypeBase, IVesselsMeasurement
    {
        public string ComparisonOperator { get; set; }
        public string VesselsCharacteristics { get; set; }
        public string VesselsCharacteristicsValue { get; set; }
        public string VesselsCharacteristicsUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new VesselsMeasurement
            {
                ComparisonOperator = ComparisonOperator,
                VesselsCharacteristics = VesselsCharacteristics,
                VesselsCharacteristicsUnit = VesselsCharacteristicsUnit,
                VesselsCharacteristicsValue = VesselsCharacteristicsValue
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
            var comparisonOperatorNode = node.FirstChild.SelectSingleNode("comparisonOperator", mgr);
            if (comparisonOperatorNode != null && comparisonOperatorNode.HasChildNodes)
            {
                ComparisonOperator = comparisonOperatorNode.FirstChild.InnerText;
            }

            var vesselsCharacteristicsNode = node.FirstChild.SelectSingleNode("vesselsCharacteristics", mgr);
            if (vesselsCharacteristicsNode != null && vesselsCharacteristicsNode.HasChildNodes)
            {
                VesselsCharacteristics = vesselsCharacteristicsNode.FirstChild.InnerText;
            }

            var vesselsCharacteristicsValueNode = node.FirstChild.SelectSingleNode("vesselsCharacteristicsValue", mgr);
            if (vesselsCharacteristicsValueNode != null && vesselsCharacteristicsValueNode.HasChildNodes)
            {
                VesselsCharacteristicsValue = vesselsCharacteristicsValueNode.FirstChild.InnerText;
            }

            var vesselsCharacteristicsUnitNode = node.FirstChild.SelectSingleNode("vesselsCharacteristicsUnit", mgr);
            if (vesselsCharacteristicsUnitNode != null && vesselsCharacteristicsUnitNode.HasChildNodes)
            {
                VesselsCharacteristicsUnit = vesselsCharacteristicsUnitNode.FirstChild.InnerText;
            }
            
            return this;
        }
    }
}
