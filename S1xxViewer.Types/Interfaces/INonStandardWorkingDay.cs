using System;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Interfaces
{
    public interface INonStandardWorkingDay : IInformationFeature
    {
        DateTime[] DateFixed { get; set; }
        string[] DateVariable { get; set; }
        IInformation[] Information { get; set; }
    }
}