using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class RxnCode : ComplexTypeBase, IRxnCode
    {
        public string CategoryOfRxn { get; set; }
        public string ActionOrActivity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new RxnCode
            {
                CategoryOfRxn = CategoryOfRxn,
                ActionOrActivity = ActionOrActivity
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
            var categoryOfRxnNode = node.FirstChild.SelectSingleNode("categoryOfRxn", mgr);
            if (categoryOfRxnNode != null && categoryOfRxnNode.HasChildNodes)
            {
                CategoryOfRxn = categoryOfRxnNode.FirstChild.InnerText;
            }

            var actionOrActivityNode = node.FirstChild.SelectSingleNode("actionOrActivity");
            if (actionOrActivityNode != null && actionOrActivityNode.HasChildNodes)
            {
                ActionOrActivity = actionOrActivityNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
