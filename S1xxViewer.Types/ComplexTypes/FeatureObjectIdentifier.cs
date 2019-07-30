using S1xxViewer.Types.Interfaces;
using System.Xml;
using System;

namespace S1xxViewer.Types.ComplexTypes
{
    public class FeatureObjectIdentifier : ComplexTypeBase, IFeatureObjectIdentifier
    {
        public string Agency { get; set; }
        public int FeatureIdentificationNumber { get; set; }
        public int FeatureIdentificationSubdivision { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new FeatureObjectIdentifier
            {
                Agency = Agency,
                FeatureIdentificationNumber = FeatureIdentificationNumber,
                FeatureIdentificationSubdivision = FeatureIdentificationSubdivision
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                var agencyNode = node.SelectSingleNode("s100:agency", mgr);
                if (agencyNode != null)
                {
                    Agency = agencyNode.FirstChild.InnerText;
                }

                var featureIdentificationNumberNode = node.SelectSingleNode("s100:featureIdentificationNumber", mgr);
                if (featureIdentificationNumberNode != null)
                {
                    int featureIdentificationNumber;
                    if (!int.TryParse(featureIdentificationNumberNode.FirstChild.InnerText, out featureIdentificationNumber))
                    {
                        featureIdentificationNumber = -1;
                    }
                    FeatureIdentificationNumber = featureIdentificationNumber;
                }

                var featureIdentificationSubdivisionNode = node.SelectSingleNode("s100:featureIdentificationSubdivision", mgr);
                if (featureIdentificationSubdivisionNode != null)
                {
                    int featureIdentificationSubdivision;
                    if (!int.TryParse(featureIdentificationSubdivisionNode.FirstChild.InnerText, out featureIdentificationSubdivision))
                    {
                        featureIdentificationSubdivision = -1;
                    }
                    FeatureIdentificationSubdivision = featureIdentificationSubdivision;
                }
            }

            return this;
        }
    }
}
