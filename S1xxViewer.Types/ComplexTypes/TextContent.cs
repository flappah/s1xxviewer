using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TextContent : ITextContent
    {
        public string CategoryOfText { get; set; }
        public IInformation Information { get; set; }

        public ISourceIndication SourceIndication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new TextContent
            {
                CategoryOfText = CategoryOfText,
                Information = Information == null 
                    ? new Information()
                    : Information.DeepClone() as IInformation,
                SourceIndication = SourceIndication == null 
                    ? new SourceIndication()
                    : SourceIndication.DeepClone() as ISourceIndication
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public virtual IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            return this;
        }
    }
}
