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
            
            // retrieve imembers
            XmlNodeList imemberNodes = xmlDocument.GetElementsByTagName("imember");
            // TODO: Read informationfeatures and link 'm up to geofeatures
            dataPackage.InformationFeatures = new IInformationFeature[0];

            // retrieve members
            var geoFeatures = new List<IGeoFeature>();
            var metaFeatures = new List<IMetaFeature>();
            XmlNodeList memberNodes = xmlDocument.GetElementsByTagName("member");
            foreach (XmlNode memberNode in memberNodes)
            {
                var feature = _featureFactory.FromXml(memberNode, nsmgr).DeepClone();

                var geoFeature = feature as IGeoFeature;
                if (geoFeature != null && memberNode.HasChildNodes)
                {
                    var geometryOfMemberNode = memberNode.FirstChild.SelectSingleNode("geometry");
                    if (geometryOfMemberNode != null && geometryOfMemberNode.HasChildNodes)
                    {
                        geoFeature.Geometry = _geometryBuilderFactory.FromXml(geometryOfMemberNode.ChildNodes[0], nsmgr);
                    }

                    geoFeatures.Add(geoFeature);
                }
                else
                {
                    var metaFeature = feature as IMetaFeature;
                    if (metaFeature != null && memberNode.HasChildNodes)
                    {
                        var geometryOfMemberNode = memberNode.FirstChild.SelectSingleNode("geometry");
                        if (geometryOfMemberNode != null && geometryOfMemberNode.HasChildNodes)
                        {
                            metaFeature.Geometry = _geometryBuilderFactory.FromXml(geometryOfMemberNode.ChildNodes[0], nsmgr);
                        }

                        metaFeatures.Add(metaFeature);
                    }
                }
            }
            //TODO: Restore links to informationfeatures

            dataPackage.GeoFeatures = geoFeatures.ToArray();
            dataPackage.MetaFeatures = metaFeatures.ToArray();
            return dataPackage;
        }
    }
}
