namespace S1xxViewer.Types.Interfaces
{
    public interface IFeatureObjectIdentifier : IComplexType
    {
        string Agency { get; set; }
        string FeatureIdentificationNumber { get; set; }
        string FeatureIdentificationSubdivision { get; set; }
    }
}
