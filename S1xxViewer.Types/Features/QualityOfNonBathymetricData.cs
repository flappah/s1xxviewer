using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class QualityOfNonBathymetricData : MetaFeatureBase, IQualityOfNonBathymetricData
    {
        public string CategoryOfTemporalVariation { get; set; }
        public string DataAssessment { get; set; }
        public ISourceIndication SourceIndication { get; set; }
        public IFeatureObjectIdentifier FeatureObjectIdentifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new QualityOfNonBathymetricData
            {
                CategoryOfTemporalVariation = CategoryOfTemporalVariation,
                DataAssessment = DataAssessment,
                SourceIndication = SourceIndication == null
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication,
                FeatureObjectIdentifier = FeatureObjectIdentifier == null 
                    ? new FeatureObjectIdentifier()
                    : FeatureObjectIdentifier.DeepClone(),
                Geometry = Geometry,
                Id = Id
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                if (node.FirstChild.Attributes.Count > 0)
                {
                    Id = node.FirstChild.Attributes["gml:id"].InnerText;
                }

                var foidNode = node.FirstChild.SelectSingleNode("s100:featureObjectIdentifier", mgr);
                if (foidNode != null && foidNode.HasChildNodes)
                {
                    FeatureObjectIdentifier = new FeatureObjectIdentifier();
                    FeatureObjectIdentifier.FromXml(foidNode, mgr);
                }

                var categoryOfTemporalVariation = node.FirstChild.SelectSingleNode("categoryOfTemporalVariation", mgr);
                if (categoryOfTemporalVariation != null)
                {
                    CategoryOfTemporalVariation = categoryOfTemporalVariation.InnerText;
                }

                var dataAssessment = node.FirstChild.SelectSingleNode("dataAssessment", mgr);
                if (dataAssessment != null)
                {
                    DataAssessment = dataAssessment.InnerText;
                }

                var sourceIndicationNode = node.FirstChild.SelectSingleNode("sourceIndication", mgr);
                if (sourceIndicationNode != null && sourceIndicationNode.HasChildNodes)
                {
                    SourceIndication = new SourceIndication();
                    SourceIndication.FromXml(sourceIndicationNode, mgr);
                }
            }

            return this;
        }
    }
}
