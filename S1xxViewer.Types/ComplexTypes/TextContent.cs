using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TextContent : ITextContent
    {
        public string CategoryOfText { get; set; }
        public IInformation[] Information { get; set; }
        public IOnlineResource OnlineResource { get; set; }
        public ISourceIndication SourceIndication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new TextContent
            {
                CategoryOfText = CategoryOfText,
                Information = Information == null 
                    ? new Information[0]
                    : Array.ConvertAll(Information, i => i.DeepClone() as IInformation),
                OnlineResource = OnlineResource == null 
                    ? new OnlineResource()
                    : OnlineResource.DeepClone() as IOnlineResource,
                SourceIndication = SourceIndication == null 
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public virtual IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var categoryOfTextNode = node.FirstChild.SelectSingleNode("categoryOfText", mgr);
            if (categoryOfTextNode != null && categoryOfTextNode.HasChildNodes)
            {
                CategoryOfText = categoryOfTextNode.FirstChild.InnerText;
            }

            var informationNodes = node.FirstChild.SelectNodes("information", mgr);
            if (informationNodes != null && informationNodes.Count > 0)
            {
                var informations = new List<Information>();
                foreach(XmlNode informationNode in informationNodes)
                {
                    if (informationNode != null && informationNode.HasChildNodes)
                    {
                        var newInformation = new Information();
                        newInformation.FromXml(informationNode.FirstChild, mgr);
                        informations.Add(newInformation);
                    }
                }
                Information = informations.ToArray();
            }

            var onlineResourceNode = node.FirstChild.SelectSingleNode("onlineResource");
            if (onlineResourceNode != null && onlineResourceNode.HasChildNodes)
            {
                OnlineResource = new OnlineResource();
                OnlineResource.FromXml(onlineResourceNode.FirstChild, mgr);
            }

            var sourceIndicationNode = node.FirstChild.SelectSingleNode("sourceIndication", mgr);
            if (sourceIndicationNode != null && sourceIndicationNode.HasChildNodes)
            {
                SourceIndication = new SourceIndication();
                SourceIndication.FromXml(sourceIndicationNode.FirstChild, mgr);
            }

            return this;
        }
    }
}
