using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Interfaces
{
    public interface IRestrictions : IInformationFeature, IS122Feature
    {
        string CategoryOfAuthority { get; set; }
        string[] Graphic { get; set; }
        IRxnCode[] RxnCode { get; set; }
        ITextContent[] TextContent { get; set; }
    }
}