using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types.Interfaces;
using System.Linq;
using System.Xml;

namespace S1xxViewer.Model
{
    public class FeatureFactory : IFeatureFactory
    {
        public IFeature[] Features { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null)
            {
                // determine the typestring of the feature we're looking for
                var featureTypeString = (node.HasChildNodes ? node.ChildNodes[0].Name : "").LastPart(char.Parse(":"));

                var locatedFeature =
                    Features.ToList().Find(tp => tp.GetType().Name.Contains(featureTypeString));
                locatedFeature.FromXml(node, mgr);

                return locatedFeature;
            }

            return null;
        }
    }
}
