namespace S1xxViewer.Types.Interfaces
{
    public interface IShipReport : IInformationFeature
    {
        string CategoryOfShipReport { get; set; }
        bool ImoFormatForReporting { get; set; }
        INoticeTime NoticeTime { get; set; }
        ITextContent TextContent { get; set; }
    }
}