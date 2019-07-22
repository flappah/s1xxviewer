using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.ComplexTypes;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class HorizontalPositionalUncertainty : IHorizontalPositionalUncertainty
    {
        public double UncertaintyFixed { get; set; }
        public double UncertaintyVariable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new HorizontalPositionalUncertainty
            {
                UncertaintyFixed = UncertaintyFixed,
                UncertaintyVariable = UncertaintyVariable
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
