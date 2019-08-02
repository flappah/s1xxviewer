namespace S1xxViewer.Types.Interfaces
{
    public interface ISpatialQuality : IInformationFeature
    {
        string CategoryOfTemporalVariation { get; set; }
        IHorizontalPositionalUncertainty HorizontalPositionalUncertainty { get; set; }
        string QualityOfHorizontalMeasurement { get; set; }
    }
}