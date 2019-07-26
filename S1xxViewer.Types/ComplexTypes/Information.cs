using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Information : ComplexTypeBase, IInformation
    {
        public string FileDescription { get; set; }
        public string FileLocator { get; set; }
        public string Headline { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new Information
            {
                FileDescription = FileDescription,
                FileLocator = FileLocator,
                Headline = Headline,
                Language = Language,
                Text = Text
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
            var fileDescriptionNode = node.FirstChild.SelectSingleNode("fileDescription", mgr);
            if (fileDescriptionNode != null && fileDescriptionNode.HasChildNodes)
            {
                FileDescription = fileDescriptionNode.FirstChild.InnerText;
            }

            var fileLocatorNode = node.FirstChild.SelectSingleNode("fileLocator", mgr);
            if (fileLocatorNode != null && fileLocatorNode.HasChildNodes)
            {
                FileLocator = fileLocatorNode.FirstChild.InnerText;
            }
            var headlineNode = node.FirstChild.SelectSingleNode("headline", mgr);
            if (headlineNode != null && headlineNode.HasChildNodes)
            {
                Headline = headlineNode.FirstChild.InnerText;
            }

            var languageNode = node.FirstChild.SelectSingleNode("language", mgr);
            if (languageNode != null && languageNode.HasChildNodes)
            {
                Language = languageNode.FirstChild.InnerText;
            }
            var textNode = node.FirstChild.SelectSingleNode("text", mgr);
            if (textNode != null && textNode.HasChildNodes)
            {
                Text = textNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
