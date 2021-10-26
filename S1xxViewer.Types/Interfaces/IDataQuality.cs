namespace S1xxViewer.Types.Interfaces
{
    public interface IDataQuality : IMetaFeature
    {
        IInformation[] Information { get; set; }
    }
}
