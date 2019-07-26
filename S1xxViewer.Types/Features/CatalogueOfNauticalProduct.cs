﻿using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class CatalogueOfNauticalProduct : GeoFeatureBase, ICatalogueOfNauticalProduct
    {
        public IGraphic[] Graphic { get; set; }
        public string IssueDate { get; set; }
        public int EditionNumber { get; set; }
        public int MarineResourceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new CatalogueOfNauticalProduct
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
                Graphic = Graphic == null
                    ? new Graphic[0]
                    : Array.ConvertAll(Graphic, g => g.DeepClone() as IGraphic),
                IssueDate = IssueDate,
                EditionNumber = EditionNumber,
                MarineResourceName = MarineResourceName,
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

            var featureNameNodes = node.FirstChild.SelectNodes("featureName", mgr);
            if (featureNameNodes != null && featureNameNodes.Count > 0)
            {
                var featureNames = new List<FeatureName>();
                foreach (XmlNode featureNameNode in featureNameNodes)
                {
                    var newFeatureName = new FeatureName();
                    newFeatureName.FromXml(featureNameNode.FirstChild, mgr);
                    featureNames.Add(newFeatureName);
                }
                FeatureName = featureNames.ToArray();
            }

            var fixedDateRangeNode = node.FirstChild.SelectSingleNode("fixedDateRange", mgr);
            if (fixedDateRangeNode != null && fixedDateRangeNode.HasChildNodes)
            {
                FixedDateRange = new DateRange();
                FixedDateRange.FromXml(fixedDateRangeNode.FirstChild, mgr);
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

            var sourceIndicationNode = node.FirstChild.SelectSingleNode("sourceIndication", mgr);
            if (sourceIndicationNode != null && sourceIndicationNode.HasChildNodes)
            {
                SourceIndication = new SourceIndication();
                SourceIndication.FromXml(sourceIndicationNode.FirstChild, mgr);
            }

            var textContentNodes = node.FirstChild.SelectNodes("textContent", mgr);
            if (textContentNodes != null && textContentNodes.Count > 0)
            {
                var textContents = new List<TextContent>();
                foreach (XmlNode textContentNode in textContentNodes)
                {
                    if (textContentNode != null && textContentNode.HasChildNodes)
                    {
                        var newTextContent = new TextContent();
                        newTextContent.FromXml(textContentNode.FirstChild, mgr);
                        textContents.Add(newTextContent);
                    }
                }

                TextContent = textContents.ToArray();
            }

            var graphicNodes = node.FirstChild.SelectNodes("graphic", mgr);
            if (graphicNodes != null && graphicNodes.Count > 0)
            {
                var graphics = new List<Graphic>();
                foreach (XmlNode graphicNode in graphicNodes)
                {
                    if (graphicNode != null && graphicNode.HasChildNodes)
                    {
                        var newGraphic = new Graphic();
                        newGraphic.FromXml(graphicNode.FirstChild, mgr);
                        graphics.Add(newGraphic);
                    }
                }
                Graphic = graphics.ToArray();
            }

            var issueDateNode = node.FirstChild.SelectSingleNode("issueDate", mgr);
            if (issueDateNode != null && issueDateNode.HasChildNodes)
            {
                //DateTime issueDate;
                //if (!DateTime.TryParse(issueDateNode.FirstChild.InnerText, out issueDate))
                //{
                //    issueDate = DateTime.MinValue;
                //}
                IssueDate = issueDateNode.FirstChild.InnerText;
            }

            var editionNumberNode = node.FirstChild.SelectSingleNode("editionNumber", mgr);
            if (editionNumberNode != null && editionNumberNode.HasChildNodes)
            {
                int editionNumber;
                if (!int.TryParse(editionNumberNode.FirstChild.InnerText, out editionNumber))
                {
                    editionNumber = -1;
                }
                EditionNumber = editionNumber;
            }

            var marineResourceNameNode = node.FirstChild.SelectSingleNode("marineResourceName", mgr);
            if (marineResourceNameNode != null && marineResourceNameNode.HasChildNodes)
            {
                //MarineResourceName = marineResourceNameNode.FirstChild.InnerText;
                int marineResourceName;
                if (!int.TryParse(marineResourceNameNode.FirstChild.InnerText, out marineResourceName))
                {
                    marineResourceName = -1;
                }
                MarineResourceName = marineResourceName;
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
