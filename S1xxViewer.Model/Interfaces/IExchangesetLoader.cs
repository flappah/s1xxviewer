using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Model.Interfaces
{
    public interface IExchangesetLoader
    {
        XmlDocument Load(string fileName);
        (string, List<string>) Parse(XmlDocument xmlDocument);
    }
}