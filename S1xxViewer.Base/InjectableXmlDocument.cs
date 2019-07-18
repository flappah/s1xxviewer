using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Base.Interfaces;

namespace S1xxViewer.Base
{
    public class InjectableXmlDocument : IInjectableXmlDocument
    {
        private readonly XmlDocument _xmlDocument;

        public InjectableXmlDocument()
        {
            _xmlDocument = new XmlDocument();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public XmlDocument Load(string fileName)
        {
            _xmlDocument.Load(fileName);
            return _xmlDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public XmlDocument LoadXml(string xml)
        {
            _xmlDocument.LoadXml(xml);
            return _xmlDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XmlNodeList SelectNodes(string xpath)
        {
            return _xmlDocument.SelectNodes(xpath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XmlNode SelectSingleNode(string xpath)
        {
            return _xmlDocument.SelectSingleNode(xpath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public XmlElement GetElementById(string elementId)
        {
            return _xmlDocument.GetElementById(elementId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNodeList GetElementsByTagName(string name)
        {
            return _xmlDocument.GetElementsByTagName(name);
        }
    }
}
