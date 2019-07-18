using S1xxViewer.Types.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Features
{
    public class DataCoverage : GeoFeatureBase, IDataCoverage
    {
        public IFeatureObjectIdentifier FeatureObjectIdentifier { get; set; }
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
                Id = Id
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
            }

            return this;
        }
    }
}
