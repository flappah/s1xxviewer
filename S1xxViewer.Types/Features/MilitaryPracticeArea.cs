﻿using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class MilitaryPracticeArea : GeoFeatureBase, IMilitaryPracticeArea, IS127Feature
    {
        public string[] CategoryOfMilitaryPracticeArea { get; set; }
        public string Nationality { get; set; }
        public string[] Restriction { get; set; }
        public string[] Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new MilitaryPracticeArea
            {
                FeatureName = FeatureName == null
                    ? new[] { new InternationalString("") }
                    : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
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
                CategoryOfMilitaryPracticeArea = CategoryOfMilitaryPracticeArea == null
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfMilitaryPracticeArea, s => s),
                Nationality = Nationality,
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
            }

            var periodicDateRangeNodes = node.FirstChild.SelectNodes("periodicDateRange", mgr);
            if (periodicDateRangeNodes != null && periodicDateRangeNodes.Count > 0)
            {
                var dateRanges = new List<DateRange>();
                foreach (XmlNode periodicDateRangeNode in periodicDateRangeNodes)
                {
                    var newDateRange = new DateRange();
                    newDateRange.FromXml(periodicDateRangeNode.FirstChild, mgr);
                    dateRanges.Add(newDateRange);
                }
                PeriodicDateRange = dateRanges.ToArray();
            }

            var featureNameNodes = node.FirstChild.SelectNodes("featureName", mgr);
            if (featureNameNodes != null && featureNameNodes.Count > 0)
            {
                var featureNames = new List<InternationalString>();
                foreach (XmlNode featureNameNode in featureNameNodes)
                {
                    var language = featureNameNode.SelectSingleNode("language", mgr)?.InnerText ?? "";
                    var name = featureNameNode.SelectSingleNode("name", mgr)?.InnerText ?? "";
                    featureNames.Add(new InternationalString(name, language));
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
                var texts = new List<TextContent>();
                foreach (XmlNode textContentNode in textContentNodes)
                {
                    var newTextContent = new TextContent();
                    newTextContent.FromXml(textContentNode.FirstChild, mgr);
                    texts.Add(newTextContent);
                }
                TextContent = texts.ToArray();
            }

            var categoryOfMilitaryPracticeAreaNodes = node.FirstChild.SelectNodes("categoryOfMilitaryPracticeArea", mgr);
            if (categoryOfMilitaryPracticeAreaNodes != null && categoryOfMilitaryPracticeAreaNodes.Count > 0)
            {
                var categories = new List<string>();
                foreach (XmlNode categoryOfMilitaryPracticeAreaNode in categoryOfMilitaryPracticeAreaNodes)
                {
                    if (categoryOfMilitaryPracticeAreaNode != null && categoryOfMilitaryPracticeAreaNode.HasChildNodes)
                    {
                        categories.Add(categoryOfMilitaryPracticeAreaNode.FirstChild.InnerText);
                    }
                }
                CategoryOfMilitaryPracticeArea = categories.ToArray();
            }

            var nationalityNode = node.FirstChild.SelectSingleNode("nationalityNode", mgr);
            if (nationalityNode != null && nationalityNode.HasChildNodes)
            {
                Nationality = nationalityNode.FirstChild.InnerText;
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
                        var status = statusNode.FirstChild.InnerText;
                        statuses.Add(status);
                    }
                }
                Status = statuses.ToArray();
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
