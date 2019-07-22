using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface ISurveyDateRange : IComplexType
    {
        DateTime DateEnd { get; set; }
        DateTime DateStart { get; set; }
    }
}