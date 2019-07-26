using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class ShipReport : InformationFeatureBase, IShipReport, IS122Feature
    {
        public string[] CategoryOfShipReport { get; set; }
        public bool ImoFormatForReporting { get; set; }
        public INoticeTime[] NoticeTime { get; set; }
        public ITextContent TextContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new ShipReport
            {
                FeatureName = FeatureName == null
                    ? new[] { new FeatureName() }
                    : Array.ConvertAll(FeatureName, fn => fn.DeepClone() as IFeatureName),
                FixedDateRange = FixedDateRange == null
                    ? new DateRange()
                    : FixedDateRange.DeepClone() as IDateRange,
                Id = Id,
                PeriodicDateRange = PeriodicDateRange == null
                    ? new DateRange[0]
                    : Array.ConvertAll(PeriodicDateRange, p => p.DeepClone() as IDateRange),
                SourceIndication = SourceIndication == null
                    ? new SourceIndication[0]
                    : Array.ConvertAll(SourceIndication, s => s.DeepClone() as ISourceIndication),
                CategoryOfShipReport = CategoryOfShipReport == null
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfShipReport, s => s),
                ImoFormatForReporting = ImoFormatForReporting,
                NoticeTime = NoticeTime == null
                    ? new NoticeTime[0]
                    : Array.ConvertAll(NoticeTime, nt => nt.DeepClone() as INoticeTime),
                TextContent = TextContent == null
                    ? new TextContent()
                    : TextContent.DeepClone() as ITextContent,
                Links = Links == null
                    ? new Link[0]
                    : Array.ConvertAll(Links, l => l.DeepClone() as ILink)
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
                var featureNames = new List<FeatureName>();
                foreach (XmlNode featureNameNode in featureNameNodes)
                {
                    var newFeatureName = new FeatureName();
                    newFeatureName.FromXml(featureNameNode.FirstChild, mgr);
                    featureNames.Add(newFeatureName);
                }
                FeatureName = featureNames.ToArray();
            }

            var fixedDateRangeNode = node.FirstChild.SelectSingleNode("fixedDateRange", mgr);
            if (fixedDateRangeNode != null && fixedDateRangeNode.HasChildNodes)
            {
                FixedDateRange = new DateRange();
                FixedDateRange.FromXml(fixedDateRangeNode.FirstChild, mgr);
            }

            var periodicDateRangeNodes = node.FirstChild.SelectNodes("periodicDateRange", mgr);
            if (periodicDateRangeNodes != null && periodicDateRangeNodes.Count > 0)
            {
                var dateRanges = new List<DateRange>();
                foreach (XmlNode periodicDateRangeNode in periodicDateRangeNodes)
                {
                    var newDateRange = new DateRange();
                    newDateRange.FromXml(periodicDateRangeNode.FirstChild, mgr);
                    dateRanges.Add(newDateRange);
                }
                PeriodicDateRange = dateRanges.ToArray();
            }

            var sourceIndicationNodes = node.FirstChild.SelectNodes("sourceIndication", mgr);
            if (sourceIndicationNodes != null && sourceIndicationNodes.Count > 0)
            {
                var sourceIndications = new List<SourceIndication>();
                foreach (XmlNode sourceIndicationNode in sourceIndicationNodes)
                {
                    if (sourceIndicationNode != null && sourceIndicationNode.HasChildNodes)
                    {
                        var sourceIndication = new SourceIndication();
                        sourceIndication.FromXml(sourceIndicationNode.FirstChild, mgr);
                        sourceIndications.Add(sourceIndication);
                    }
                }
                SourceIndication = sourceIndications.ToArray();
            }

            var categoryOfShipReportNodes = node.FirstChild.SelectNodes("categoryOfShipReport");
            if (categoryOfShipReportNodes != null && categoryOfShipReportNodes.Count > 0)
            {
                var categories = new List<string>();
                foreach(XmlNode categoryOfShipReportNode in categoryOfShipReportNodes)
                {
                    if (categoryOfShipReportNode != null && categoryOfShipReportNode.HasChildNodes)
                    {
                        categories.Add(categoryOfShipReportNode.FirstChild.InnerText);
                    }
                }
            }

            var imoFormatForReportingNode = node.FirstChild.SelectSingleNode("imoFormatForReporting", mgr);
            if (imoFormatForReportingNode != null && imoFormatForReportingNode.HasChildNodes)
            {
                bool imoFormatForReporting;
                if (!bool.TryParse(imoFormatForReportingNode.FirstChild.InnerText, out imoFormatForReporting))
                {
                    imoFormatForReporting = false;
                }
                ImoFormatForReporting = imoFormatForReporting;
            }

            var noticeTimeNodes = node.FirstChild.SelectNodes("noticeTime");
            if (noticeTimeNodes != null && noticeTimeNodes.Count > 0)
            {
                var noticeTimes = new List<NoticeTime>();
                foreach (XmlNode noticeTimeNode in noticeTimeNodes)
                {
                    if (noticeTimeNode != null && noticeTimeNode.HasChildNodes)
                    {
                        var newNoticeTime = new NoticeTime();
                        newNoticeTime.FromXml(noticeTimeNode.FirstChild, mgr);
                        noticeTimes.Add(newNoticeTime);
                    }
                }
                NoticeTime = noticeTimes.ToArray();
            }

            var textContentNode = node.FirstChild.SelectSingleNode("textContent", mgr);
            if (textContentNode != null && textContentNode.HasChildNodes)
            {
                TextContent = new TextContent();
                TextContent.FromXml(textContentNode.FirstChild, mgr);
            }

            var linkNodes = node.FirstChild.SelectNodes("*[boolean(@xlink:href)]", mgr);
            if (linkNodes != null && linkNodes.Count > 0)
            {
                var links = new List<Link>();
                foreach (XmlNode linkNode in linkNodes)
                {
                    var newLink = new Link();
                    newLink.FromXml(linkNode, mgr);
                    links.Add(newLink);
                }
                Links = links.ToArray();
            }

            return this;
        }
    }
}
