using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Links;

namespace S1xxViewer.Types.Features
{
    public class ContactDetails : InformationFeatureBase, IContactDetails
    {
        public string CallName { get; set; }
        public string CallSign { get; set; }
        public string CategoryOfCommPref { get; set; }
        public string CommunicationChannel { get; set; }
        public string ContactInstructions { get; set; }
        public int MMsiCode { get; set; }

        public IContactAddress ContactAddress { get; set; }
        public IFrequencyPair FrequencyPair { get; set; }
        public IInformation Information { get; set; }
        public IOnlineResource OnlineResource { get; set; }
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
                CallName = CallName,
                CallSign = CallSign,
                CategoryOfCommPref= CategoryOfCommPref,
                CommunicationChannel = CommunicationChannel,
                ContactInstructions = ContactInstructions,
                MMsiCode  = MMsiCode,
                ContactAddress = ContactAddress == null 
                    ? new ContactAddress()
                    : ContactAddress.DeepClone() as IContactAddress,
                FrequencyPair = FrequencyPair == null   
                    ? new FrequencyPair() 
                    : FrequencyPair.DeepClone() as IFrequencyPair,
                Id = Id,
                Information = Information == null  
                    ? new Information()
                    : Information.DeepClone() as IInformation,
                OnlineResource = OnlineResource == null
                    ? new OnlineResource()
                    : OnlineResource.DeepClone() as IOnlineResource,
                RadioCommunications = RadioCommunications == null
                    ? new RadioCommunications[0]
                    : Array.ConvertAll(RadioCommunications, r => r.DeepClone() as IRadioCommunications),
                Telecommunications = Telecommunications == null
                    ? new Telecommunications[0]
                    : Array.ConvertAll(Telecommunications, t => t.DeepClone() as ITelecommunications),
                Links = Links == null
                    ? new[] { new Link() }
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
            return this;
        }
    }
}
