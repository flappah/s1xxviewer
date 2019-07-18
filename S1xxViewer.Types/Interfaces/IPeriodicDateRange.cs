using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IPeriodicDateRange : IComplexType
    {
        string EndMonthDay { get; set; }
        string StartMonthDay { get; set; }
    }
}