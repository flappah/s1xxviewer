using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types.Interfaces;
using System.Linq;
using System.Xml;

namespace S1xxViewer.Model
{
    public class FeatureFactory : IFeatureFactory
    {
        /// <summary>
        /// Uses Autofac to insert all existing IFeature's in this property. 
        /// </summary>
        public IFeature[] Features { get; set; }

        /// <summary>
        /// Uses specified XMLNode to determine type of feature to create.
        /// </summary>
        /// <param name="node">XmlNode</param>
        /// <param name="mgr">XmlNamespaceManager</param>
        /// <returns>IFeature</returns>
        public IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null)
            {
                // determine the typestring of the feature we're looking for
                var featureTypeString = (node.HasChildNodes ? node.ChildNodes[0].Name : "").LastPart(char.Parse(":"));

                // look for the feature in the collection of features Autofac initialized and inserted in the Features property
                var locatedFeature =
                    Features.ToList().Find(tp => tp.GetType().Name.Contains(featureTypeString));

                // if there's a feature, start XML parsing it and return the feature
                if (locatedFeature != null)
                {
                    // just to make sure to have a copy of the autofac feature
                    var clonedFeature = locatedFeature.DeepClone() as IFeature;
                    if (clonedFeature != null)
                    {
                        // clear the feature of the original content
                        clonedFeature.Clear();
                        // and parse xml content into it
                        clonedFeature.FromXml(node, mgr);
                        return clonedFeature;
                    }
                }
            }

            return null;
        }
    }
}
