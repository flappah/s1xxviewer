using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace S1xxViewer.Types.Features
{
    public class QualityOfNonBathymetricData : MetaFeatureBase, IQualityOfNonBathymetricData, IS122Feature
    {
        public string CategoryOfTemporalVariation { get; set; }
        public string DataAssessment { get; set; }
        public ISourceIndication SourceIndication { get; set; }
        public IFeatureObjectIdentifier FeatureObjectIdentifier { get; set; }
        public double[] HorizontalDistanceUncertainty { get; set; }
        public IHorizontalPositionalUncertainty HorizontalPositionalUncertainty { get; set; }
        public double DirectionUncertainty { get; set; }
        public ISurveyDateRange SurveyDateRange { get; set; }
        public IInformation Information { get; set; }

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
                HorizontalDistanceUncertainty = HorizontalDistanceUncertainty == null
                    ? new double[0]
                    : Array.ConvertAll(HorizontalDistanceUncertainty, hdu => hdu),
                HorizontalPositionalUncertainty = HorizontalPositionalUncertainty == null   
                    ? new HorizontalPositionalUncertainty()
                    : HorizontalPositionalUncertainty.DeepClone() as IHorizontalPositionalUncertainty,
                DirectionUncertainty = DirectionUncertainty,
                SurveyDateRange = SurveyDateRange == null   
                    ? new SurveyDateRange()
                    : SurveyDateRange.DeepClone() as ISurveyDateRange,
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

                var horizontalDistanceUncertaintyNodes = node.FirstChild.SelectNodes("horizontalDistanceUncertainty", mgr);
                if (horizontalDistanceUncertaintyNodes != null && horizontalDistanceUncertaintyNodes.Count > 0)
                {
                    var distanceUncertainties = new List<double>();
                    foreach(XmlNode horizontalDistanceUncertaintyNode in horizontalDistanceUncertaintyNodes )
                    {
                        if (horizontalDistanceUncertaintyNode != null && horizontalDistanceUncertaintyNode.HasChildNodes)
                        {
                            double uncertainty;
                            if (!double.TryParse(horizontalDistanceUncertaintyNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out uncertainty))
                            {
                                uncertainty = 0.0;
                            }
                            distanceUncertainties.Add(uncertainty);
                        }
                    }
                    HorizontalDistanceUncertainty = distanceUncertainties.ToArray();
                }

                var horizontalPositionalUncertaintyNode = node.FirstChild.SelectSingleNode("horizontalPositionalUncertainty", mgr);
                if (horizontalPositionalUncertaintyNode != null && horizontalPositionalUncertaintyNode.HasChildNodes)
                {
                    HorizontalPositionalUncertainty = new HorizontalPositionalUncertainty();
                    HorizontalPositionalUncertainty.FromXml(horizontalPositionalUncertaintyNode.FirstChild, mgr);
                }

                var directionUncertaintyNode = node.FirstChild.SelectSingleNode("directionUncertaintyNode", mgr);
                if (directionUncertaintyNode != null && directionUncertaintyNode.HasChildNodes)
                {
                    double uncertainty;
                    if (!double.TryParse(directionUncertaintyNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out uncertainty))
                    {
                        uncertainty = 0.0;
                    }
                    DirectionUncertainty = uncertainty;
                }

                var surveyDateRangeNode = node.FirstChild.SelectSingleNode("surveyDateRange", mgr);
                if (surveyDateRangeNode != null && surveyDateRangeNode.HasChildNodes)
                {
                    SurveyDateRange = new SurveyDateRange();
                    SurveyDateRange.FromXml(surveyDateRangeNode.FirstChild, mgr);
                }

                var informationNode = node.FirstChild.SelectSingleNode("information", mgr);
                if (informationNode != null && informationNode.HasChildNodes)
                {
                    Information = new Information();
                    Information.FromXml(informationNode.FirstChild, mgr);
                }
            }

            return this;
        }
    }
}
