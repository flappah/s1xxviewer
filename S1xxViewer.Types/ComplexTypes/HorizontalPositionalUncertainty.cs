using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.ComplexTypes;
using System.Xml;
using System.Globalization;

namespace S1xxViewer.Types.ComplexTypes
{
    public class HorizontalPositionalUncertainty : ComplexTypeBase, IHorizontalPositionalUncertainty
    {
        public double UncertaintyFixed { get; set; }
        public double UncertaintyVariable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new HorizontalPositionalUncertainty
            {
                UncertaintyFixed = UncertaintyFixed,
                UncertaintyVariable = UncertaintyVariable
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var uncertaintyFixedNode = node.SelectSingleNode("uncertaintyFixed", mgr);
            if (uncertaintyFixedNode != null && uncertaintyFixedNode.HasChildNodes)
            {
                double uncertainty;
                if (!double.TryParse(uncertaintyFixedNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out uncertainty))
                {
                    uncertainty = 0.0;
                }
                UncertaintyFixed = uncertainty;
            }

            var uncertaintyVariableNode = node.SelectSingleNode("uncertaintyVariable", mgr);
            if (uncertaintyVariableNode != null && uncertaintyVariableNode.HasChildNodes)
            {
                double uncertainty;
                if (!double.TryParse(uncertaintyVariableNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out uncertainty))
                {
                    uncertainty = 0.0;
                }
                UncertaintyVariable = uncertainty;
            }

            return this;
        }
    }
}
