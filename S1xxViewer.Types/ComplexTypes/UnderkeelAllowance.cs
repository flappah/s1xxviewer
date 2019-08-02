using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class UnderkeelAllowance : ComplexTypeBase, IUnderkeelAllowance
    {
        public double UnderkeelAllowanceFixed { get; set; }
        public double UnderkeelAllowanceVariableBeamBased { get; set; }
        public double UnderkeelAllowanceVariableDraughtBased { get; set; }
        public string Operation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new UnderkeelAllowance
            {
                UnderkeelAllowanceFixed = UnderkeelAllowanceFixed,
                UnderkeelAllowanceVariableBeamBased = UnderkeelAllowanceVariableBeamBased,
                UnderkeelAllowanceVariableDraughtBased = UnderkeelAllowanceVariableDraughtBased,
                Operation = Operation
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
            var underkeelAllowanceFixedNode = node.SelectSingleNode("underkeelAllowanceFixed", mgr);
            if (underkeelAllowanceFixedNode != null && underkeelAllowanceFixedNode.HasChildNodes)
            {
                double underkeelAllowance;
                if (!double.TryParse(underkeelAllowanceFixedNode.FirstChild.InnerText, out underkeelAllowance))
                {
                    underkeelAllowance = 0.0;
                }
                UnderkeelAllowanceFixed = underkeelAllowance;
            }

            var underkeelAllowanceVariableBeamBasedNode = node.SelectSingleNode("underkeelAllowanceVariableBeamBased", mgr);
            if (underkeelAllowanceVariableBeamBasedNode != null && underkeelAllowanceVariableBeamBasedNode.HasChildNodes)
            {
                double underkeelAllowance;
                if (!double.TryParse(underkeelAllowanceVariableBeamBasedNode.FirstChild.InnerText, out underkeelAllowance))
                {
                    underkeelAllowance = 0.0;
                }
                UnderkeelAllowanceVariableBeamBased = underkeelAllowance;
            }

            var underkeelAllowanceVariableDraughtBasedNode = node.SelectSingleNode("underkeelAllowanceVariableDraughtBased", mgr);
            if (underkeelAllowanceVariableDraughtBasedNode != null && underkeelAllowanceVariableDraughtBasedNode.HasChildNodes)
            {
                double underkeelAllowance;
                if (!double.TryParse(underkeelAllowanceVariableDraughtBasedNode.FirstChild.InnerText, out underkeelAllowance))
                {
                    underkeelAllowance = 0.0;
                }
                UnderkeelAllowanceVariableDraughtBased = underkeelAllowance;
            }

            var operationNode = node.SelectSingleNode("operation", mgr);
            if (operationNode != null && operationNode.HasChildNodes)
            {
                Operation = operationNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
