using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Interfaces
{
    public interface IServiceHours : IInformationFeature, IS122Feature
    {
        IScheduleByDoW ScheduleByDoW { get; set; }
        IInformation Information { get; set; }
    }
}