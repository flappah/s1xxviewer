using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Model.Interfaces
{
    public interface IDataPackageParser 
    {
        IDataParser[] DataParsers { get; set; }

        IS1xxDataPackage Parse(XmlDocument xmlDocument);
    }
}