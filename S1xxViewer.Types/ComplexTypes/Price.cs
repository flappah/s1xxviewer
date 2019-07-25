using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Price : IPrice
    {
        public int PriceNumber { get; set; }
        public string Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
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
        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var priceNumberNode = node.FirstChild.SelectSingleNode("priceNumber");
            if (priceNumberNode != null && priceNumberNode.HasChildNodes)
            {
                int price;
                if (!int.TryParse(priceNumberNode.FirstChild.InnerText, out price))
                {
                    price = -1;
                }
                PriceNumber = price;
            }

            var currencyNode = node.FirstChild.SelectSingleNode("currency");
            if (currencyNode != null && currencyNode.HasChildNodes)
            {
                Currency = currencyNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
