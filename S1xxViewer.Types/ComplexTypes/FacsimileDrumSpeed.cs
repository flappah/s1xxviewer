using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class FacsimileDrumSpeed : ComplexTypeBase, IFacsimileDrumSpeed
    {
        public int DrumSpeed { get; set; }
        public int IndexOfCooperation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new FacsimileDrumSpeed
            {
                DrumSpeed = DrumSpeed,
                IndexOfCooperation = IndexOfCooperation
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
            var drumSpeedNode = node.FirstChild.SelectSingleNode("drumSpeed");
            if (drumSpeedNode != null && drumSpeedNode.HasChildNodes)
            {
                int drumSpeed;
                if (!int.TryParse(drumSpeedNode.FirstChild.InnerText, out drumSpeed))
                {
                    drumSpeed = 0;
                }
                DrumSpeed = drumSpeed;
            }

            var indexOfCooperationNode = node.FirstChild.SelectSingleNode("indexOfCooperation");
            if (indexOfCooperationNode != null && indexOfCooperationNode.HasChildNodes)
            {
                int index;
                if (!int.TryParse(indexOfCooperationNode.FirstChild.InnerText, out index))
                {
                    index = 0;
                }
                IndexOfCooperation = index;
            }

            return this;
        }
    }
}
