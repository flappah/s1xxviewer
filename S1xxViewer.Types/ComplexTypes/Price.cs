using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Price : ComplexTypeBase, IPrice
    {
        public int PriceNumber { get; set; }
        public string Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new Price
            {
                PriceNumber = PriceNumber,
                Currency = Currency
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
            var priceNumberNode = node.SelectSingleNode("priceNumber");
            if (priceNumberNode != null && priceNumberNode.HasChildNodes)
            {
                int price;
                if (!int.TryParse(priceNumberNode.FirstChild.InnerText, out price))
                {
                    price = -1;
                }
                PriceNumber = price;
            }

            var currencyNode = node.SelectSingleNode("currency");
            if (currencyNode != null && currencyNode.HasChildNodes)
            {
                Currency = currencyNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
