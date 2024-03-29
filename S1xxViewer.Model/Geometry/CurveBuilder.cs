﻿using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Globalization;
using System.Xml;
using System.Collections.Generic;
using S1xxViewer.Storage;
using S1xxViewer.Storage.Interfaces;

namespace S1xxViewer.Model.Geometry
{
    public class CurveBuilder : GeometryBuilderBase, ICurveBuilder
    {
        /// <summary>
        ///     For injection purposes
        /// </summary>
        /// <param name="optionsStorage"></param>
        public CurveBuilder(IOptionsStorage optionsStorage)
        {
            _optionsStorage = optionsStorage;
        }

        /// <summary>
        ///     Retrieves the geometry from the specified Xml Node
        /// </summary>
        /// <param name="node">node containing a basic geometry</param>
        /// <param name="mgr">namespace manager</param>
        /// <returns>ESRI Arc GIS geometry</returns>
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

                if (_spatialReferenceSystem == 0)
                {
                    string defaultCRSString = _optionsStorage.Retrieve("comboBoxCRS");
                    if (int.TryParse(defaultCRSString, out int defaultCRS))
                    {
                        _spatialReferenceSystem = defaultCRS; // if no srsNode is found assume default reference systema
                    }
                    else
                    {
                        _spatialReferenceSystem = 4326; // since most S1xx standards assume WGS84 is default, use this is the uber default CRS
                    }
                }

                string invertLatLonString = _optionsStorage.Retrieve("checkBoxInvertLatLon");
                if (!bool.TryParse(invertLatLonString, out bool invertLatLon))
                {
                    invertLatLon = false;
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
                                        .Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

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

                                    if (invertLatLon)
                                    {
                                        curveMapPoints.Add(new MapPoint(y, x, new SpatialReference(_spatialReferenceSystem)));
                                    }
                                    else
                                    {
                                        curveMapPoints.Add(new MapPoint(x, y, new SpatialReference(_spatialReferenceSystem)));
                                    }
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
