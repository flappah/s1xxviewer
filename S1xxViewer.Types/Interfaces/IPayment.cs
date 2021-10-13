namespace S1xxViewer.Types.Interfaces
{
    public interface IPayment : IComplexType
    {
        string Currency { get; set; }
        string PriceNumber { get; set; }
    }
}
