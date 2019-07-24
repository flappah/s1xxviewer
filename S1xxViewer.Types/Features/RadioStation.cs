using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class RadioStation : GeoFeatureBase, IRadioStation, IS123Feature
    {
        public string CallSign { get; set; }
        public string CategoryOfRadioStation { get; set; }
        public double EstimatedRangeOffTransmission { get; set; }
        public IOrientation Orientation { get; set; }
        public IRadioStationCommunicationDescription[] RadioStationCommunicationDescription { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new RadioStation
            {
                FeatureName = FeatureName == null
                ? new[] { new InternationalString("") }
                : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
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
                CallSign = CallSign,
                CategoryOfRadioStation = CategoryOfRadioStation,
                EstimatedRangeOffTransmission = EstimatedRangeOffTransmission,
                Orientation = Orientation == null
                    ? new Orientation()
                    : Orientation.DeepClone() as IOrientation,
                RadioStationCommunicationDescription = RadioStationCommunicationDescription == null
                    ? new RadioStationCommunicationDescription[0]
                    : Array.ConvertAll(RadioStationCommunicationDescription, rcd => rcd.DeepClone() as IRadioStationCommunicationDescription),
                Status = Status,
                Links = Links == null
                    ? new Link[0]
                    : Array.ConvertAll(Links, l => l.DeepClone() as ILink)
            };
        }

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
                    newDateRange.FromXml(periodicDateRangeNode.FirstChild, mgr);
                    dateRanges.Add(newDateRange);
                }
                PeriodicDateRange = dateRanges.ToArray();
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
                        content.FromXml(textContentNode.FirstChild, mgr);
                        textContents.Add(content);
                    }
                }
                TextContent = textContents.ToArray();
            }

            var callSignNode = node.FirstChild.SelectSingleNode("callSign", mgr);
            if (callSignNode != null && callSignNode.HasChildNodes)
            {
                CallSign = callSignNode.FirstChild.InnerText;
            }

            var categoryOfRadioStationNode = node.FirstChild.SelectSingleNode("categoryOfRadioStation", mgr);
            if (categoryOfRadioStationNode != null && categoryOfRadioStationNode.HasChildNodes)
            {
                CategoryOfRadioStation = categoryOfRadioStationNode.FirstChild.InnerText;
            }

            var estimatedRangeOffTransmissionNode = node.FirstChild.SelectSingleNode("estimatedRangeOffTransmission", mgr);
            if (estimatedRangeOffTransmissionNode != null && estimatedRangeOffTransmissionNode.HasChildNodes)
            {
                double range;
                if (!double.TryParse(estimatedRangeOffTransmissionNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out range))
                {
                    range = 0.0;
                }
                EstimatedRangeOffTransmission = range;
            }

            var orientationNode = node.FirstChild.SelectSingleNode("orientation", mgr);
            if (orientationNode != null && orientationNode.HasChildNodes)
            {
                Orientation = new Orientation();
                Orientation.FromXml(orientationNode.FirstChild, mgr);
            }

            var radioStationCommunicationDescriptionNodes = node.FirstChild.SelectNodes("radioStationCommunicationDescription", mgr);
            if (radioStationCommunicationDescriptionNodes != null && radioStationCommunicationDescriptionNodes.Count > 0)
            {
                var rdoComDescriptions = new List<IRadioStationCommunicationDescription>();
                foreach (XmlNode radioStationCommunicationDescriptionNode in radioStationCommunicationDescriptionNodes)
                {
                    if (radioStationCommunicationDescriptionNode != null && radioStationCommunicationDescriptionNode.HasChildNodes)
                    {
                        var rdoComDescription = new RadioStationCommunicationDescription();
                        rdoComDescription.FromXml(radioStationCommunicationDescriptionNode.FirstChild, mgr);
                        rdoComDescriptions.Add(rdoComDescription);
                    }
                }
                RadioStationCommunicationDescription = rdoComDescriptions.ToArray();
            }

            var statusNode = node.FirstChild.SelectSingleNode("status", mgr);
            if (statusNode != null && statusNode.HasChildNodes)
            {
                Status = statusNode.FirstChild.InnerText;
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
