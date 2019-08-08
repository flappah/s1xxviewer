﻿using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class QualityOfNonBathymetricData : MetaFeatureBase, IQualityOfNonBathymetricData, IS122Feature, IS123Feature
    {
        public string CategoryOfTemporalVariation { get; set; }
        public string DataAssessment { get; set; }
        public ISourceIndication SourceIndication { get; set; }
        public string[] HorizontalDistanceUncertainty { get; set; }
        public IHorizontalPositionalUncertainty HorizontalPositionalUncertainty { get; set; }
        public string DirectionUncertainty { get; set; }
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
                    : FeatureObjectIdentifier.DeepClone() as IFeatureObjectIdentifier,
                HorizontalDistanceUncertainty = HorizontalDistanceUncertainty == null
                    ? new string[0]
                    : Array.ConvertAll(HorizontalDistanceUncertainty, hdu => hdu),
                HorizontalPositionalUncertainty = HorizontalPositionalUncertainty == null   
                    ? new HorizontalPositionalUncertainty()
                    : HorizontalPositionalUncertainty.DeepClone() as IHorizontalPositionalUncertainty,
                DirectionUncertainty = DirectionUncertainty,
                SurveyDateRange = SurveyDateRange == null   
                    ? new SurveyDateRange()
                    : SurveyDateRange.DeepClone() as ISurveyDateRange,
                Geometry = Geometry,
                Id = Id,
                Links = Links == null
                    ? new Link[0]
                    : Array.ConvertAll(Links, l => l.DeepClone() as ILink)
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

                var featureObjectIdentifierNode = node.FirstChild.SelectSingleNode("s100:featureObjectIdentifier", mgr);
                if (featureObjectIdentifierNode != null && featureObjectIdentifierNode.HasChildNodes)
                {
                    FeatureObjectIdentifier = new FeatureObjectIdentifier();
                    FeatureObjectIdentifier.FromXml(featureObjectIdentifierNode, mgr);
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
                    var distanceUncertainties = new List<string>();
                    foreach(XmlNode horizontalDistanceUncertaintyNode in horizontalDistanceUncertaintyNodes )
                    {
                        if (horizontalDistanceUncertaintyNode != null && horizontalDistanceUncertaintyNode.HasChildNodes)
                        {
                            distanceUncertainties.Add(horizontalDistanceUncertaintyNode.FirstChild.InnerText);
                        }
                    }
                    HorizontalDistanceUncertainty = distanceUncertainties.ToArray();
                }

                var horizontalPositionalUncertaintyNode = node.FirstChild.SelectSingleNode("horizontalPositionalUncertainty", mgr);
                if (horizontalPositionalUncertaintyNode != null && horizontalPositionalUncertaintyNode.HasChildNodes)
                {
                    HorizontalPositionalUncertainty = new HorizontalPositionalUncertainty();
                    HorizontalPositionalUncertainty.FromXml(horizontalPositionalUncertaintyNode, mgr);
                }

                var directionUncertaintyNode = node.FirstChild.SelectSingleNode("directionUncertainty", mgr);
                if (directionUncertaintyNode != null && directionUncertaintyNode.HasChildNodes)
                {
                    DirectionUncertainty = directionUncertaintyNode.FirstChild.InnerText;
                }

                var surveyDateRangeNode = node.FirstChild.SelectSingleNode("surveyDateRange", mgr);
                if (surveyDateRangeNode != null && surveyDateRangeNode.HasChildNodes)
                {
                    SurveyDateRange = new SurveyDateRange();
                    SurveyDateRange.FromXml(surveyDateRangeNode, mgr);
                }

                var informationNode = node.FirstChild.SelectSingleNode("information", mgr);
                if (informationNode != null && informationNode.HasChildNodes)
                {
                    Information = new Information();
                    Information.FromXml(informationNode, mgr);
                }
            }

            var linkNodes = node.FirstChild.SelectNodes("*[boolean(@xlink:href)]", mgr);
            if (linkNodes != null && linkNodes.Count > 0)
            {
                var links = new List<Link>();
                foreach (XmlNode linkNode in linkNodes)
                {
                    var newLink = new Link();
                    newLink.FromXml(linkNode, mgr);
                    links.Add(newLink);
                }
                Links = links.ToArray();
            }

            return this;
        }
    }
}
