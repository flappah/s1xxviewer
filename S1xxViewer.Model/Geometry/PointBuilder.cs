using Esri.ArcGISRuntime.Geometry;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class PointBuilder : GeometryBuilderBase, IPointBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
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

                var pointNode = node.FirstChild;
                if (pointNode != null && pointNode.HasChildNodes && pointNode.FirstChild.Name.ToUpper().Contains("POS"))
                {
                    var splittedPosition = 
                        pointNode.FirstChild.InnerText.Replace("\t", " ").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    double x;
                    double y;
                    if (!double.TryParse(splittedPosition[0], NumberStyles.Float, new CultureInfo("en-US"), out x))
                    {
                        x = 0.0;
                    }
                    if (!double.TryParse(splittedPosition[1], NumberStyles.Float, new CultureInfo("en-US"), out y))
                    {
                        y = 0.0;
                    }

                    return new MapPoint(x, y, new SpatialReference(_spatialReferenceSystem));
                }
            }

            return null;
        }
    }
}
