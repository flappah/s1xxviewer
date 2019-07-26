using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class FeatureName : ComplexTypeBase, IFeatureName
    {
        public bool DisplayName { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new FeatureName
            {
                DisplayName = DisplayName,
                Language = Language,
                Name = Name
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
            var displayNameNode = node.FirstChild.SelectSingleNode("displayName", mgr);
            if (displayNameNode != null && displayNameNode.HasChildNodes)
            {
                bool displayName;
                if (!bool.TryParse(displayNameNode.FirstChild.InnerText, out displayName))
                {
                    displayName = false;
                }
                DisplayName = displayName;
            }

            var languageNode = node.FirstChild.SelectSingleNode("language", mgr);
            if (languageNode != null && languageNode.HasChildNodes)
            {
                Language = languageNode.FirstChild.InnerText;
            }

            var nameNode = node.FirstChild.SelectSingleNode("name", mgr);
            if (nameNode != null && nameNode.HasChildNodes)
            {
                Name = nameNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
