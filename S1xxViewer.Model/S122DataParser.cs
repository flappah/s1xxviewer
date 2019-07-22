using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types;
using S1xxViewer.Types.Interfaces;
using System.Collections.Generic;
using System.Xml;
using System;

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
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");

            // retrieve boundingbox
            var boundingBoxNodes = xmlDocument.GetElementsByTagName("gml:boundedBy");
            if (boundingBoxNodes != null && boundingBoxNodes.Count > 0)
            {
                dataPackage.BoundingBox = _geometryBuilderFactory.FromXml(boundingBoxNodes[0], nsmgr);
            }
            
            // retrieve imembers
            XmlNodeList imemberNodes = xmlDocument.GetElementsByTagName("imember");
            var informationFeatures = new List<IInformationFeature>();

            foreach (XmlNode imemberNode in imemberNodes)
            {
                var feature = _featureFactory.FromXml(imemberNode, nsmgr).DeepClone();
                var informationFeature = feature as IInformationFeature;
                if (informationFeature != null)
                {
                    informationFeatures.Add(informationFeature);
                }
            }

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

            // Populate links between features
            foreach(IFeature geoFeature in geoFeatures)
            {
                ResolveLinks(geoFeature.Links, informationFeatures, metaFeatures, geoFeatures);
            }

            foreach (IFeature metaFeature in metaFeatures)
            {
                ResolveLinks(metaFeature.Links, informationFeatures, metaFeatures, geoFeatures);
            }

            foreach (IFeature infoFeature in informationFeatures)
            {
                ResolveLinks(infoFeature.Links, informationFeatures, metaFeatures, geoFeatures);
            }

            dataPackage.GeoFeatures = geoFeatures.ToArray();
            dataPackage.MetaFeatures = metaFeatures.ToArray();
            dataPackage.InformationFeatures = informationFeatures.ToArray();
            return dataPackage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="links"></param>
        /// <param name="informationFeatures"></param>
        /// <param name="metaFeatures"></param>
        /// <param name="geoFeatures"></param>
        private void ResolveLinks(ILink[] links, List<IInformationFeature> informationFeatures, List<IMetaFeature> metaFeatures, List<IGeoFeature> geoFeatures)
        {
            foreach (ILink link in links)
            {
                int foundInfoFeatureIndex =
                    informationFeatures.FindIndex(ftr =>
                        !String.IsNullOrEmpty(ftr.Id) &&
                        ftr.Id.Contains(link.Href.Replace("#", "")));

                if (foundInfoFeatureIndex != -1)
                {
                    link.Offset = $"I_{foundInfoFeatureIndex}";
                }
                else
                {
                    int foundMetaFeatureIndex =
                        metaFeatures.FindIndex(ftr =>
                            !String.IsNullOrEmpty(ftr.Id) &&
                            ftr.Id.Contains(link.Href.Replace("#", "")));

                    if (foundMetaFeatureIndex != -1)
                    {
                        link.Offset = $"M_{foundMetaFeatureIndex}";
                    }
                    else
                    {
                        int foundGeoFeatureIndex =
                            geoFeatures.FindIndex(ftr =>
                                !String.IsNullOrEmpty(ftr.Id) &&
                                ftr.Id.Contains(link.Href.Replace("#", "")));

                        if (foundGeoFeatureIndex != -1)
                        {
                            link.Offset = $"G_{foundGeoFeatureIndex}";
                        }
                    }
                }
            }
        }
    }
}
