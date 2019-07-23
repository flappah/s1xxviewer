using S1xxViewer.Types.Interfaces;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Orientation : IOrientation
    {
        public double OrientationUncertainty { get; set; }
        public double OrientationValue { get; set; }

        public IComplexType DeepClone()
        {
            return new Orientation
            {
                OrientationUncertainty = OrientationUncertainty,
                OrientationValue = OrientationValue
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var orientationUncertaintyNode = node.FirstChild.SelectSingleNode("orientationUncertainty", mgr);
            if (orientationUncertaintyNode != null && orientationUncertaintyNode.HasChildNodes)
            {
                double uncertainty;
                if (!double.TryParse(orientationUncertaintyNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out uncertainty))
                {
                    uncertainty = 0.0;
                }
                OrientationUncertainty = uncertainty;
            }

            var orientationValueNode = node.FirstChild.SelectSingleNode("orientationValue", mgr);
            if (orientationValueNode != null && orientationValueNode.HasChildNodes)
            {
                double value;
                if (!double.TryParse(orientationValueNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out value))
                {
                    value = 0.0;
                }
                OrientationValue = value;
            }

            return this;
        }
    }
}
