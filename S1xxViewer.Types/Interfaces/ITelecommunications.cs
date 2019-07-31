﻿namespace S1xxViewer.Types.Interfaces
{
    public interface ITelecommunications : IComplexType
    {
        string CategoryOfCommPref { get; set; }
        string ContactInstructions { get; set; }
        string TelcomCarrier { get; set; }
        string TelecommunicationsIdentifier { get; set; }
        string[] TelecommunicationsService { get; set; }
        IScheduleByDoW ScheduleByDoW { get; set; }
    }
}