using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Features
{
    [Serializable]
    public class Authority : InformationFeatureBase, IAuthority
    {
        public string CategoryOfAuthority { get; set; }
        public InternationalString[] FeatureName { get; set; }
        public IContactDetails[] ContactDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new Authority
            {
                FeatureName = FeatureName == null 
                    ? new[] { new InternationalString("") } 
                    : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
                CategoryOfAuthority = CategoryOfAuthority ?? "",
                ContactDetails = ContactDetails == null 
                    ? new[] { new ContactDetails() } 
                    : Array.ConvertAll(ContactDetails, cd => cd.DeepClone() as IContactDetails),
                Id = Id ?? ""
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                if (node.FirstChild.Attributes.Count > 0)
                {
                    Id = node.FirstChild.Attributes["gml:id"].InnerText;
                }
            }

            var featureNameNodes = node.FirstChild.SelectNodes("featureName", mgr);
            if (featureNameNodes != null && featureNameNodes.Count > 0)
            {
                var featureNames = new List<InternationalString>();
                foreach (XmlNode featureNameNode in featureNameNodes)
                {
                    var language = featureNameNode.SelectSingleNode("language", mgr)?.InnerText ?? "";
                    var name = featureNameNode.SelectSingleNode("name", mgr)?.InnerText ?? "";
                    featureNames.Add(new InternationalString(name, language));
                }
                FeatureName = featureNames.ToArray();
            }

            var categoryOfAuthorityNode = node.FirstChild.SelectSingleNode("categoryOfAuthority", mgr);
            if (categoryOfAuthorityNode != null)
            {
                CategoryOfAuthority = categoryOfAuthorityNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
