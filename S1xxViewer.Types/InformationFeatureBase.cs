using S1xxViewer.Types.Interfaces;
using System.Xml;
using System;

namespace S1xxViewer.Types
{
    public abstract class InformationFeatureBase : IInformationFeature
    {
        public InternationalString[] FeatureName { get; set; }
        public IDateRange FixedDateRange { get; set; }
        public string Id { get; set; }
        public IDateRange PeriodicDateRange { get; set; }
        public ISourceIndication SourceIndication { get; set; }

        // linkages
        public ILink[] Links { get; set; }

        public abstract IFeature DeepClone();
        public abstract IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
