using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Interfaces
{
    public interface IScheduleByDoW : IComplexType
    {
        string CategoryOfSchedule { get; set; }
        ITmIntervalsByDoW[] TmIntervalsByDoW { get; set; }
    }
}