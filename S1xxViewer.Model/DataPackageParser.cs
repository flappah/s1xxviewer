using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types;
using S1xxViewer.Types.Interfaces;
using System;
using System.Linq;
using System.Xml;

namespace S1xxViewer.Model
{
    public class DataPackageParser : IDataPackageParser
    {       
        public IDataParser[] DataParsers { get; set; }

        /// <summary>
        /// Parses XMLDocument, determines S1xx type and starts the corresponding dataparser to determine content
        /// </summary>
        /// <param name="xmlDocument">XmlDocument</param>
        /// <returns>IS1xxDataPackage</returns>
        public IS1xxDataPackage Parse(XmlDocument xmlDocument)
        {
            string namespaceName = xmlDocument.DocumentElement?.Name;
            string s12xTypeString = namespaceName.Substring(0, namespaceName.IndexOf(":"));
            var s12xType = (S1xxTypes)Enum.Parse(typeof(S1xxTypes), s12xTypeString);

            var locatedDataParser =
                DataParsers.ToList().Find(tp => tp.GetType().Name.Contains(s12xType + "DataParser"));

            if (locatedDataParser != null)
            {
                return locatedDataParser.Parse(xmlDocument);
            }

            return new NullDataParser().Parse(xmlDocument);
        }
    }
}
