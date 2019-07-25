﻿namespace S1xxViewer.Types.Interfaces
{
    public interface IPrintInformation : IComplexType
    {
        string PrintAgency { get; set; }
        string PrintNation { get; set; }
        string PrintSize { get; set; }
        int PrintWeek { get; set; }
        string PrintYear { get; set; }
        string ReprintEdition { get; set; }
        string ReprintNation { get; set; }
    }
}