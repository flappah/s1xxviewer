using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class MarineProtectedArea : GeoFeatureBase, IMarineProtectedArea
    {
        // data
        public string CategoryOfMarineProtectedArea { get; set; }
        public string[] CategoryOfRestrictedArea { get; set; }
        public InternationalString FeatureName { get; set; }
        public string Jurisdiction { get; set; }
        public IPeriodicDateRange PeriodicDateRange { get; set; }
        public string[] Restriction { get; set; }
        public ISourceIndication SourceIndication { get; set; }
        public string Status { get; set; }

        // linkages
        public ILink[] Links { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new MarineProtectedArea
            {
                CategoryOfMarineProtectedArea = CategoryOfMarineProtectedArea ?? "",
                CategoryOfRestrictedArea = CategoryOfRestrictedArea == null
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfRestrictedArea, s => s),
                FeatureName = FeatureName == null
                    ? new InternationalString("")
                    : FeatureName,
                Jurisdiction = Jurisdiction ?? "",
                PeriodicDateRange = PeriodicDateRange == null
                    ? new PeriodicDateRange()
                    : PeriodicDateRange.DeepClone() as IPeriodicDateRange,
                SourceIndication = SourceIndication == null
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication,
                Status = Status ?? "",
                Geometry = Geometry,
                Id = Id,
                Links = Links == null
                    ? new[] { new Link() }
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

            var periodicDateRangeNode = node.FirstChild.SelectSingleNode("periodicDateRange", mgr);
            if (periodicDateRangeNode != null && periodicDateRangeNode.HasChildNodes)
            {
                PeriodicDateRange = new PeriodicDateRange();
                PeriodicDateRange.FromXml(periodicDateRangeNode, mgr);
            }

            var featureNameNode = node.FirstChild.SelectSingleNode("featureName", mgr);
            if (featureNameNode != null && featureNameNode.HasChildNodes)
            {
                var language = featureNameNode.SelectSingleNode("language", mgr)?.InnerText ?? "";
                var name = featureNameNode.SelectSingleNode("name", mgr)?.InnerText ?? "";
                FeatureName = new InternationalString(name, language);
            }

            var sourceIndication = node.FirstChild.SelectSingleNode("sourceIndication", mgr);
            if (sourceIndication != null && sourceIndication.HasChildNodes)
            {
                SourceIndication = new SourceIndication();
                SourceIndication.FromXml(sourceIndication, mgr);
            }

            var categoryOfMarineProtectedAreaNode = node.FirstChild.SelectSingleNode("categoryOfMarineProtectedArea", mgr);
            if (categoryOfMarineProtectedAreaNode != null)
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
                Restriction = restrictions.ToArray();
            }

            var statusNode = node.FirstChild.SelectSingleNode("status", mgr);
            if (statusNode != null && statusNode.HasChildNodes)

            {
                Status = statusNode.FirstChild.InnerText;
            }

            //TODO: resolve links

            return this;
        }
    }
}
