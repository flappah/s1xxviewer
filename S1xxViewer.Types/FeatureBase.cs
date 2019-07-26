using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S1xxViewer.Types.Interfaces;
using System.Xml;
using System;
using System.Reflection;

namespace S1xxViewer.Types
{
    public abstract class FeatureBase : IFeature
    {
        public string Id { get; set; }
        public ILink[] Links { get; set; }

        public abstract IFeature DeepClone();

        public abstract IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> SerializeProperties()
        {
            var properties = new Dictionary<string, string>();
            var typeProperties = GetType().GetProperties();
            foreach(var property in properties)
            {
                
            }

            return properties;
        }
    }
}
