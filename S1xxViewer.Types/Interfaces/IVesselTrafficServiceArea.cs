namespace S1xxViewer.Types.Interfaces
{
    public interface IVesselTrafficServiceArea : IGeoFeature, IS122Feature
    {
        string CategoryOfVesselTrafficService { get; set; }
        ILink[] Links { get; set; }
    }
}