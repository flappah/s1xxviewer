using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using S1xxViewer.Types.Interfaces;
using S1xxViewer.Types.ComplexTypes;

namespace S1xxViewer.Types.Features
{
    public class Applicability : InformationFeatureBase, IApplicability
    {
        public string CategoryOfVessel { get; set; }
        public IVesselsMeasurement VesselsMeasurement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IFeature DeepClone()
        {
            return new Applicability
            {
                CategoryOfVessel = CategoryOfVessel,
                VesselsMeasurement = VesselsMeasurement == null
                    ? new VesselsMeasurement()
                    : VesselsMeasurement.DeepClone() as IVesselsMeasurement
            };
        }

        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            throw new NotImplementedException();
        }
    }
}
