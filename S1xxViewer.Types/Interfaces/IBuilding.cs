using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IBuilding : IGeoFeature
    {
        string[] Function { get; set; }
        string[] Status { get; set; }
    }
}