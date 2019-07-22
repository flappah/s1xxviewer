﻿namespace S1xxViewer.Types.Interfaces
{
    public interface IContactDetails : IInformationFeature, IS122Feature
    {
        string CallName { get; set; }
        string CallSign { get; set; }
        string CategoryOfCommPref { get; set; }
        string CommunicationChannel { get; set; }
        string ContactInstructions { get; set; }
        int MMsiCode { get; set; }

        IContactAddress ContactAddress { get; set; }
        IFrequencyPair FrequencyPair { get; set; }
        IInformation Information { get; set; }
        IOnlineResource OnlineResource { get; set; }
        IRadioCommunications[] RadioCommunications { get; set; }
        ITelecommunications[] Telecommunications { get; set; }

    }
}