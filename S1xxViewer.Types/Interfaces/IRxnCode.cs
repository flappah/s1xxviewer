namespace S1xxViewer.Types.Interfaces
{
    public interface IRxnCode : IComplexType
    {
        string ActionOrActivity { get; set; }
        string CategoryOfRxn { get; set; }
    }
}