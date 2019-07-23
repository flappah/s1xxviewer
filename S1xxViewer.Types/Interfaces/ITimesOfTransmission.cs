using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface ITimesOfTransmission : IComplexType
    {
        int MinutePastEvenHours { get; set; }
        int MinutePastEveryHours { get; set; }
        int MinutePastOddHours { get; set; }
        string TimeReference { get; set; }
        DateTime[] TransmissionTime { get; set; }
    }
}