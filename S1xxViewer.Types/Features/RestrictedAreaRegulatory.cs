using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    [Serializable]
    public class RestrictedAreaRegulatory : GeoFeatureBase, IRestrictedAreaRegulatory
    {
        public string[] CategoryOfRestrictedArea { get; set; }
        public string[] Restriction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new RestrictedAreaRegulatory
            {
                CategoryOfRestrictedArea = CategoryOfRestrictedArea == null
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfRestrictedArea, s => s),
                FeatureName = FeatureName == null
                    ? new[] { new InternationalString("") }
                    : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
                FixedDateRange = FixedDateRange == null
                    ? new DateRange()
                    : FixedDateRange.DeepClone() as IDateRange,
                Id = Id ?? "",
                PeriodicDateRange = PeriodicDateRange == null
                    ? new DateRange()
                    : PeriodicDateRange.DeepClone() as IDateRange,
                Restriction = Restriction == null ? new string[0] : Array.ConvertAll(Restriction, s => s),
                SourceIndication = SourceIndication == null
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication,
                TextContent = TextContent == null
                    ? new TextContent()
                    : TextContent.DeepClone() as ITextContent,
                Geometry = Geometry,
                Links = Links == null
                    ? new[] { new Link() }
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

            var periodicDateRangeNode = node.FirstChild.SelectSingleNode("periodicDateRange", mgr);
            if (periodicDateRangeNode != null && periodicDateRangeNode.HasChildNodes)
            {
                PeriodicDateRange = new DateRange();
                PeriodicDateRange.FromXml(periodicDateRangeNode, mgr);
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

            var categoryOfRestrictedAreaNodes = node.FirstChild.SelectNodes("categoryOfRestrictedArea", mgr);
            if (categoryOfRestrictedAreaNodes != null && categoryOfRestrictedAreaNodes.Count > 0)
            {
                var categories = new List<string>();
                foreach (XmlNode categoryOfRestrictedAreaNode in categoryOfRestrictedAreaNodes)
                {
                    if (categoryOfRestrictedAreaNode != null && categoryOfRestrictedAreaNode.HasChildNodes)
                    {
                        categories.Add(categoryOfRestrictedAreaNode.FirstChild.InnerText);
                    }
                }
                CategoryOfRestrictedArea = categories.ToArray();
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

            return this;
        }
    }
}
