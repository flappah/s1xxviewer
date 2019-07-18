using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;
using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Features
{
    [Serializable]
    public class RestrictedAreaNavigational : GeoFeatureBase, IRestrictedAreaNavigational
    {
        public string CategoryOfRestrictedArea { get; set; }
        public InternationalString[] FeatureName { get; set; }
        public string[] Restriction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new RestrictedAreaNavigational
            {
                FeatureName = FeatureName == null
                    ? new[] { new InternationalString("") }
                    : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
                CategoryOfRestrictedArea = CategoryOfRestrictedArea ?? "",
                Restriction = Restriction == null ? new string[0] : Array.ConvertAll(Restriction, s => s),
                Geometry = Geometry,
                Id = Id ?? ""
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
                var featureNames = new List<InternationalString>();
                foreach (XmlNode featureNameNode in featureNameNodes)
                {
                    var language = featureNameNode.SelectSingleNode("language", mgr)?.InnerText ?? "";
                    var name = featureNameNode.SelectSingleNode("name", mgr)?.InnerText ?? "";
                    featureNames.Add(new InternationalString(name, language));
                }
                FeatureName = featureNames.ToArray();
            }

            var categoryOfRestrictedAreaNode = node.FirstChild.SelectSingleNode("categoryOfRestrictedArea", mgr);
            if (categoryOfRestrictedAreaNode != null)
            {
                CategoryOfRestrictedArea = categoryOfRestrictedAreaNode.FirstChild.InnerText;
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
