namespace S1xxViewer.Types.Interfaces
{
    public interface IQualityOfNonBathymetricData : IMetaFeature, IS122Feature
    {
        string CategoryOfTemporalVariation { get; set; }
        string DataAssessment { get; set; }
        IFeatureObjectIdentifier FeatureObjectIdentifier { get; set; }
        ISourceIndication SourceIndication { get; set; }
    }
}