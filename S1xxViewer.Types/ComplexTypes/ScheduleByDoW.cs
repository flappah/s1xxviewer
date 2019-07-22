using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.ComplexTypes
{
    public class ScheduleByDoW : IScheduleByDoW
    {
        public string CategoryOfSchedule { get; set; }
        public ITmIntervalsByDoW[] TmIntervalsByDoW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComplexType DeepClone()
        {
            return new ScheduleByDoW
            {
                CategoryOfSchedule = CategoryOfSchedule,
                TmIntervalsByDoW = TmIntervalsByDoW == null
                    ? new TmIntervalsByDoW[0]
                    : Array.ConvertAll(TmIntervalsByDoW, t => t.DeepClone() as ITmIntervalsByDoW)
            };
        }

        public IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
