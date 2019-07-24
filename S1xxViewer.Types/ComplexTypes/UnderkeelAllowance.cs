using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class UnderkeelAllowance : IUnderkeelAllowance
    {
        public double UnderkeelAllowanceFixed { get; set; }
        public double UnderkeelAllowanceVariableBeamBased { get; set; }
        public double UnderkeelAllowanceVariableDraughtBased { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new UnderkeelAllowance
            {
                UnderkeelAllowanceFixed = UnderkeelAllowanceFixed,
                UnderkeelAllowanceVariableBeamBased = UnderkeelAllowanceVariableBeamBased,
                UnderkeelAllowanceVariableDraughtBased = UnderkeelAllowanceVariableDraughtBased
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
            var underkeelAllowanceFixedNode = node.FirstChild.SelectSingleNode("underkeelAllowanceFixed", mgr);
            if (underkeelAllowanceFixedNode != null && underkeelAllowanceFixedNode.HasChildNodes)
            {
                double underkeelAllowance;
                if (!double.TryParse(underkeelAllowanceFixedNode.FirstChild.InnerText, out underkeelAllowance))
                {
                    underkeelAllowance = 0.0;
                }
                UnderkeelAllowanceFixed = underkeelAllowance;
            }

            var underkeelAllowanceVariableBeamBasedNode = node.FirstChild.SelectSingleNode("underkeelAllowanceVariableBeamBased", mgr);
            if (underkeelAllowanceVariableBeamBasedNode != null && underkeelAllowanceVariableBeamBasedNode.HasChildNodes)
            {
                double underkeelAllowance;
                if (!double.TryParse(underkeelAllowanceVariableBeamBasedNode.FirstChild.InnerText, out underkeelAllowance))
                {
                    underkeelAllowance = 0.0;
                }
                UnderkeelAllowanceVariableBeamBased = underkeelAllowance;
            }

            var underkeelAllowanceVariableDraughtBasedNode = node.FirstChild.SelectSingleNode("underkeelAllowanceVariableDraughtBasedNode", mgr);
            if (underkeelAllowanceVariableDraughtBasedNode != null && underkeelAllowanceVariableDraughtBasedNode.HasChildNodes)
            {
                double underkeelAllowance;
                if (!double.TryParse(underkeelAllowanceVariableDraughtBasedNode.FirstChild.InnerText, out underkeelAllowance))
                {
                    underkeelAllowance = 0.0;
                }
                UnderkeelAllowanceVariableDraughtBased = underkeelAllowance;
            }

            return this;
        }
    }
}
