namespace S1xxViewer.Types.Interfaces
{
    public interface IShipReport : IInformationFeature, IS122Feature
    {
        string CategoryOfShipReport { get; set; }
        bool ImoFormatForReporting { get; set; }
        INoticeTime NoticeTime { get; set; }
        ITextContent TextContent { get; set; }
    }
}