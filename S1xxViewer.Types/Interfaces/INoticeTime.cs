namespace S1xxViewer.Types.Interfaces
{
    public interface INoticeTime : IComplexType
    {
        string[] NoticeTimeHours { get; set; }
        string NoticeTimeText { get; set; }
        string Operation { get; set; }
    }
}