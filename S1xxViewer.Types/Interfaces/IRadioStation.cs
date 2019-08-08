namespace S1xxViewer.Types.Interfaces
{
    public interface IRadioStation : IGeoFeature
    {
        string CallSign { get; set; }
        string CategoryOfRadioStation { get; set; }
        string EstimatedRangeOffTransmission { get; set; }
        IOrientation Orientation { get; set; }
        IRadioCommunications[] RadioCommunications { get; set; }
        string Status { get; set; }
    }
}