using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class RxnCode : IRxnCode
    {
        public string CategoryOfRxn { get; set; }
        public string ActionOrActivity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new RxnCode
            {
                CategoryOfRxn = CategoryOfRxn,
                ActionOrActivity = ActionOrActivity
            };
        }

        public virtual IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new System.NotImplementedException();
        }
    }
}
