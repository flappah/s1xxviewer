namespace S1xxViewer.Types.Interfaces
{
    public interface IFeatureObjectIdentifier : IComplexType
    {
        string Agency { get; set; }
        int FeatureIdentificationNumber { get; set; }
        int FeatureIdentificationSubdivision { get; set; }
    }
}
