namespace S1xxViewer.Types.Interfaces
{
    public interface INauticalInformation : IInformationFeature
    {
        string CategoryOfAuthority { get; set; }
        IPeriodicDateRange PeriodicDataRange { get; set; }
        ITextContent TextContent { get; set; }

    }
}