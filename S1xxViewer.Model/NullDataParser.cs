using S1xxViewer.Model.Interface;
using S1xxViewer.Types;
using S1xxViewer.Types.Interfaces;
using System.Threading.Tasks;
using System.Xml;

namespace S1xxViewer.Model
{
    public class NullDataParser : DataParserBase, INullDataParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public override IS1xxDataPackage Parse(XmlDocument xmlDocument)
        {
            return new S1xxDataPackage
            {
                Type = S1xxTypes.Null,
                RawData = xmlDocument,
                GeoFeatures = new IGeoFeature[0],
                MetaFeatures = new IMetaFeature [0],
                InformationFeatures = new IInformationFeature[0]
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public async override Task<IS1xxDataPackage> ParseAsync(XmlDocument xmlDocument)
        {
            return new S1xxDataPackage
            {
                Type = S1xxTypes.Null,
                RawData = xmlDocument,
                GeoFeatures = new IGeoFeature[0],
                MetaFeatures = new IMetaFeature[0],
                InformationFeatures = new IInformationFeature[0]
            };
        }
    }
}
