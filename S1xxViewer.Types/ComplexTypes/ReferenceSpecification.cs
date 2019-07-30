using S1xxViewer.Types.Interfaces;
using System;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class ReferenceSpecification : ComplexTypeBase, IReferenceSpecification
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
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
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var nameNode = node.SelectSingleNode("name");
            if (nameNode != null && nameNode.HasChildNodes)
            {
                Name = node.FirstChild.InnerText;
            }

            var versionNode = node.SelectSingleNode("version");
            if (versionNode != null && versionNode.HasChildNodes)
            {
                Version = node.FirstChild.InnerText;
            }

            var dateNode = node.SelectSingleNode("date");
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
