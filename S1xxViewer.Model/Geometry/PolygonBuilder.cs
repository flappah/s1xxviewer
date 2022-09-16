using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using S1xxViewer.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class PolygonBuilder : GeometryBuilderBase, IPolygonBuilder
    {
        /// <summary>
        ///     For injection purposes
        /// </summary>
        /// <param name="optionsStorage"></param>
        public PolygonBuilder(IOptionsStorage optionsStorage)
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
                    string defaultCRS = _optionsStorage.Retrieve("comboBoxCRS");
                    if (int.TryParse(defaultCRS, out int defaultCRSValue))
                    {
                        _spatialReferenceSystem = defaultCRSValue; // if no srsNode is found assume default reference systema
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
                                    .Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

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

                                    if (invertLatLon)
                                    {
                                        exteriorMapPoints.Add(new MapPoint(y, x, new SpatialReference(_spatialReferenceSystem)));
                                    }
                                    else
                                    {
                                        exteriorMapPoints.Add(new MapPoint(x, y, new SpatialReference(_spatialReferenceSystem)));
                                    }
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
                                    .Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

                            if ((splittedPositionArray.Length / 2.0) == Math.Abs(splittedPositionArray.Length / 2.0))
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

                                    if (invertLatLon)
                                    {
                                        interiorMapPoints.Add(new MapPoint(y, x, new SpatialReference(_spatialReferenceSystem)));
                                    }
                                    else
                                    {
                                        interiorMapPoints.Add(new MapPoint(x, y, new SpatialReference(_spatialReferenceSystem)));
                                    }
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