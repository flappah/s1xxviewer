using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface ITimeOfObservation : IComplexType
    {
        DateTime ObservationTime { get; set; }
        string TimeReference { get; set; }
    }
}