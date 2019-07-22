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
    public class NonStandardWorkingDay : InformationFeatureBase, INonStandardWorkingDay
    {
        public DateTime DateFixed { get; set; }
        public string DateVariable { get; set; }
        public IInformation Information { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new NonStandardWorkingDay
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
                DateFixed = DateFixed,
                DateVariable = DateVariable,
                Information = Information == null
                    ? new Information()
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
