using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Model.Interfaces
{
    public interface IDataParser
    {
        IS1xxDataPackage Parse(XmlDocument xmlDocument);
    }
}
