namespace S1xxViewer.Types.Interfaces
{
    public interface IRestrictedAreaRegulatory : IGeoFeature
    {
        string[] CategoryOfRestrictedArea { get; set; }
        string[] Restriction { get; set; }
    }
}