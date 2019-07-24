using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Model.Interfaces
{
    public interface IS127DataParser : IDataParser
    {
        IS1xxDataPackage Parse(XmlDocument xmlDocument);
    }
}