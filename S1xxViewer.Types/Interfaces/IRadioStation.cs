namespace S1xxViewer.Types.Interfaces
{
    public interface IRadioStation : IGeoFeature
    {
        string CallSign { get; set; }
        string CategoryOfRadioStation { get; set; }
        double EstimatedRangeOffTransmission { get; set; }
        IOrientation Orientation { get; set; }
        IRadioStationCommunicationDescription[] RadioStationCommunicationDescription { get; set; }
        string Status { get; set; }
    }
}