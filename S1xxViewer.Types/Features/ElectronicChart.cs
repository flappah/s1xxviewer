using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class ElectronicChart : ChartProductBase, IElectronicChart
    {
        public string[] DatasetName { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateNumber { get; set; }

        public IReferenceSpecification ProductSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new ElectronicChart
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
                ChartNumber = ChartNumber,
                DistributionStatus = DistributionStatus,
                CompilationScale = CompilationScale,
                EditionNumber = EditionNumber,
                SpecificUsage = SpecificUsage,
                ProducerCode = ProducerCode,
                ProducerNation = ProducerNation,
                DatasetName = DatasetName == null
                    ? new string[0]
                    : Array.ConvertAll(DatasetName, s => s),
                UpdateDate = UpdateDate,
                UpdateNumber = UpdateNumber,
                ProductSpecification = ProductSpecification == null
                    ? new ReferenceSpecification()
                    : ProductSpecification.DeepClone() as IReferenceSpecification,
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

            var copyrightNode = node.FirstChild.SelectSingleNode("copyright");
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
                DateTime issueDate;
                if (!DateTime.TryParse(issueDateNode.FirstChild.InnerText, out issueDate))
                {
                    issueDate = DateTime.MinValue;
                }
                IssueDate = issueDate;
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

            var chartNumberNode = node.FirstChild.SelectSingleNode("chartNumber", mgr);
            if (chartNumberNode != null && chartNumberNode.HasChildNodes)
            {
                ChartNumber = chartNumberNode.FirstChild.InnerText;
            }

            var distributionStatusNode = node.FirstChild.SelectSingleNode("distributionStatus", mgr);
            if (distributionStatusNode != null && distributionStatusNode.HasChildNodes)
            {
                DistributionStatus = distributionStatusNode.FirstChild.InnerText;
            }

            var compilationScaleNode = node.FirstChild.SelectSingleNode("compilationScale", mgr);
            if (compilationScaleNode != null && compilationScaleNode.HasChildNodes)
            {
                CompilationScale = compilationScaleNode.FirstChild.InnerText;
            }

            var editionNumberNode = node.FirstChild.SelectSingleNode("editionNumber", mgr);
            if (editionNumberNode != null && editionNumberNode.HasChildNodes)
            {
                int editionNumber;
                if (!int.TryParse(editionNumberNode.FirstChild.InnerText, out editionNumber))
                {
                    editionNumber = -1;
                }
                EditionNumber = editionNumber;
            }

            var specificUsageNode = node.FirstChild.SelectSingleNode("specificUsage", mgr);
            if (specificUsageNode != null && specificUsageNode.HasChildNodes)
            {
                SpecificUsage = specificUsageNode.FirstChild.InnerText;
            }

            var producerCodeNode = node.FirstChild.SelectSingleNode("producerCode", mgr);
            if (producerCodeNode != null && producerCodeNode.HasChildNodes)
            {
                ProducerCode = producerCodeNode.FirstChild.InnerText;
            }

            var producerNationNode = node.FirstChild.SelectSingleNode("producerNation", mgr);
            if (producerNationNode != null && producerNationNode.HasChildNodes)
            {
                ProducerNation = producerNationNode.FirstChild.InnerText;
            }

            var datasetNameNodes = node.FirstChild.SelectNodes("datasetName", mgr);
            if (datasetNameNodes != null && datasetNameNodes.Count > 0)
            {
                var datasets = new List<string>();
                foreach (XmlNode datasetNameNode in datasetNameNodes)
                {
                    if (datasetNameNode != null && datasetNameNode.HasChildNodes)
                    {
                        datasets.Add(datasetNameNode.FirstChild.InnerText);
                    }
                }
                DatasetName = datasets.ToArray();
            }

            var updateDateNode = node.FirstChild.SelectSingleNode("updateDate", mgr);
            if (updateDateNode != null && updateDateNode.HasChildNodes)
            {
                DateTime updateDate;
                if (!DateTime.TryParse(updateDateNode.FirstChild.InnerText, out updateDate))
                {
                    updateDate = DateTime.MinValue;
                }
                UpdateDate = updateDate;
            }

            var updateNumberNode = node.FirstChild.SelectSingleNode("updateNumber", mgr);
            if (updateNumberNode != null && updateNumberNode.HasChildNodes)
            {
                int updateNumber;
                if (!int.TryParse(updateNumberNode.FirstChild.InnerText, out updateNumber))
                {
                    updateNumber = -1;
                }
                UpdateNumber = updateNumber;
            }

            var productSpecificationNode = node.FirstChild.SelectSingleNode("productSpecification", mgr);
            if (productSpecificationNode != null && productSpecificationNode.HasChildNodes)
            {
                ProductSpecification = new ReferenceSpecification();
                ProductSpecification.FromXml(productSpecificationNode, mgr);
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
