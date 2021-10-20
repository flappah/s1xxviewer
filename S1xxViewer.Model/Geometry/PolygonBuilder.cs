using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class PolygonBuilder : GeometryBuilderBase, IPolygonBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override Esri.ArcGISRuntime.Geometry.Geometry FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                XmlNode srsNode = null;
                if (node.Attributes.Count > 0 && node.Attributes[0].Name == "srsName")
                {
                    srsNode = node;
                }
                else if (node.FirstChild.Attributes.Count > 0 && node.FirstChild.Attributes[0].Name == "srsName")
                {
                    srsNode = node.FirstChild;
                }

                if (srsNode != null)
                {
                    if (!int.TryParse(srsNode.Attributes[0].Value.ToString().LastPart(char.Parse(":")), out int refSystem))
                    {
                        refSystem = 0;
                    }
                    _spatialReferenceSystem = refSystem;
                }

                // parse the exterior linearring

                var segments = new List<List<MapPoint>>();
                var exteriorNode = node["gml:exterior"];
                if (exteriorNode != null && exteriorNode.HasChildNodes)
                {
                    var exteriorMapPoints = new List<MapPoint>();
                    var linearRingNodes = exteriorNode.ChildNodes;
                    foreach (XmlNode linearRingNode in linearRingNodes)
                    {
                        if (linearRingNode.HasChildNodes &&
                            linearRingNode.ChildNodes[0].Name.ToUpper().Contains("POSLIST"))
                        {
                            string[] splittedPositionArray =
                                linearRingNode.ChildNodes[0].InnerText
                                    .Replace("\t", " ")
                                    .Replace("\n", " ")
                                    .Replace("\r", " ")
                                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                            if (((double)splittedPositionArray.Length / 2.0) == Math.Abs(splittedPositionArray.Length / 2.0))
                            {
                                for (int i = 0; i < splittedPositionArray.Length; i += 2)
                                {
                                    if (!double.TryParse(splittedPositionArray[i], NumberStyles.Float, new CultureInfo("en-US"), out double x))
                                    {
                                        x = 0.0;
                                    }
                                    if (!double.TryParse(splittedPositionArray[i + 1], NumberStyles.Float, new CultureInfo("en-US"), out double y))
                                    {
                                        y = 0.0;
                                    }

                                    exteriorMapPoints.Add(new MapPoint(y, x, new SpatialReference(_spatialReferenceSystem)));
                                }
                            }
                        }

                        segments.Add(exteriorMapPoints);
                    }
                }

                var interiorNode = node["gml:interior"];
                if (interiorNode != null && interiorNode.HasChildNodes)
                {
                    var interiorMapPoints = new List<MapPoint>();
                    var linearRingNodes = interiorNode.ChildNodes;
                    foreach (XmlNode linearRingNode in linearRingNodes)
                    {
                        if (linearRingNode.HasChildNodes &&
                            linearRingNode.ChildNodes[0].Name.ToUpper().Contains("POSLIST"))
                        {
                            string[] splittedPositionArray =
                                linearRingNode.ChildNodes[0].InnerText
                                    .Replace("\t", " ")
                                    .Replace("\n", " ")
                                    .Replace("\r", " ")
                                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                            if (((double)splittedPositionArray.Length / 2.0) == Math.Abs(splittedPositionArray.Length / 2.0))
                            {
                                for (int i = 0; i < splittedPositionArray.Length; i += 2)
                                {
                                    if (!double.TryParse(splittedPositionArray[i], NumberStyles.Float, new CultureInfo("en-US"), out double x))
                                    {
                                        x = 0.0;
                                    }
                                    if (!double.TryParse(splittedPositionArray[i + 1], NumberStyles.Float, new CultureInfo("en-US"), out double y))
                                    {
                                        y = 0.0;
                                    }

                                    interiorMapPoints.Add(new MapPoint(y, x, new SpatialReference(_spatialReferenceSystem)));
                                }
                            }
                        }

                        segments.Add(interiorMapPoints);
                    }
                }

                if (segments.Count > 0)
                {
                    var polygon = new Polygon(segments, new SpatialReference(_spatialReferenceSystem));
                    return polygon;
                }
            }

            return null;
        }
    }
}