namespace S1xxViewer.Types.Interfaces
{
    public interface IDataCoverage : IMetaFeature, IS122Feature
    {
        int MaximumDisplayScale { get; set; }
        int MinimumDisplayScale { get; set; }
    }
}