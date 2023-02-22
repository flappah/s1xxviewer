using S1xxViewer.Base.Interfaces;
using S1xxViewer.Model.Interfaces;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Model
{
    public class ExchangesetLoader : IExchangesetLoader
    {
        private readonly IInjectableXmlDocument _injectableXmlDocument;

        /// <summary>
        ///     Publicly accessible XmlDocument that contains the exchangeset
        /// </summary>
        public XmlDocument ExchangesetXml { get; set; }

        /// <summary>
        ///     For autofac initialization
        /// </summary>
        public ExchangesetLoader(IInjectableXmlDocument injectableXmlDocument)
        {
            _injectableXmlDocument = injectableXmlDocument;
        }

        /// <summary>
        ///     Loads the specified filename and returns an initialized XmlDocument
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual XmlDocument Load(string fileName)
        {
            ExchangesetXml = _injectableXmlDocument.Load(fileName);
            return ExchangesetXml;
        }

        /// <summary>
        ///     Parses the specified XmlDocument and retrieves the standard and filename the exchangeset refers to
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public virtual (string, List<string>) Parse(XmlDocument xmlDocument)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("S100XC", "http://www.iho.int/s100/xc");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");

            var productSpecificationNameNode = xmlDocument.DocumentElement.SelectSingleNode("S100XC:productSpecification/S100XC:name", nsmgr);
            string productStandard = string.Empty;
            if (productSpecificationNameNode != null)
            {
                productStandard = productSpecificationNameNode.InnerText;
            }

            var datasetDicoveryNodes = xmlDocument.DocumentElement.SelectNodes("S100XC:datasetDiscoveryMetadata", nsmgr);
            var productFileNames = new List<string>();
            if (datasetDicoveryNodes != null && datasetDicoveryNodes.Count > 0)
            {
                foreach(XmlNode node in datasetDicoveryNodes)
                {
                    XmlNode metaDataNode = null;
                    if (node.FirstChild.Name.Contains("DatasetDiscoveryMetadata"))
                    {
                        metaDataNode = node.FirstChild;
                    }
                    else
                    {
                        metaDataNode = node;
                    }
                    
                    var fileName = string.Empty;
                    var fileNameNode = metaDataNode.SelectSingleNode("S100XC:fileName", nsmgr);
                    if (fileNameNode != null)
                    {
                        fileName = fileNameNode.InnerText;
                    }

                    var filePathNode = metaDataNode.SelectSingleNode("S100XC:filePath", nsmgr);
                    if (filePathNode != null) // V4 compliancy
                    {
                        productFileNames.Add($@"{filePathNode.InnerText.Replace("/", @"\")}\{fileName}");
                    }
                    else // V5 compliancy assumes static folder called DATASET_FILES and a known ProducerCode
                    {
                        var producerCodeNode = metaDataNode.SelectSingleNode("S100XC:producerCode", nsmgr);
                        if (producerCodeNode != null)
                        {
                            productFileNames.Add($@"DATASET_FILES\{producerCodeNode.InnerText.PadRight(4, char.Parse("0"))}\{fileName}");
                        }
                        else
                        {
                            productFileNames.Add(fileName);
                        }
                    }
                }

                return (productStandard, productFileNames);
            }

            return (string.Empty, new List<string>());
        }

    }
}
