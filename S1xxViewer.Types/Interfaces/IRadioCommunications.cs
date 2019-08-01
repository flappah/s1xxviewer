using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Interfaces
{
    public interface IRadioCommunications : IComplexType
    {
        string CategoryOfCommPref { get; set; }
        string[] CommunicationChannel { get; set; }
        string ContactInstructions { get; set; }
        IFrequencyPair[] FrequencyPair { get; set; }
        ITmIntervalsByDoW[] TmIntervalsByDoW { get; set; }
    }
}