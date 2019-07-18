using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Interfaces
{
    public interface IMarineProtectedArea : IGeoFeature, IS122Feature
    {
        string CategoryOfMarineProtectedArea { get; set; }
        string[] CategoryOfRestrictedArea { get; set; }
        InternationalString FeatureName { get; set; }
        string Jurisdiction { get; set; }
        IPeriodicDateRange PeriodicDateRange { get; set; }
        string[] Restriction { get; set; }
        ISourceIndication SourceIndication { get; set; }
        string Status { get; set; }

        ILink[] Links { get; set; }
    }
}