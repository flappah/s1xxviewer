namespace S1xxViewer.Types.Interfaces
{
    public interface IRadioStationCommunicationDescription : IComplexType
    {
        string[] CategoryOfMaritimeBroadcast { get; set; }
        string[] CommunicationChannel { get; set; }
        int SignalFrequency { get; set; }
        string TransmissionContent { get; set; }
    }
}