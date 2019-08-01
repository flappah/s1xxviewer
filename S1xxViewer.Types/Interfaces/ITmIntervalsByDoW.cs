using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface ITmIntervalsByDoW : IComplexType
    {
        int DayOfWeek { get; set; }
        bool DayOfWeekIsRange { get; set; }
        DateTime[] TimeOfDayEnd { get; set; }
        DateTime[] TimeOfDayStart { get; set; }
        string TimeReference { get; set; }
    }
}