namespace S1xxViewer.Types.Interfaces
{
    public interface IDataCoverage : IS122Feature, IGeoFeature
    {
        int MaximumDisplayScale { get; set; }
        int MinimumDisplayScale { get; set; }
    }
}