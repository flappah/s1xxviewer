using S1xxViewer.Types.Interfaces;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class PeriodicDateRange : IPeriodicDateRange
    {
        public string StartMonthDay { get; set; }
        public string EndMonthDay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IComplexType DeepClone()
        {
            return new PeriodicDateRange
            {
                StartMonthDay = StartMonthDay,
                EndMonthDay = EndMonthDay
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node != null && node.HasChildNodes)
            {
                var dateStart = node.SelectSingleNode("dateStart", mgr);
                if (dateStart != null && dateStart.HasChildNodes)
                {
                    StartMonthDay = dateStart.FirstChild.InnerText;
                }

                var dateEnd = node.SelectSingleNode("dateEnd", mgr);
                if (dateEnd != null && dateEnd.HasChildNodes)
                {
                    EndMonthDay = dateEnd.FirstChild.InnerText;
                }
            }

            return this;
        }
    }
}
