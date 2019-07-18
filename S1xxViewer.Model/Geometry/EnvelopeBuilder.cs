using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class EnvelopeBuilder : GeometryBuilderBase, IEnvelopeBuilder
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

                if (node.ChildNodes[0].ChildNodes.Count == 2)
                {
                    var lowerLeft = node.ChildNodes[0].ChildNodes[0].InnerText;
                    var upperRight = node.ChildNodes[0].ChildNodes[1].InnerText;

                    var llPos = lowerLeft.Replace(@"\t", " ").Replace(@"\n", " ").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var urPos = upperRight.Replace(@"\t", " ").Replace(@"\n", " ").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    double llX;
                    double llY;

                    if (!double.TryParse(llPos[0], NumberStyles.Float, new CultureInfo("en-US"), out llX))
                    {
                        llX = 0.0;
                    }
                    if (!double.TryParse(llPos[1], NumberStyles.Float, new CultureInfo("en-US"), out llY))
                    {
                        llY = 0.0;
                    }

                    double urX;
                    double urY;
                    if (!double.TryParse(urPos[0], NumberStyles.Float, new CultureInfo("en-US"), out urX))
                    {
                        urX = 0.0;
                    }
                    if (!double.TryParse(urPos[1], NumberStyles.Float, new CultureInfo("en-US"), out urY))
                    {
                        urY = 0.0;
                    }

                    var createdEnvelope = 
                        new Esri.ArcGISRuntime.Geometry.Envelope(llX, llY, urX, urY, new Esri.ArcGISRuntime.Geometry.SpatialReference(_spatialReferenceSystem));
                    return createdEnvelope;
                }
            }

            return null;
        }
    }
}
