using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;

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

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
