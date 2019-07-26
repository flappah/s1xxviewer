using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IInformationFeature : IFeature
    {
        IFeatureName[] FeatureName { get; set; }
        IDateRange FixedDateRange { get; set; }
        IDateRange[] PeriodicDateRange { get; set; }
        ISourceIndication[] SourceIndication { get; set; }
    }
}