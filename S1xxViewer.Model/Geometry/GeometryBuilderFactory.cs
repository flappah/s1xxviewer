using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using System;
using System.Linq;
using System.Xml;

namespace S1xxViewer.Model.Geometry
{
    public class GeometryBuilderFactory : IGeometryBuilderFactory
    {
        public IGeometryBuilder[] Builders { get; set; }

        public Esri.ArcGISRuntime.Geometry.Geometry FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var geometryTypeString = (node.HasChildNodes ? node.ChildNodes[0].Name : "").LastPart(char.Parse(":"));

            var locatedBuilder =
                Builders.ToList().Find(tp => tp.GetType().ToString().Contains($"{geometryTypeString}Builder"));

            if (locatedBuilder != null)
            {
                return locatedBuilder.FromXml(node, mgr);
            }

            return null;
        }
    }
}
