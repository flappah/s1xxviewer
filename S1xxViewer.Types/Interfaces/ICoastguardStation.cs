using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface ICoastguardStation : IGeoFeature
    {
        string[] CommunicationsChannel { get; set; }
        string IsMRCC { get; set; }
        string[] Status { get; set; }
    }
}