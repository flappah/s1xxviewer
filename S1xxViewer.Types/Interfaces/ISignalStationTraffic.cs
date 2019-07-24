namespace S1xxViewer.Types.Interfaces
{
    public interface ISignalStationTraffic : IGeoFeature
    {
        string[] CategoryOfSignalStationTraffic { get; set; }
        string[] CommunicationChannel { get; set; }
        string[] Status { get; set; }
    }
}