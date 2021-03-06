﻿using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Globalization;
using System.Xml;
using System.Collections.Generic;

namespace S1xxViewer.Model.Geometry
{
    public class CurveBuilder : GeometryBuilderBase, ICurveBuilder
    {
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

                var segmentNodes = node.FirstChild.SelectNodes("gml:segments", mgr);
                if (segmentNodes != null && segmentNodes.Count > 0)
                {
                    var segments = new List<List<MapPoint>>();
                    foreach (XmlNode segmentNode in segmentNodes)
                    {
                        var curveMapPoints = new List<MapPoint>();
                        var lineStringNodes = segmentNode.ChildNodes;
                        foreach (XmlNode lineStringNode in lineStringNodes)
                        {
                            if (lineStringNode.HasChildNodes &&
                                lineStringNode.ChildNodes[0].Name.ToUpper().Contains("POSLIST"))
                            {
                                string[] splittedPositionArray =
                                    lineStringNode.ChildNodes[0].InnerText
                                        .Replace("\t", " ")
                                        .Replace("\n", " ")
                                        .Replace("\r", " ")
                                        .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

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

                                    curveMapPoints.Add(new MapPoint(x, y));
                                }
                            }
                        }

                        segments.Add(curveMapPoints);
                    }

                    if (segments.Count > 0)
                    {
                        var polyline = new Polyline(segments, new SpatialReference(_spatialReferenceSystem));
                        return polyline;
                    }
                }
            }

            return null;
        }
    }
}
