using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class TmIntervalsByDoW : ITmIntervalsByDoW
    {
        public int DayOfWeek { get; set; }
        public bool DayOfWeekIsRange { get; set; }
        public string TimeReference { get; set; }
        public DateTime TimeOfDayStart { get; set; }
        public DateTime TimeOfDayEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new TmIntervalsByDoW
            {
                DayOfWeek = DayOfWeek,
                DayOfWeekIsRange = DayOfWeekIsRange,
                TimeReference = TimeReference,
                TimeOfDayEnd = TimeOfDayEnd,
                TimeOfDayStart = TimeOfDayStart
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
