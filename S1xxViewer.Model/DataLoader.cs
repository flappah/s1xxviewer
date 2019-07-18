using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Xml;
using S1xxViewer.Base.Interfaces;

namespace S1xxViewer.Model
{
    public class DataLoader
    {
        private readonly IInjectableXmlDocument _injectableXmlDocument;

        /// <summary>
        /// For autofac initialization
        /// </summary>
        public DataLoader(IInjectableXmlDocument injectableXmlDocument)
        {
            _injectableXmlDocument = injectableXmlDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual XmlDocument Load(string fileName)
        {
            var xmlDoc = _injectableXmlDocument.Load(fileName);
            return xmlDoc;
        }
    }
}
