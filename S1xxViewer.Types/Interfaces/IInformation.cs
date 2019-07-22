using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Interfaces
{
    public interface IInformation : IComplexType
    {
        string FileDescription { get; set; }
        string FileLocator { get; set; }
        string Headline { get; set; }
        string Language { get; set; }
        InternationalString Text { get; set; }
    }
}