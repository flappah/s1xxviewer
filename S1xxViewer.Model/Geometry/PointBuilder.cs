using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class PointBuilder : GeometryBuilderBase, IPointBuilder
    {
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
                    _spatialReferenceSystem = 4326; // if no srsNode is found assume default reference system, WGS 84
                }

                var pointNode = node.FirstChild;
                if (pointNode != null && pointNode.HasChildNodes && pointNode.FirstChild.Name.ToUpper().Contains("POS"))
                {
                    var splittedPosition = 
                        pointNode.FirstChild.InnerText.Replace("\t", " ").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    if (!double.TryParse(splittedPosition[0], NumberStyles.Float, new CultureInfo("en-US"), out double x))
                    {
                        x = 0.0;
                    }
                    if (!double.TryParse(splittedPosition[1], NumberStyles.Float, new CultureInfo("en-US"), out double y))
                    {
                        y = 0.0;
                    }

                    return new MapPoint(y, x, new SpatialReference(_spatialReferenceSystem));
                }
            }

            return null;
        }
    }
}
