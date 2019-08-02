using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Links;

namespace S1xxViewer.Types.Features
{
    public class DataCoverage : MetaFeatureBase, IDataCoverage, IS122Feature, IS123Feature
    {
        public int MaximumDisplayScale { get; set; }
        public int MinimumDisplayScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new DataCoverage()
            {
                FeatureObjectIdentifier = FeatureObjectIdentifier == null 
                    ? new FeatureObjectIdentifier()
                    : FeatureObjectIdentifier,
                MaximumDisplayScale = MaximumDisplayScale,
                MinimumDisplayScale = MinimumDisplayScale,
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

                var foidNode = node.FirstChild.SelectSingleNode("s100:featureObjectIdentifier", mgr);
                if (foidNode != null && foidNode.HasChildNodes)
                {
                    FeatureObjectIdentifier = new FeatureObjectIdentifier();
                    FeatureObjectIdentifier.FromXml(foidNode, mgr);
                }

                var maximumDisplayScaleNode = node.FirstChild.SelectSingleNode("maximumDisplayScale", mgr);
                if (maximumDisplayScaleNode != null)
                {
                    int maximumDisplayScale;
                    if (!int.TryParse(maximumDisplayScaleNode.InnerText, out maximumDisplayScale))
                    {
                        maximumDisplayScale = -1;
                    }
                    MaximumDisplayScale = maximumDisplayScale;
                }

                var minimumDisplayScaleNode = node.FirstChild.SelectSingleNode("minimumDisplayScale",  mgr);
                if (minimumDisplayScaleNode != null)
                {
                    int minimumDisplayScale;
                    if (!int.TryParse(minimumDisplayScaleNode.InnerText, out minimumDisplayScale))
                    {
                        minimumDisplayScale = -1;
                    }
                    MinimumDisplayScale = minimumDisplayScale;
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
            }

            return this;
        }
    }
}
