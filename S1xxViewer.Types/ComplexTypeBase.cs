using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S1xxViewer.Types.Interfaces;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;

namespace S1xxViewer.Types
{
    public abstract class ComplexTypeBase : IComplexType
    {
        public abstract IComplexType DeepClone();
        public abstract IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr);

        /// <summary>
        /// Returns the properties of the current object in a dictionary of strings
        /// </summary>
        /// <returns>Dictionary<string, string></returns>
        public Dictionary<string, string> GetData()
        {
            var properties = new Dictionary<string, string>();
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                if (propertyInfo.PropertyType.FullName.Contains(GetType().FullName.Substring(0, GetType().FullName.IndexOf("."))))
                {
                    if (propertyInfo.PropertyType.IsArray)
                    {
                        var objs = GetPropertyValue(propertyInfo, propertyInfo.Name) as IComplexType[];
                        if (objs != null)
                        {
                            foreach (IComplexType obj in objs)
                            {
                                Dictionary<string, string> childProperties = obj.GetData();
                                foreach (var childProperty in childProperties)
                                {
                                    properties.Add(childProperty.Key, childProperty.Value);
                                }
                            }
                        }
                    }
                    else
                    {
                        var obj = GetPropertyValue(propertyInfo, propertyInfo.Name) as IComplexType;
                        if (obj != null)
                        {
                            Dictionary<string, string> childProperties = obj.GetData();
                            foreach (var childProperty in childProperties)
                            {
                                properties.Add(childProperty.Key, childProperty.Value);
                            }
                        }
                    }
                }
                else
                {
                    var value = GetPropertyValue(propertyInfo, propertyInfo.Name);
                    if (value != null)
                    {
                        if (propertyInfo.PropertyType.IsArray)
                        {
                            properties.Add(propertyInfo.Name, String.Join(",", Array.ConvertAll<object, string>((object[])value, v => v?.ToString() ?? string.Empty)));
                        }
                        else
                        {
                            properties.Add(propertyInfo.Name, value.ToString());
                        }
                    }
                }
            }

            return properties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcobj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private object GetPropertyValue(PropertyInfo pi, string propertyName)
        {
            if (pi == null)
                return null;

            // Split property name to parts (propertyName could be hierarchical, like obj.subobj.subobj.property
            string[] propertyNameParts = propertyName.Split('.');

            foreach (string propertyNamePart in propertyNameParts)
            {
                // propertyNamePart could contain reference to specific 
                // element (by index) inside a collection
                if (!propertyNamePart.Contains("["))
                {
                    if (pi == null) return null;
                    return pi.GetValue(this, null);
                }
                else
                {   // propertyNamePart is areference to specific element 
                    // (by index) inside a collection
                    // like AggregatedCollection[123]
                    //   get collection name and element index
                    int indexStart = propertyNamePart.IndexOf("[") + 1;
                    string collectionPropertyName = propertyNamePart.Substring(0, indexStart - 1);
                    int collectionElementIndex = Int32.Parse(propertyNamePart.Substring(indexStart, propertyNamePart.Length - indexStart - 1));
                    //   get collection object

                    if (pi == null) return null;
                    object unknownCollection = pi.GetValue(this, null);
                    //   try to process the collection as array
                    if (unknownCollection.GetType().IsArray)
                    {
                        object[] collectionAsArray = unknownCollection as Array[];
                        return collectionAsArray[collectionElementIndex];
                    }
                    else
                    {
                        //   try to process the collection as IList
                        System.Collections.IList collectionAsList = unknownCollection as System.Collections.IList;
                        if (collectionAsList != null)
                        {
                            return collectionAsList[collectionElementIndex];
                        }
                        else
                        {
                            // ??? Unsupported collection type
                        }
                    }
                }
            }

            return "";
        }
    }
}
