namespace S1xxViewer.Types.Interfaces
{
    public interface IRestrictedAreaNavigational : IGeoFeature
    {
        string CategoryOfRestrictedArea { get; set; }
        InternationalString[] FeatureName { get; set; }
        string[] Restriction { get; set; }
    }
}