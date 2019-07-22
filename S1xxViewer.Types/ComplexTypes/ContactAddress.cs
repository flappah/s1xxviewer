using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class ContactAddress : IContactAddress
    {
        public string[] DeliveryPoint { get; set; }
        public string CityName { get; set; }
        public string AdministrativeDivision { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new ContactAddress
            {
                DeliveryPoint = DeliveryPoint == null
                    ? new string[0]
                    : Array.ConvertAll(DeliveryPoint, s => s),
                CityName = CityName,
                AdministrativeDivision = AdministrativeDivision,
                Country = Country,
                PostalCode = PostalCode
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
            var deliveryPointNodes = node.FirstChild.SelectNodes("deliveryPoint", mgr);
            if (deliveryPointNodes != null && deliveryPointNodes.Count > 0)
            {
                var deliveryPoints = new List<string>();
                foreach (XmlNode deliveryPointNode in deliveryPointNodes)
                {
                    if (deliveryPointNode != null && deliveryPointNode.HasChildNodes)
                    {
                        deliveryPoints.Add(deliveryPointNode.FirstChild.InnerText);
                    }
                }
                DeliveryPoint = deliveryPoints.ToArray();
            }

            var cityNameNode = node.FirstChild.SelectSingleNode("cityName", mgr);
            if (cityNameNode != null && cityNameNode.HasChildNodes)
            {
                CityName = cityNameNode.FirstChild.InnerText;
            }

            var administrativeDivisionNode = node.FirstChild.SelectSingleNode("administrativeDivision", mgr);
            if (administrativeDivisionNode != null && administrativeDivisionNode.HasChildNodes)
            {
                AdministrativeDivision = administrativeDivisionNode.FirstChild.InnerText;
            }

            var countryNode = node.FirstChild.SelectSingleNode("country", mgr);
            if (countryNode != null && countryNode.HasChildNodes)
            {
                Country = countryNode.FirstChild.InnerText;
            }

            var postalCodeNode = node.FirstChild.SelectSingleNode("postalCode", mgr);
            if (postalCodeNode != null && postalCodeNode.HasChildNodes)
            {
                PostalCode = postalCodeNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
