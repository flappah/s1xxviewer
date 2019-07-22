using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.ComplexTypes;
using S1xxViewer.Types.Links;
namespace S1xxViewer.Types.Features
{
    public class ServiceHours : InformationFeatureBase, IServiceHours
    {
        public IScheduleByDoW ScheduleByDoW { get; set; }
        public IInformation Information { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new ServiceHours
            {
                FeatureName = FeatureName == null
                    ? new[] { new InternationalString("") }
                    : Array.ConvertAll(FeatureName, fn => new InternationalString(fn.Value, fn.Language)),
                FixedDateRange = FixedDateRange == null
                    ? new DateRange()
                    : FixedDateRange.DeepClone() as IDateRange,
                Id = Id,
                PeriodicDateRange = PeriodicDateRange == null
                    ? new DateRange[0]
                    : Array.ConvertAll(PeriodicDateRange, p => p.DeepClone() as IDateRange),
                SourceIndication = SourceIndication == null
                    ? new SourceIndication[0]
                    : Array.ConvertAll(SourceIndication, s => s.DeepClone() as ISourceIndication),
                ScheduleByDoW = ScheduleByDoW == null 
                    ? new ScheduleByDoW()
                    : ScheduleByDoW.DeepClone() as IScheduleByDoW,
                Information = Information == null 
                    ? new Information ()
                    : Information.DeepClone() as IInformation,
                Links = Links == null
                    ? new[] { new Link() }
                    : Array.ConvertAll(Links, l => l.DeepClone() as ILink)
            };
        }

        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
