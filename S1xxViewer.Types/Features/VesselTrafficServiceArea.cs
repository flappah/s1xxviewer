using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class VesselTrafficServiceArea : GeoFeatureBase, IVesselTrafficServiceArea
    {
        // data
        public string CategoryOfVesselTrafficService { get; set; }
        public InternationalString FeatureName { get; set; }
        public IPeriodicDateRange PeriodicDateRange { get; set; }

        // linkages
        public ILink[] Links { get; set; }

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
                    ? new InternationalString("")
                    : FeatureName,
                PeriodicDateRange = PeriodicDateRange == null
                    ? new PeriodicDateRange()
                    : PeriodicDateRange.DeepClone() as IPeriodicDateRange,
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

            var categoryOfVesselTrafficService = node.FirstChild.SelectSingleNode("categoryOfVesselTrafficService", mgr);
            if (categoryOfVesselTrafficService != null)
            {
                CategoryOfVesselTrafficService = categoryOfVesselTrafficService.FirstChild.InnerText;
            }

            var featureNameNode = node.FirstChild.SelectSingleNode("featureName", mgr);
            if (featureNameNode != null && featureNameNode.HasChildNodes)
            {
                var language = featureNameNode.SelectSingleNode("language", mgr)?.InnerText ?? "";
                var name = featureNameNode.SelectSingleNode("name", mgr)?.InnerText ?? "";
                FeatureName = new InternationalString(name, language);
            }

            var periodicDateRangeNode = node.FirstChild.SelectSingleNode("periodicDateRange", mgr);
            if (periodicDateRangeNode != null && periodicDateRangeNode.HasChildNodes)
            {
                PeriodicDateRange = new PeriodicDateRange();
                PeriodicDateRange.FromXml(periodicDateRangeNode, mgr);
            }

            //TODO: resolve links

            return this;
        }
    }
}
