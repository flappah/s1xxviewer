namespace S1xxViewer.Types.Interfaces
{
    public interface IFrequencyPair : IComplexType
    {
        int FrequencyShoreStationReceives { get; set; }
        int FrequencyShoreStationTransmits { get; set; }
    }
}