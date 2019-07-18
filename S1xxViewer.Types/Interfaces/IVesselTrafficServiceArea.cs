namespace S1xxViewer.Types.Interfaces
{
    public interface IVesselTrafficServiceArea : IGeoFeature
    {
        string CategoryOfVesselTrafficService { get; set; }
        InternationalString FeatureName { get; set; }
        ILink[] Links { get; set; }
    }
}