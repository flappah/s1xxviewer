using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.Links;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public class ContactDetails : InformationFeatureBase, IContactDetails, IS122Feature
    {
        public string CallName { get; set; }
        public string CallSign { get; set; }
        public string CategoryOfCommPref { get; set; }
        public string CommunicationChannel { get; set; }
        public string ContactInstructions { get; set; }
        public int MMsiCode { get; set; }

        public IContactAddress[] ContactAddress { get; set; }
        public IInformation[] Information { get; set; }
        public IFrequencyPair[] FrequencyPair { get; set; }
        public IOnlineResource[] OnlineResource { get; set; }
        public IRadioCommunications[] RadioCommunications { get; set; }
        public ITelecommunications[] Telecommunications { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new ContactDetails
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
                CallName = CallName,
                CallSign = CallSign,
                CategoryOfCommPref= CategoryOfCommPref,
                CommunicationChannel = CommunicationChannel,
                ContactInstructions = ContactInstructions,
                MMsiCode  = MMsiCode,
                ContactAddress = ContactAddress == null 
                    ? new ContactAddress[0]
                    : Array.ConvertAll(ContactAddress, ca => ca.DeepClone() as IContactAddress),
                Information = Information == null
                    ? new Information[0]
                    : Array.ConvertAll(Information, i => i.DeepClone() as IInformation),
                FrequencyPair = FrequencyPair == null   
                    ? new FrequencyPair[0]
                    : Array.ConvertAll(FrequencyPair, fp => fp.DeepClone() as IFrequencyPair),
                OnlineResource = OnlineResource == null
                    ? new OnlineResource[0]
                    : Array.ConvertAll(OnlineResource, or => or.DeepClone() as IOnlineResource),
                RadioCommunications = RadioCommunications == null
                    ? new RadioCommunications[0]
                    : Array.ConvertAll(RadioCommunications, r => r.DeepClone() as IRadioCommunications),
                Telecommunications = Telecommunications == null
                    ? new Telecommunications[0]
                    : Array.ConvertAll(Telecommunications, t => t.DeepClone() as ITelecommunications),
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

            var callNameNode = node.FirstChild.SelectSingleNode("callName", mgr);
            if (callNameNode != null && callNameNode.HasChildNodes)
            {
                CallName = callNameNode.FirstChild.InnerText;
            }

            var callSignNode = node.FirstChild.SelectSingleNode("callSign", mgr);
            if (callSignNode != null && callSignNode.HasChildNodes)
            {
                CallSign = callSignNode.FirstChild.InnerText;
            }

            var categoryOfCommPrefNode = node.FirstChild.SelectSingleNode("categoryOfCommPref", mgr);
            if (categoryOfCommPrefNode != null && categoryOfCommPrefNode.HasChildNodes)
            {
                CategoryOfCommPref = categoryOfCommPrefNode.FirstChild.InnerText;
            }

            var communicationChannelNode = node.FirstChild.SelectSingleNode("communicationChannel", mgr);
            if (communicationChannelNode != null && communicationChannelNode.HasChildNodes)
            {
                CommunicationChannel = communicationChannelNode.FirstChild.InnerText;
            }

            var contactInstructionsNode = node.FirstChild.SelectSingleNode("contactInstructions", mgr);
            if (contactInstructionsNode != null && contactInstructionsNode.HasChildNodes)
            {
                ContactInstructions = contactInstructionsNode.FirstChild.InnerText;
            }

            var mmsiCodeNode = node.FirstChild.SelectSingleNode("mMSICode", mgr);
            if (mmsiCodeNode != null && mmsiCodeNode.HasChildNodes)
            {
                int mmsiCode;
                if (int.TryParse(mmsiCodeNode.FirstChild.InnerText, out mmsiCode))
                {
                    mmsiCode = 0;
                }
                MMsiCode = mmsiCode;
            }

            var contactAddressNodes = node.FirstChild.SelectNodes("contactAddress", mgr);
            if (contactAddressNodes != null && contactAddressNodes.Count > 0)
            {
                var contactAddresses = new List<ContactAddress>();
                foreach(XmlNode contactAddressNode in contactAddressNodes)
                {
                    if (contactAddressNode != null && contactAddressNode.HasChildNodes)
                    {
                        var newContactAddress = new ContactAddress();
                        newContactAddress.FromXml(contactAddressNode.FirstChild, mgr);
                        contactAddresses.Add(newContactAddress);
                    }
                }
                ContactAddress = contactAddresses.ToArray();
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
                        newInformation.FromXml(informationNode.FirstChild, mgr);
                        informations.Add(newInformation);
                    }
                }
                Information = informations.ToArray();
            }

            var frequencyPairNodes = node.FirstChild.SelectNodes("frequencyPair", mgr);
            if (frequencyPairNodes != null && frequencyPairNodes.Count > 0)
            {
                var frequencyPairs = new List<FrequencyPair>();
                foreach (XmlNode frequencyPairNode in frequencyPairNodes)
                {
                    if (frequencyPairNode != null && frequencyPairNode.HasChildNodes)
                    {
                        var newFrequencyPair = new FrequencyPair();
                        newFrequencyPair.FromXml(frequencyPairNode.FirstChild, mgr);
                        frequencyPairs.Add(newFrequencyPair);
                    }
                }
                FrequencyPair = frequencyPairs.ToArray();
            }

            var onlineResourceNodes = node.FirstChild.SelectNodes("onlineResource", mgr);
            if (onlineResourceNodes != null && onlineResourceNodes.Count > 0)
            {
                var onlineResources = new List<OnlineResource>();
                foreach (XmlNode onlineResourceNode in onlineResourceNodes)
                {
                    if (onlineResourceNode != null && onlineResourceNode.HasChildNodes)
                    {
                        var newOnlineResource = new OnlineResource();
                        newOnlineResource.FromXml(onlineResourceNode.FirstChild, mgr);
                        onlineResources.Add(newOnlineResource);
                    }
                }
                OnlineResource = onlineResources.ToArray();
            }

            var radioCommunicationNodes = node.FirstChild.SelectNodes("radiocommunications", mgr);
            if (radioCommunicationNodes != null && radioCommunicationNodes.Count > 0)
            {
                var radioCommunications = new List<RadioCommunications>();
                foreach (XmlNode radioCommunicationNode in radioCommunicationNodes)
                {
                    if (radioCommunicationNode != null && radioCommunicationNode.HasChildNodes)
                    {
                        var newRadioCommunications = new RadioCommunications();
                        newRadioCommunications.FromXml(radioCommunicationNode.FirstChild, mgr);
                        radioCommunications.Add(newRadioCommunications);
                    }
                }
                RadioCommunications = radioCommunications.ToArray();
            }

            var teleCommunicationNodes = node.FirstChild.SelectNodes("telecommunications", mgr);
            if (teleCommunicationNodes != null && teleCommunicationNodes.Count > 0)
            {
                var teleCommunications = new List<Telecommunications>();
                foreach (XmlNode teleCommunicationNode in teleCommunicationNodes)
                {
                    if (teleCommunicationNode != null && teleCommunicationNode.HasChildNodes)
                    {
                        var newTelecommunications = new Telecommunications();
                        newTelecommunications.FromXml(teleCommunicationNode.FirstChild, mgr);
                        teleCommunications.Add(newTelecommunications);
                    }
                }
                Telecommunications = teleCommunications.ToArray();
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
