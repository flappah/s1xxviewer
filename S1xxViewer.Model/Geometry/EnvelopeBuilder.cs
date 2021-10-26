using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class EnvelopeBuilder : GeometryBuilderBase, IEnvelopeBuilder
    {
        /// <summary>
        ///     Retrieves the geometry from the specified Xml Node
        /// </summary>
        /// <param name="node">node containing a basic geometry</param>
        /// <param name="mgr">namespace manager</param>
        /// <returns>ESRI Arc GIS geometry</returns>
        public override Esri.ArcGISRuntime.Geometry.Geometry FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            _spatialReferenceSystem = 0;
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
                else
                {
                    _spatialReferenceSystem = 4326; // if no srsNode is found assume default reference system, WGS 84
                }

                if (node.ChildNodes[0].ChildNodes.Count == 2)
                {
                    var lowerLeft = node.ChildNodes[0].ChildNodes[0].InnerText;
                    var upperRight = node.ChildNodes[0].ChildNodes[1].InnerText;

                    var llPos = lowerLeft.Replace(@"\t", " ").Replace(@"\n", " ").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var urPos = upperRight.Replace(@"\t", " ").Replace(@"\n", " ").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    if (!double.TryParse(llPos[0], NumberStyles.Float, new CultureInfo("en-US"), out double llX))
                    {
                        llX = 0.0;
                    }
                    if (!double.TryParse(llPos[1], NumberStyles.Float, new CultureInfo("en-US"), out double llY))
                    {
                        llY = 0.0;
                    }

                    if (!double.TryParse(urPos[0], NumberStyles.Float, new CultureInfo("en-US"), out double urX))
                    {
                        urX = 0.0;
                    }
                    if (!double.TryParse(urPos[1], NumberStyles.Float, new CultureInfo("en-US"), out double urY))
                    {
                        urY = 0.0;
                    }

                    var createdEnvelope = 
                        new Esri.ArcGISRuntime.Geometry.Envelope(llY, llX, urY, urX, new Esri.ArcGISRuntime.Geometry.SpatialReference(_spatialReferenceSystem));
                    return createdEnvelope;
                }
            }

            return null;
        }
    }
}
