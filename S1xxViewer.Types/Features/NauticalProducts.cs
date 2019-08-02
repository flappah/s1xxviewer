using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class NauticalProducts : ProductBase, INauticalProducts
    {
        public string PublicationNumber { get; set; }
        public string DataSetName { get; set; }
        public string Version { get; set; }
        public string ServiceStatus { get; set; }
        public string Keywords { get; set; }

        public IReferenceSpecification ProductSpecification { get; set; }
        public IOnlineResource OnlineResource { get; set; }
        public IReferenceSpecification ServiceSpecification { get; set; }
        public IReferenceSpecification ServiceDesign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new NauticalProducts
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
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication,
                TextContent = TextContent == null
                    ? new TextContent[0]
                    : Array.ConvertAll(TextContent, t => t.DeepClone() as ITextContent),
                Geometry = Geometry,
                Classification = Classification,
                Copyright = Copyright,
                MaximumDisplayScale = MaximumDisplayScale,
                HorizontalDatumReference = HorizontalDatumReference,
                HorizontalDatumValue = HorizontalDatumValue,
                VerticalDatum = VerticalDatum,
                SoundingDatum = SoundingDatum,
                ProductType = ProductType,
                MinimumDisplayScale = MinimumDisplayScale,
                IssueDate = IssueDate,
                Purpose = Purpose,
                Information = Information == null
                    ? new Information[0]
                    : Array.ConvertAll(Information, i => i.DeepClone() as IInformation),
                Price = Price == null
                    ? new Price[0]
                    : Array.ConvertAll(Price, p => p.DeepClone() as IPrice),
                ProducingAgency = ProducingAgency == null
                    ? new ProducingAgency()
                    : ProducingAgency.DeepClone() as IProducingAgency,
                PublicationNumber = PublicationNumber,
                DataSetName = DataSetName,
                Version = Version,
                ServiceStatus = ServiceStatus,
                Keywords = Keywords,
                ProductSpecification = ProductSpecification == null
                    ? new ReferenceSpecification()
                    : ProductSpecification.DeepClone() as IReferenceSpecification,
                OnlineResource = OnlineResource == null
                    ? new OnlineResource()
                    : OnlineResource.DeepClone() as IOnlineResource,
                ServiceSpecification = ServiceSpecification == null
                    ? new ReferenceSpecification()
                    : ServiceSpecification.DeepClone() as IReferenceSpecification,
                ServiceDesign = ServiceDesign == null
                    ? new ReferenceSpecification()
                    : ServiceDesign.DeepClone() as IReferenceSpecification,
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

            var periodicDateRangeNodes = node.FirstChild.SelectNodes("periodicDateRange", mgr);
            if (periodicDateRangeNodes != null && periodicDateRangeNodes.Count > 0)
            {
                var dateRanges = new List<DateRange>();
                foreach (XmlNode periodicDateRangeNode in periodicDateRangeNodes)
                {
                    var newDateRange = new DateRange();
                    newDateRange.FromXml(periodicDateRangeNode, mgr);
                    dateRanges.Add(newDateRange);
                }
                PeriodicDateRange = dateRanges.ToArray();
            }

            var fixedDateRangeNode = node.FirstChild.SelectSingleNode("fixedDateRange", mgr);
            if (fixedDateRangeNode != null && fixedDateRangeNode.HasChildNodes)
            {
                FixedDateRange = new DateRange();
                FixedDateRange.FromXml(fixedDateRangeNode, mgr);
            }

            var featureNameNodes = node.FirstChild.SelectNodes("featureName", mgr);
            if (featureNameNodes != null && featureNameNodes.Count > 0)
            {
                var featureNames = new List<FeatureName>();
                foreach (XmlNode featureNameNode in featureNameNodes)
                {
                    var newFeatureName = new FeatureName();
                    newFeatureName.FromXml(featureNameNode, mgr);
                    featureNames.Add(newFeatureName);
                }
                FeatureName = featureNames.ToArray();
            }

            var sourceIndication = node.FirstChild.SelectSingleNode("sourceIndication", mgr);
            if (sourceIndication != null && sourceIndication.HasChildNodes)
            {
                SourceIndication = new SourceIndication();
                SourceIndication.FromXml(sourceIndication, mgr);
            }

            var textContentNodes = node.FirstChild.SelectNodes("textContent", mgr);
            if (textContentNodes != null && textContentNodes.Count > 0)
            {
                var textContents = new List<TextContent>();
                foreach (XmlNode textContentNode in textContentNodes)
                {
                    if (textContentNode != null && textContentNode.HasChildNodes)
                    {
                        var content = new TextContent();
                        content.FromXml(textContentNode, mgr);
                        textContents.Add(content);
                    }
                }
                TextContent = textContents.ToArray();
            }

            var classificationNode = node.FirstChild.SelectSingleNode("classification", mgr);
            if (classificationNode != null && classificationNode.HasChildNodes)
            {
                Classification = classificationNode.FirstChild.InnerText;
            }

            var copyrightNode = node.FirstChild.SelectSingleNode("copyright", mgr);
            if (copyrightNode != null && copyrightNode.HasChildNodes)
            {
                Copyright = copyrightNode.FirstChild.InnerText;
            }

            var maximumDisplayScaleNode = node.FirstChild.SelectSingleNode("maximumDisplayScale", mgr);
            if (maximumDisplayScaleNode != null && maximumDisplayScaleNode.HasChildNodes)
            {
                MaximumDisplayScale = maximumDisplayScaleNode.FirstChild.InnerText;
            }

            var horizontalDatumReferenceNode = node.FirstChild.SelectSingleNode("horizontalDatumReference", mgr);
            if (horizontalDatumReferenceNode != null && horizontalDatumReferenceNode.HasChildNodes)
            {
                HorizontalDatumReference = horizontalDatumReferenceNode.FirstChild.InnerText;
            }

            var horizontalDatumValueNode = node.FirstChild.SelectSingleNode("horizontalDatumValue", mgr);
            if (horizontalDatumValueNode != null && horizontalDatumValueNode.HasChildNodes)
            {
                double value;
                if (!double.TryParse(horizontalDatumValueNode.FirstChild.InnerText, out value))
                {
                    value = -1.0;
                }
                HorizontalDatumValue = value;
            }

            var verticalDatumNode = node.FirstChild.SelectSingleNode("verticalDatum", mgr);
            if (verticalDatumNode != null && verticalDatumNode.HasChildNodes)
            {
                VerticalDatum = verticalDatumNode.FirstChild.InnerText;
            }

            var soundingDatumNode = node.FirstChild.SelectSingleNode("soundingDatum", mgr);
            if (soundingDatumNode != null && soundingDatumNode.HasChildNodes)
            {
                SoundingDatum = soundingDatumNode.FirstChild.InnerText;
            }

            var productTypeNode = node.FirstChild.SelectSingleNode("productType", mgr);
            if (productTypeNode != null && productTypeNode.HasChildNodes)
            {
                ProductType = productTypeNode.FirstChild.InnerText;
            }

            var minimumDisplayScaleNode = node.FirstChild.SelectSingleNode("minimumDisplayScale", mgr);
            if (minimumDisplayScaleNode != null && minimumDisplayScaleNode.HasChildNodes)
            {
                MinimumDisplayScale = minimumDisplayScaleNode.FirstChild.InnerText;
            }

            var issueDateNode = node.FirstChild.SelectSingleNode("issueDate", mgr);
            if (issueDateNode != null && issueDateNode.HasChildNodes)
            {
                IssueDate = issueDateNode.FirstChild.InnerText;
            }

            var purposeNode = node.FirstChild.SelectSingleNode("purpose", mgr);
            if (purposeNode != null && purposeNode.HasChildNodes)
            {
                Purpose = purposeNode.FirstChild.InnerText;
            }

            var informationNodes = node.FirstChild.SelectNodes("information", mgr);
            if (informationNodes != null && informationNodes.Count > 0)
            {
                var informations = new List<Information>();
                foreach (XmlNode informationNode in informationNodes)
                {
                    if (informationNode != null && informationNode.HasChildNodes)
                    {
                        var newInformation = new Information();
                        newInformation.FromXml(informationNode, mgr);
                        informations.Add(newInformation);
                    }
                }
                Information = informations.ToArray();
            }

            var priceNodes = node.FirstChild.SelectNodes("price", mgr);
            if (priceNodes != null && priceNodes.Count > 0)
            {
                var prices = new List<Price>();
                foreach (XmlNode priceNode in priceNodes)
                {
                    if (priceNode != null && priceNode.HasChildNodes)
                    {
                        var newPrice = new Price();
                        newPrice.FromXml(priceNode, mgr);
                        prices.Add(newPrice);
                    }
                }
                Price = prices.ToArray();
            }

            var producingAgencyNode = node.FirstChild.SelectSingleNode("producingAgency", mgr);
            if (producingAgencyNode != null && producingAgencyNode.HasChildNodes)
            {
                ProducingAgency = new ProducingAgency();
                ProducingAgency.FromXml(producingAgencyNode, mgr);
            }

            var publicationNumberNode = node.FirstChild.SelectSingleNode("publicationNumber", mgr);
            if (publicationNumberNode != null && publicationNumberNode.HasChildNodes)
            {
                PublicationNumber = publicationNumberNode.FirstChild.InnerText;
            }

            var dataSetNameNode = node.FirstChild.SelectSingleNode("dataSetName", mgr);
            if (dataSetNameNode != null && dataSetNameNode.HasChildNodes)
            {
                DataSetName = dataSetNameNode.FirstChild.InnerText;
            }

            var versionNode = node.FirstChild.SelectSingleNode("version", mgr);
            if (versionNode != null && versionNode.HasChildNodes)
            {
                Version = versionNode.FirstChild.InnerText;
            }

            var serviceStatusNode = node.FirstChild.SelectSingleNode("serviceStatus", mgr);
            if (serviceStatusNode != null && serviceStatusNode.HasChildNodes)
            {
                ServiceStatus = serviceStatusNode.FirstChild.InnerText;
            }

            var keywordsNode = node.FirstChild.SelectSingleNode("keywords", mgr);
            if (keywordsNode != null && keywordsNode.HasChildNodes)
            {
                Keywords = keywordsNode.FirstChild.InnerText;
            }

            var productSpecificationNode = node.FirstChild.SelectSingleNode("productSpecification", mgr);
            if (productSpecificationNode != null && productSpecificationNode.HasChildNodes)
            {
                ProductSpecification = new ReferenceSpecification();
                ProductSpecification.FromXml(productSpecificationNode, mgr);
            }

            var onlineResourceNode = node.FirstChild.SelectSingleNode("onlineResource", mgr);
            if (onlineResourceNode != null && onlineResourceNode.HasChildNodes)
            {
                OnlineResource = new OnlineResource();
                OnlineResource.FromXml(onlineResourceNode, mgr);
            }

            var serviceSpecificationNode = node.FirstChild.SelectSingleNode("serviceSpecification", mgr);
            if (serviceSpecificationNode != null && serviceSpecificationNode.HasChildNodes)
            {
                ServiceSpecification = new ReferenceSpecification();
                ServiceSpecification.FromXml(serviceSpecificationNode, mgr);
            }

            var serviceDesignNode = node.FirstChild.SelectSingleNode("serviceDesign", mgr);
            if (serviceDesignNode != null && serviceDesignNode.HasChildNodes)
            {
                ServiceDesign = new ReferenceSpecification();
                ServiceDesign.FromXml(serviceDesignNode, mgr);
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
