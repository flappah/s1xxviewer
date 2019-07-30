namespace S1xxViewer.Types.Interfaces
{
    public interface IQualityOfNonBathymetricData : IMetaFeature
    {
        double[] HorizontalDistanceUncertainty { get; set; }
        IHorizontalPositionalUncertainty HorizontalPositionalUncertainty { get; set; }
        double DirectionUncertainty { get; set; }
        ISourceIndication SourceIndication { get; set; }
        ISurveyDateRange SurveyDateRange { get; set; }

        string CategoryOfTemporalVariation { get; set; }
        string DataAssessment { get; set; }
        IInformation Information { get; set; }
    }
}