﻿using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class ProducingAgency : ComplexTypeBase, IProducingAgency
    {
        public string IndividualName { get; set; }
        public string OrganizationName { get; set; }
        public string PositionName { get; set; }
        public IContactAddress ContactAddress { get; set; }
        public IOnlineResource OnlineResource { get; set; }
        public ITelecommunications Telecommunications { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new ProducingAgency
            {
                IndividualName = IndividualName,
                OrganizationName = OrganizationName,
                PositionName = PositionName,
                ContactAddress = ContactAddress == null
                    ? new ContactAddress()
                    : ContactAddress.DeepClone() as IContactAddress,
                OnlineResource = OnlineResource == null
                    ? new OnlineResource()
                    : OnlineResource.DeepClone() as IOnlineResource,
                Telecommunications = Telecommunications == null
                    ? new Telecommunications()
                    : Telecommunications.DeepClone() as ITelecommunications
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var individualNameNode = node.FirstChild.SelectSingleNode("individualName");
            if (individualNameNode != null && individualNameNode.HasChildNodes)
            {
                IndividualName = individualNameNode.FirstChild.InnerText;
            }

            var organizationNameNode = node.FirstChild.SelectSingleNode("organizationName");
            if (organizationNameNode != null && organizationNameNode.HasChildNodes)
            {
                OrganizationName = organizationNameNode.FirstChild.InnerText;
            }

            var positionNameNode = node.FirstChild.SelectSingleNode("positionName");
            if (positionNameNode != null && positionNameNode.HasChildNodes)
            {
                PositionName = positionNameNode.FirstChild.InnerText;
            }

            var contactAddressNode = node.FirstChild.SelectSingleNode("contactAddress");
            if (contactAddressNode != null && contactAddressNode.HasChildNodes)
            {
                ContactAddress = new ContactAddress();
                ContactAddress.FromXml(contactAddressNode.FirstChild, mgr);
            }

            var onlineResourceNode = node.FirstChild.SelectSingleNode("onlineResource");
            if (onlineResourceNode != null && onlineResourceNode.HasChildNodes)
            {
                OnlineResource = new OnlineResource();
                OnlineResource.FromXml(onlineResourceNode.FirstChild, mgr);
            }

            var telecommunicationsNode = node.FirstChild.SelectSingleNode("telecommunications");
            if (telecommunicationsNode != null && telecommunicationsNode.HasChildNodes)
            {
                Telecommunications = new Telecommunications();
                Telecommunications.FromXml(telecommunicationsNode.FirstChild, mgr);
            }

            return this;
        }
    }
}
