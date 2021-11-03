using S1xxViewer.Types.Interfaces;

namespace S1xxViewer.Types.Features
{
    public abstract class DataQuality : MetaFeatureBase, IDataQuality
    {
        public IInformation[] Information { get; set; }
    }
}
