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
    public class Applicability : InformationFeatureBase, IApplicability, IS122Feature
    {
        public bool Ballast { get; set; }
        public string[] CategoryOfCargo { get; set; }
        public string[] CategoryOfDangerousOrHazardousCargo { get; set; }
        public string CategoryOfVessel { get; set; }
        public string CategoryOfVesselRegistry { get; set; }
        public string LogicalConnectives { get; set; }
        public int ThicknessOfIceCapability { get; set; }
        public IVesselsMeasurement[] VesselsMeasurements { get; set; }
        public string VesselPerformance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new Applicability
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
                Ballast = Ballast,
                CategoryOfCargo = CategoryOfCargo == null 
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfCargo, s => s),
                CategoryOfDangerousOrHazardousCargo = CategoryOfDangerousOrHazardousCargo == null
                    ? new string[0]
                    : Array.ConvertAll(CategoryOfDangerousOrHazardousCargo, s => s),
                CategoryOfVessel = CategoryOfVessel,
                CategoryOfVesselRegistry = CategoryOfVesselRegistry,
                LogicalConnectives = LogicalConnectives,
                ThicknessOfIceCapability = ThicknessOfIceCapability,
                VesselsMeasurements = VesselsMeasurements == null
                    ? new VesselsMeasurement[0]
                    : Array.ConvertAll(VesselsMeasurements, v => v.DeepClone() as IVesselsMeasurement),
                VesselPerformance = VesselPerformance,
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
