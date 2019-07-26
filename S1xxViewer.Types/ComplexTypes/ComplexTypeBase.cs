using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public abstract class ComplexTypeBase : IComplexType
    {
        public abstract IComplexType DeepClone();
        public abstract IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr);

        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> SerializeProperties()
        {
            var properties = new Dictionary<string, string>();
            var typeProperties = GetType().GetProperties();
            foreach (var property in properties)
            {

            }

            return properties;
        }
    }
}
