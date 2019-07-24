namespace S1xxViewer.Types.Interfaces
{
    public interface ISignalStationWarning : IGeoFeature
    {
        string[] CategoryOfSignalStationWarning { get; set; }
        string[] CommunicationChannel { get; set; }
        string[] Status { get; set; }
    }
}