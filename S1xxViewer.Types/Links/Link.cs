using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace S1xxViewer.Types.Links
{
    public class Link : ILink
    {
        public string Href { get; set; }
        public string ArcRole { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ILink DeepClone()
        {
            return new Link
            {
                Href = Href,
                ArcRole = ArcRole,
                Name = Name
            };
        }

        public ILink FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
