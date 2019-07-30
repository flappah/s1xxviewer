using System.Xml;

namespace S1xxViewer.Types.Interfaces
{
    public interface IMilitaryPracticeArea : IGeoFeature
    {
        string[] CategoryOfMilitaryPracticeArea { get; set; }
        string Nationality { get; set; }
        string[] Restriction { get; set; }
        string[] Status { get; set; }
    }
}