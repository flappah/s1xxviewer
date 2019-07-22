using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class VesselTrafficServiceArea : GeoFeatureBase, IVesselTrafficServiceArea
    {
        // data
        public string CategoryOfVesselTrafficService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new VesselTrafficServiceArea
            {
                CategoryOfVesselTrafficService = CategoryOfVesselTrafficService,
                FeatureName = FeatureName == null
                    ? new[] { new InternationalString("") }
                    : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
                FixedDateRange = FixedDateRange == null
                    ? new DateRange()
                    : FixedDateRange.DeepClone() as IDateRange,
                PeriodicDateRange = PeriodicDateRange == null
                    ? new DateRange()
                    : PeriodicDateRange.DeepClone() as IDateRange,
                SourceIndication = SourceIndication == null
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication,
                TextContent = TextContent == null
                    ? new TextContent()
                    : TextContent.DeepClone() as ITextContent,
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

            var categoryOfVesselTrafficService = node.FirstChild.SelectSingleNode("categoryOfVesselTrafficService", mgr);
            if (categoryOfVesselTrafficService != null)
            {
                CategoryOfVesselTrafficService = categoryOfVesselTrafficService.FirstChild.InnerText;
            }

            //TODO: resolve links

            return this;
        }
    }
}
