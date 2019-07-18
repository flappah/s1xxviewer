using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types;
using S1xxViewer.Types.Interfaces;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Model
{
    public class S122DataParser : DataParserBase, IS122DataParser
    {
        private IGeometryBuilderFactory _geometryBuilderFactory;
        private IFeatureFactory _featureFactory;

        /// <summary>
        /// For autofac initialization
        /// </summary>
        public S122DataParser(IGeometryBuilderFactory geometryBuilderFactory, IFeatureFactory featureFactory)
        {
            _geometryBuilderFactory = geometryBuilderFactory;
            _featureFactory = featureFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public override IS1xxDataPackage Parse(XmlDocument xmlDocument)
        {
            var dataPackage = new S1xxDataPackage();
            dataPackage.Type = S1xxTypes.S122;
            dataPackage.RawData = xmlDocument;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("gml", "http://www.opengis.net/gml/3.2");
            nsmgr.AddNamespace("S122", "http://www.iho.int/S122/gml/1.0");
            nsmgr.AddNamespace("s100", "http://www.iho.int/s100gml/1.0");

            // retrieve boundingbox
            var boundingBoxNodes = xmlDocument.GetElementsByTagName("gml:boundedBy");
            if (boundingBoxNodes != null && boundingBoxNodes.Count > 0)
            {
                dataPackage.BoundingBox = _geometryBuilderFactory.FromXml(boundingBoxNodes[0], nsmgr);
            }

            dataPackage.MetaFeatures = new IMetaFeature[0];

            // retrieve imembers
            XmlNodeList imemberNodes = xmlDocument.GetElementsByTagName("imember");
            dataPackage.InformationFeatures = new IInformationFeature[0];

            // retrieve members
            var features = new List<IGeoFeature>();
            XmlNodeList memberNodes = xmlDocument.GetElementsByTagName("member");
            foreach (XmlNode memberNode in memberNodes)
            {
                var feature = _featureFactory.FromXml(memberNode, nsmgr).DeepClone() as IGeoFeature;
                if (feature != null && memberNode.HasChildNodes)
                {
                    var geometryOfMemberNode = memberNode.FirstChild.SelectSingleNode("geometry");
                    if (geometryOfMemberNode != null && geometryOfMemberNode.HasChildNodes)
                    {
                        feature.Geometry = _geometryBuilderFactory.FromXml(geometryOfMemberNode.ChildNodes[0], nsmgr);
                    }

                    features.Add(feature);
                }
            }

            dataPackage.GeoFeatures = features.ToArray();
            return dataPackage;
        }
    }
}
