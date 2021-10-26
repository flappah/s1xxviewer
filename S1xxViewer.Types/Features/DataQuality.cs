using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public abstract class DataQuality : MetaFeatureBase, IDataQuality
    {
        public IInformation[] Information { get; set; }
    }
}
