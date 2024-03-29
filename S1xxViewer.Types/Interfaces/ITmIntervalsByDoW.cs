﻿using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface ITmIntervalsByDoW : IComplexType
    {
        string[] DayOfWeek { get; set; }
        string DayOfWeekIsRanges { get; set; }
        string[] TimeOfDayEnd { get; set; }
        string[] TimeOfDayStart { get; set; }
        string TimeReference { get; set; }
    }
}