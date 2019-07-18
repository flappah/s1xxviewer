using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Model
{
    public abstract class DataParserBase : IDataParser
    {
        public abstract IS1xxDataPackage Parse(XmlDocument xmlDocument);
    }
}
