namespace S1xxViewer.Types.Interfaces
{
    public interface IRecommendations : IInformationFeature, IS122Feature
    {
        string CategoryOfAuthority { get; set; }
        string[] Graphic { get; set; }
        IRxnCode[] RxnCode { get; set; }
        ITextContent[] TextContent { get; set; }
    }
}