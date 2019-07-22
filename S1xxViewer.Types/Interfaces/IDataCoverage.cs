namespace S1xxViewer.Types.Interfaces
{
    public interface IDataCoverage : IMetaFeature
    {
        int MaximumDisplayScale { get; set; }
        int MinimumDisplayScale { get; set; }
    }
}