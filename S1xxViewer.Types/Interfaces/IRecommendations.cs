namespace S1xxViewer.Types.Interfaces
{
    public interface IRecommendations : IInformationFeature
    {
        string CategoryOfAuthority { get; set; }
        ITextContent TextContent { get; set; }
    }
}