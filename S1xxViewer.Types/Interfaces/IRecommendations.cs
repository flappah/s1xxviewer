namespace S1xxViewer.Types.Interfaces
{
    public interface IRecommendations : IInformationFeature
    {
        string CategoryOfAuthority { get; set; }
        IGraphic[] Graphic { get; set; }
        IRxnCode[] RxnCode { get; set; }
        ITextContent[] TextContent { get; set; }
    }
}