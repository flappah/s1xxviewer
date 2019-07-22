namespace S1xxViewer.Types.Interfaces
{
    public interface IDateRange : IComplexType
    {
        string EndMonthDay { get; set; }
        string StartMonthDay { get; set; }
    }
}