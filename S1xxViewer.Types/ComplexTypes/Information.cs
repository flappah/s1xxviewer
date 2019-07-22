using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Information : IInformation
    {
        public string FileDescription { get; set; }
        public string FileLocator { get; set; }
        public string Headline { get; set; }
        public string Language { get; set; }
        public InternationalString Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new Information
            {
                FileDescription = FileDescription,
                FileLocator = FileLocator,
                Headline = Headline,
                Language = Language,
                Text = new InternationalString(Text.Value, Text.Language)
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
            throw new System.NotImplementedException();
        }
    }
}
