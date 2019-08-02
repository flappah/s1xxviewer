using System;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Interfaces
{
    public interface INonStandardWorkingDay : IInformationFeature
    {
        string[] DateFixed { get; set; }
        string[] DateVariable { get; set; }
        IInformation[] Information { get; set; }
    }
}