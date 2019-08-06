using S1xxViewer.Types.Interfaces;
using System.Threading.Tasks;
using System.Xml;

namespace S1xxViewer.Model.Interfaces
{
    public interface IDataPackageParser 
    {
        IDataParser[] DataParsers { get; set; }

        Task<IS1xxDataPackage> ParseAsync(XmlDocument xmlDocument);
        IS1xxDataPackage Parse(XmlDocument xmlDocument);
    }
}