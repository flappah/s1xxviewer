namespace S1xxViewer.Types.Interfaces
{
    public interface ISurveyDateRange : IComplexType
    {
        string DateEnd { get; set; }
        string DateStart { get; set; }
    }
}