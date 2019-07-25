namespace S1xxViewer.Types.Interfaces
{
    public interface IPrice : IComplexType
    {
        string Currency { get; set; }
        int PriceNumber { get; set; }
    }
}