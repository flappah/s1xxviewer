﻿using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class MarineProtectedArea : GeoFeatureBase, IMarineProtectedArea, IS122Feature
    {
        // data
        public string CategoryOfMarineProtectedArea { get; set; }
        public string[] CategoryOfRestrictedArea { get; set; }
        public string Jurisdiction { get; set; }
        public string[] Restriction { get; set; }
        public string[] Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new MarineProtectedArea
            {
                FeatureName = FeatureName == null
                    ? new[] { new FeatureName() }
                    : Array.ConvertAll(FeatureName, fn => fn.DeepClone() as IFeatureName),
                FixedDateRange = FixedDateRange == null
                    ? new DateRange()
                    : FixedDateRange.DeepClone() as IDateRange,
                Id = Id,
                PeriodicDateRange = PeriodicDateRange == null
                    ? new DateRange[0]
                    : Array.ConvertAll(PeriodicDateRange, p => p.DeepClone() as IDateRange),
                SourceIndication = SourceIndication == null
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication,
                TextContent = TextContent == null
                    ? new TextContent[0]
                    : Array.ConvertAll(TextContent, t => t.DeepClone() as ITextContent),
                Geometry = Geometry,
                CategoryOfMarineProtectedArea = CategoryOfMarineProtectedArea ?? "",
                CategoryOfRestrictedArea = CategoryOfRestrictedArea == null
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfRestrictedArea, s => s),
                Jurisdiction = Jurisdiction ?? "",
                Restriction = Restriction == null
                    ? new string[0] 
                    : Array.ConvertAll(Restriction, s => s),
                Status = Status == null
                    ? new string[0]
                    : Array.ConvertAll(Status, s => s),
                Links = Links == null
                    ? new Link[0]
                    : Array.ConvertAll(Links, l => l.DeepClone() as ILink)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                if (node.FirstChild.Attributes.Count > 0)
                {
                    Id = node.FirstChild.Attributes["gml:id"].InnerText;
                }
            }

            var periodicDateRangeNodes = node.FirstChild.SelectNodes("periodicDateRange", mgr);
            if (periodicDateRangeNodes != null && periodicDateRangeNodes.Count > 0)
            {
                var dateRanges = new List<DateRange>();
                foreach (XmlNode periodicDateRangeNode in periodicDateRangeNodes)
                {
                    var newDateRange = new DateRange();
                    newDateRange.FromXml(periodicDateRangeNode, mgr);
                    dateRanges.Add(newDateRange);
                }
                PeriodicDateRange = dateRanges.ToArray();
            }

            var fixedDateRangeNode = node.FirstChild.SelectSingleNode("fixedDateRange", mgr);
            if (fixedDateRangeNode != null && fixedDateRangeNode.HasChildNodes)
            {
                FixedDateRange = new DateRange();
                FixedDateRange.FromXml(fixedDateRangeNode, mgr);
            }

            var featureNameNodes = node.FirstChild.SelectNodes("featureName", mgr);
            if (featureNameNodes != null && featureNameNodes.Count > 0)
            {
                var featureNames = new List<FeatureName>();
                foreach (XmlNode featureNameNode in featureNameNodes)
                {
                    var newFeatureName = new FeatureName();
                    newFeatureName.FromXml(featureNameNode, mgr);
                    featureNames.Add(newFeatureName);
                }
                FeatureName = featureNames.ToArray();
            }

            var sourceIndication = node.FirstChild.SelectSingleNode("sourceIndication", mgr);
            if (sourceIndication != null && sourceIndication.HasChildNodes)
            {
                SourceIndication = new SourceIndication();
                SourceIndication.FromXml(sourceIndication, mgr);
            }

            var textContentNodes = node.FirstChild.SelectNodes("textContent", mgr);
            if (textContentNodes != null && textContentNodes.Count > 0)
            {
                var textContents = new List<TextContent>();
                foreach (XmlNode textContentNode in textContentNodes)
                {
                    if (textContentNode != null && textContentNode.HasChildNodes)
                    {
                        var content = new TextContent();
                        content.FromXml(textContentNode, mgr);
                        textContents.Add(content);
                    }
                }
                TextContent = textContents.ToArray();
            }

            var categoryOfMarineProtectedAreaNode = node.FirstChild.SelectSingleNode("categoryOfMarineProtectedArea", mgr);
            if (categoryOfMarineProtectedAreaNode != null && categoryOfMarineProtectedAreaNode.HasChildNodes)
            {
                CategoryOfMarineProtectedArea = categoryOfMarineProtectedAreaNode.FirstChild.InnerText;
            }

            var categoryOfRestrictedAreaNodes = node.FirstChild.SelectNodes("categoryOfRestrictedArea", mgr);
            if (categoryOfRestrictedAreaNodes != null && categoryOfRestrictedAreaNodes.Count > 0)
            {
                var categories = new List<string>();
                foreach(XmlNode categoryOfRestrictedAreaNode in categoryOfRestrictedAreaNodes)
                {
                    if (categoryOfRestrictedAreaNode != null && categoryOfRestrictedAreaNode.HasChildNodes)
                    {
                        categories.Add(categoryOfRestrictedAreaNode.FirstChild.InnerText);
                    }
                }

                categories.Sort();
                CategoryOfRestrictedArea = categories.ToArray();
            }

            var jurisdictionNode = node.FirstChild.SelectSingleNode("jurisdiction", mgr);
            if (jurisdictionNode != null && jurisdictionNode.HasChildNodes)

            {
                Jurisdiction = jurisdictionNode.FirstChild.InnerText;
            }

            var restrictionNodes = node.FirstChild.SelectNodes("restriction", mgr);
            if (restrictionNodes != null && restrictionNodes.Count > 0)
            {
                var restrictions = new List<string>();
                foreach (XmlNode restrictionNode in restrictionNodes)
                {
                    if (restrictionNode != null && restrictionNode.HasChildNodes)
                    {
                        restrictions.Add(restrictionNode.FirstChild.InnerText);
                    }
                }

                restrictions.Sort();
                Restriction = restrictions.ToArray();
            }

            var statusNodes = node.FirstChild.SelectNodes("status", mgr);
            if (statusNodes != null && statusNodes.Count > 0)
            {
                var statuses = new List<string>();
                foreach (XmlNode statusNode in statusNodes)
                {
                    if (statusNode != null && statusNode.HasChildNodes)
                    {
                        statuses.Add(statusNode.FirstChild.InnerText);
                    }
                }

                statuses.Sort();
                Status = statuses.ToArray();
            }

            var linkNodes = node.FirstChild.SelectNodes("*[boolean(@xlink:href)]", mgr);
            if (linkNodes != null && linkNodes.Count > 0)
            {
                var links = new List<Link>();
                foreach(XmlNode linkNode in linkNodes)
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
