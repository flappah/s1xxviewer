namespace S1xxViewer.Types.Interfaces
{
    public interface INoticeTime : IComplexType
    {
        int NoticeTimeHours { get; set; }
        string NoticeTimeText { get; set; }
    }
}