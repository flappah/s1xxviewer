using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Interfaces
{
    public interface IInformation : IComplexType
    {
        string Headline { get; set; }
        InternationalString Text { get; set; }

    }
}