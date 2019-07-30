using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IPlaceOfRefuge : IGeoFeature
    {
        string[] Status { get; set; }
    }
}