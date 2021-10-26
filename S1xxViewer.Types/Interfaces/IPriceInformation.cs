namespace S1xxViewer.Types.Interfaces
{
    public interface IPriceInformation : IInformationFeature
    {
        IPayment Payment { get; set; }
    }
}
