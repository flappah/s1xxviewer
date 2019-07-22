﻿using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.Links
{
    public class Link : ILink
    {
        public string Href { get; set; }
        public string ArcRole { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Clones the object
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

        /// <summary>
        /// Retrieves contents from specified XmlNode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public ILink FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.Attributes.Count > 0)
            {
                Href = node.Attributes["xlink:href"].InnerText;
                ArcRole = node.Attributes["xlink:arcrole"]?.InnerText;
                Name = node.Name;
            }

            return this;
        }
    }
}
