using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class SurfaceBuilder : GeometryBuilderBase, ISurfaceBuilder
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
                if (node.FirstChild.Attributes.Count > 0 &&
                    node.FirstChild.Attributes[0].Name == "srsName")
                {
                    int refSystem;
                    if (!int.TryParse(node.FirstChild.Attributes[0].Value.ToString().LastPart(char.Parse(":")), out refSystem))
                    {
                        refSystem = 0;
                    }
                    _spatialReferenceSystem = refSystem;
                }
            }

            var segments = new List<List<MapPoint>>();
            if (node.HasChildNodes && node.FirstChild.HasChildNodes)
            {
                var surfaceNode = node.FirstChild;
                var patchesNodes = surfaceNode.SelectNodes(@"gml:patches", mgr);
                if (patchesNodes != null && patchesNodes.Count > 0)
                {
                    foreach (XmlNode patchesNode in patchesNodes)
                    {
                        if (patchesNode.HasChildNodes)
                        {
                            var polygonPatchNode = patchesNode.FirstChild;
                            var exteriorNode = polygonPatchNode.SelectSingleNode("gml:exterior", mgr);
                            if (exteriorNode != null && exteriorNode.HasChildNodes)
                            {
                                var exteriorMapPoints = new List<MapPoint>();
                                var linearRingNodes = exteriorNode.ChildNodes;
                                foreach (XmlNode linearRingNode in linearRingNodes)
                                {
                                    var posListNode = linearRingNode.SelectSingleNode("gml:posList", mgr);
                                    if (posListNode != null)
                                    {
                                        string[] splittedPositionArray =
                                            posListNode.InnerText
                                                .Replace("\t", " ")
                                                .Replace("\n", " ")
                                                .Replace("\r", " ")
                                                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                                        if (((double)splittedPositionArray.Length / 2.0) == Math.Abs(splittedPositionArray.Length / 2.0))
                                        {
                                            for (int i = 0; i < splittedPositionArray.Length; i += 2)
                                            {
                                                double x;
                                                double y;
                                                if (!double.TryParse(splittedPositionArray[i], NumberStyles.Float, new CultureInfo("en-US"), out x))
                                                {
                                                    x = 0.0;
                                                }
                                                if (!double.TryParse(splittedPositionArray[i + 1], NumberStyles.Float, new CultureInfo("en-US"), out y))
                                                {
                                                    y = 0.0;
                                                }

                                                exteriorMapPoints.Add(new MapPoint(x, y));
                                            }
                                        }
                                    }

                                    segments.Add(exteriorMapPoints);
                                }
                            }

                            var interiorNode = polygonPatchNode.SelectSingleNode("gml:interior", mgr);
                            if (interiorNode != null && interiorNode.HasChildNodes)
                            {
                                var interiorMapPoints = new List<MapPoint>();
                                var linearRingNodes = interiorNode.ChildNodes;
                                foreach (XmlNode linearRingNode in linearRingNodes)
                                {
                                    var posListNode = linearRingNode.SelectSingleNode("gml:posList", mgr);
                                    if (posListNode != null)
                                    {
                                        string[] splittedPositionArray =
                                            posListNode.InnerText
                                                .Replace("\t", " ")
                                                .Replace("\n", " ")
                                                .Replace("\r", " ")
                                                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                                        if (((double)splittedPositionArray.Length / 2.0) == Math.Abs(splittedPositionArray.Length / 2.0))
                                        {
                                            for (int i = 0; i < splittedPositionArray.Length; i += 2)
                                            {
                                                double x;
                                                double y;
                                                if (!double.TryParse(splittedPositionArray[i], NumberStyles.Float, new CultureInfo("en-US"), out x))
                                                {
                                                    x = 0.0;
                                                }
                                                if (!double.TryParse(splittedPositionArray[i + 1], NumberStyles.Float, new CultureInfo("en-US"), out y))
                                                {
                                                    y = 0.0;
                                                }

                                                interiorMapPoints.Add(new MapPoint(x, y));
                                            }
                                        }
                                    }

                                    segments.Add(interiorMapPoints);
                                }
                            }
                        }
                    }

                    if (segments.Count > 0)
                    {
                        var polygon = new Polygon(segments, new SpatialReference(_spatialReferenceSystem));
                        return polygon;
                    }
                }
            }

            return null;
        }
    }
}
