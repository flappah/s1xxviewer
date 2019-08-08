namespace S1xxViewer.Types.Interfaces
{
    public interface IQualityOfNonBathymetricData : IMetaFeature
    {
        string[] HorizontalDistanceUncertainty { get; set; }
        IHorizontalPositionalUncertainty HorizontalPositionalUncertainty { get; set; }
        string DirectionUncertainty { get; set; }
        ISourceIndication SourceIndication { get; set; }
        ISurveyDateRange SurveyDateRange { get; set; }

        string CategoryOfTemporalVariation { get; set; }
        string DataAssessment { get; set; }
        IInformation Information { get; set; }
    }
}