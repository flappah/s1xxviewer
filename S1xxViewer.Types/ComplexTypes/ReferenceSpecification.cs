using S1xxViewer.Types.Interfaces;
using System;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class ReferenceSpecification : IReferenceSpecification
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new ReferenceSpecification
            {
                Name = Name,
                Version = Version,
                Date = Date
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
            var nameNode = node.FirstChild.SelectSingleNode("name");
            if (nameNode != null && nameNode.HasChildNodes)
            {
                Name = node.FirstChild.InnerText;
            }

            var versionNode = node.FirstChild.SelectSingleNode("version");
            if (versionNode != null && versionNode.HasChildNodes)
            {
                Version = node.FirstChild.InnerText;
            }

            var dateNode = node.FirstChild.SelectSingleNode("date");
            if (dateNode != null && dateNode.HasChildNodes)
            {
                DateTime date;
                if (!DateTime.TryParse(node.FirstChild.InnerText, out date))
                {
                    date = DateTime.MinValue;
                }
                Date = date;
            }

            return this;
        }
    }
}
