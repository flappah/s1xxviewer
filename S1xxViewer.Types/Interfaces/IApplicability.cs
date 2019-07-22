namespace S1xxViewer.Types.Interfaces
{
    public interface IApplicability : IInformationFeature
    {
        bool Ballast { get; set; }
        string[] CategoryOfCargo { get; set; }
        string[] CategoryOfDangerousOrHazardousCargo { get; set; }
        string CategoryOfVessel { get; set; }
        string CategoryOfVesselRegistry { get; set; }
        string LogicalConnectives { get; set; }
        int ThicknessOfIceCapability { get; set; }
        IVesselsMeasurement[] VesselsMeasurements { get; set; }
        string VesselPerformance { get; set; }
    }
}