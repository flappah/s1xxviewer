using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Model
{
    public abstract class DataParserBase : IDataParser
    {
        public abstract IS1xxDataPackage Parse(XmlDocument xmlDocument);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="links"></param>
        /// <param name="informationFeatures"></param>
        /// <param name="metaFeatures"></param>
        /// <param name="geoFeatures"></param>
        protected void ResolveLinks(ILink[] links, List<IInformationFeature> informationFeatures, List<IMetaFeature> metaFeatures, List<IGeoFeature> geoFeatures)
        {
            foreach (ILink link in links)
            {
                int foundInfoFeatureIndex =
                    informationFeatures.FindIndex(ftr =>
                        !String.IsNullOrEmpty(ftr.Id) &&
                        ftr.Id.Contains(link.Href.Replace("#", "")));

                if (foundInfoFeatureIndex != -1)
                {
                    link.Offset = $"I_{foundInfoFeatureIndex}";
                }
                else
                {
                    int foundMetaFeatureIndex =
                        metaFeatures.FindIndex(ftr =>
                            !String.IsNullOrEmpty(ftr.Id) &&
                            ftr.Id.Contains(link.Href.Replace("#", "")));

                    if (foundMetaFeatureIndex != -1)
                    {
                        link.Offset = $"M_{foundMetaFeatureIndex}";
                    }
                    else
                    {
                        int foundGeoFeatureIndex =
                            geoFeatures.FindIndex(ftr =>
                                !String.IsNullOrEmpty(ftr.Id) &&
                                ftr.Id.Contains(link.Href.Replace("#", "")));

                        if (foundGeoFeatureIndex != -1)
                        {
                            link.Offset = $"G_{foundGeoFeatureIndex}";
                        }
                    }
                }
            }
        }
    }
}
