using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class SourceIndication : ISourceIndication
    {
        public string CategoryOfAuthority { get; set; }
        public string Country { get; set; }
        public InternationalString[] FeatureName { get; set; }
        public DateTime ReportedDate { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new SourceIndication
            {
                CategoryOfAuthority = CategoryOfAuthority,
                Country = Country,
                FeatureName = FeatureName == null
                    ? new[] { new InternationalString("") }
                    : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
                ReportedDate = ReportedDate,
                Source = Source,
                SourceType = SourceType
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                var categoryOfAuthority = node.SelectSingleNode("categoryOfAuthority", mgr);
                if (categoryOfAuthority != null)
                {
                    CategoryOfAuthority = categoryOfAuthority.InnerText;
                }

                var country = node.SelectSingleNode("country", mgr);
                if (country != null)
                {
                    Country = country.InnerText;
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

                var reportedDate = node.SelectSingleNode("reportedDate", mgr);
                if (reportedDate != null)
                {
                    var reportedDateString = reportedDate.InnerText;
                    DateTime reportedDateTime;
                    if (!DateTime.TryParse(reportedDateString, out reportedDateTime))
                    {
                        reportedDateTime = DateTime.MinValue;
                    }
                    ReportedDate = reportedDateTime;
                }

                var source = node.SelectSingleNode("source", mgr);
                if (source != null)
                {
                    Source = source.InnerText;
                }

                var sourceType = node.SelectSingleNode("sourceType", mgr);
                if (sourceType != null)
                {
                    SourceType = sourceType.InnerText;
                }
            }

            return this;
        }

    }
}
