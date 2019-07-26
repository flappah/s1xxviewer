using S1xxViewer.Types.Interfaces;
using System;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class SurveyDateRange : ComplexTypeBase, ISurveyDateRange
    {
        public DateTime DateEnd { get; set; }
        public DateTime DateStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new SurveyDateRange
            {
                DateEnd = DateEnd,
                DateStart = DateStart
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var dateEndNode = node.SelectSingleNode("dateEnd", mgr);
            if (dateEndNode != null)
            {
                var dateEndString = dateEndNode.InnerText;
                DateTime dateEnd;
                if (!DateTime.TryParse(dateEndString, out dateEnd))
                {
                    dateEnd = DateTime.MinValue;
                }
                DateEnd = dateEnd;
            }

            var dateStartNode = node.SelectSingleNode("dateStart", mgr);
            if (dateStartNode != null)
            {
                var dateStartString = dateStartNode.InnerText;
                DateTime dateStart;
                if (!DateTime.TryParse(dateStartString, out dateStart))
                {
                    dateStart = DateTime.MinValue;
                }
                DateStart = dateStart;
            }

            return this;
        }
    }
}
