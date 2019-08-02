using S1xxViewer.Base;
using S1xxViewer.Types.Interfaces;
using System;
using System.Data;
using System.Reflection;
using System.Xml;

namespace S1xxViewer.Types
{
    public abstract class FeatureBase : IFeature
    {
        public IFeatureObjectIdentifier FeatureObjectIdentifier { get; set; }
        public string Id { get; set; }
        public ILink[] Links { get; set; }

        public abstract IFeature DeepClone();

        public abstract IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                propertyInfo.SetValue(this, null);
            }
        }

        /// <summary>
        /// Returns the properties of the current object in a dictionary of strings
        /// </summary>
        /// <returns>Dictionary<string, string></returns>
        public DataTable GetData()
        {
            var results = new DataTable($"Results_{this.GetHashCode()}");
            results.Columns.AddRange(new DataColumn[]
            {
                new DataColumn
                {
                    DataType = System.Type.GetType("System.String"),
                    ColumnName = "Name"
                },
                 new DataColumn
                {
                    DataType = System.Type.GetType("System.String"),
                    ColumnName = "Value"
                }
            });

            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                if (propertyInfo.PropertyType.FullName.Contains(GetType().FullName.Substring(0, GetType().FullName.IndexOf("."))))
                {
                    if (propertyInfo.PropertyType.IsArray)
                    {
                        var objs = GetPropertyValue(propertyInfo, propertyInfo.Name);
                        if (objs is IComplexType[])
                        {
                            foreach (IComplexType obj in (IComplexType[])objs)
                            {
                                DataTable childResults = obj.GetData();
                                foreach (DataRow childRow in childResults.Rows)
                                {
                                    var row = results.NewRow();
                                    row["Name"] = $"{propertyInfo.Name}.{childRow["Name"]}";
                                    row["Value"] = childRow["Value"];
                                    results.Rows.Add(row);
                                }
                            }
                        }
                        else if (objs is ILink[])
                        {
                            foreach (ILink obj in (ILink[])objs)
                            {
                                if (obj.LinkedFeature != null)
                                {
                                    var linkedFeatureData = obj.LinkedFeature.GetData();
                                    foreach (DataRow linkedRow in linkedFeatureData.Rows)
                                    {
                                        var row = results.NewRow();
                                        row["Name"] = $"{obj.LinkedFeature.GetType().ToString().LastPart(".")}.{linkedRow["Name"]}";
                                        row["Value"] = linkedRow["Value"];
                                        results.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var obj = GetPropertyValue(propertyInfo, propertyInfo.Name) as IComplexType;
                        if (obj != null)
                        {
                            DataTable childResults = obj.GetData();
                            foreach (DataRow childRow in childResults.Rows)
                            {
                                var row = results.NewRow();
                                row["Name"] = $"{propertyInfo.Name}.{childRow["Name"]}";
                                row["Value"] = childRow["Value"];
                                results.Rows.Add(row);
                            }
                        }
                    }
                }
                else
                {
                    var value = GetPropertyValue(propertyInfo, propertyInfo.Name);
                    if (value != null)
                    {
                        var row = results.NewRow();
                        if (propertyInfo.PropertyType.IsArray)
                        {
                            row["Name"] = propertyInfo.Name;

                            string[] arrayAsStrings;
                            if (value is double[])
                            {
                                arrayAsStrings = Array.ConvertAll((double[])value, v => v.ToString() ?? "");
                            }
                            else if (value is int[])
                            {
                                arrayAsStrings = Array.ConvertAll((int[])value, v => v.ToString() ?? "");
                            }
                            else if (value is float[])
                            {
                                arrayAsStrings = Array.ConvertAll((float[])value, v => v.ToString() ?? "");
                            }
                            else if (value is DateTime[])
                            {
                                arrayAsStrings = Array.ConvertAll((DateTime[])value, v => v.ToString("yyyy-MM-dd HH:mm:ss") ?? "");
                            }
                            else
                            {
                                arrayAsStrings = Array.ConvertAll((object[])value, v => v.ToString() ?? "");
                            }

                            row["Value"] = String.Join(",", arrayAsStrings);
                        }
                        else
                        {
                            row["Name"] = propertyInfo.Name;
                            row["Value"] = value.ToString();
                        }
                        results.Rows.Add(row);
                    }
                }
            }

            return results;
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
