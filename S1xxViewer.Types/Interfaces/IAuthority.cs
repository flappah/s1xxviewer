namespace S1xxViewer.Types.Interfaces
{
    public interface IAuthority : IInformationFeature
    {
        string CategoryOfAuthority { get; set; }
        IContactDetails[] ContactDetails { get; set; }
        InternationalString[] FeatureName { get; set; }
    }
}