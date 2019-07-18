namespace S1xxViewer.Types.Interfaces
{
    public interface IQualityOfNonBathymetricData : IGeoFeature
    {
        string CategoryOfTemporalVariation { get; set; }
        string DataAssessment { get; set; }
        IFeatureObjectIdentifier FeatureObjectIdentifier { get; set; }
        ISourceIndication SourceIndication { get; set; }
    }
}