namespace S1xxViewer.Types.Interfaces
{
    public interface IApplicability : IInformationFeature
    {
        string CategoryOfVessel { get; set; }
        IVesselsMeasurement VesselsMeasurement { get; set; }
    }
}