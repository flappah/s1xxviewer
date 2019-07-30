using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Model
{
    public abstract class DataParserBase : IDataParser
    {
        /// <summary>
        /// Parses specified XMLDocument
        /// </summary>
        /// <param name="xmlDocument">XmlDocument</param>
        /// <returns>IS1xxDataPackage</returns>
        public abstract IS1xxDataPackage Parse(XmlDocument xmlDocument);

        /// <summary>
        /// Resolves specified links by looking in the specified lists for the requested ID's
        /// </summary>
        /// <param name="links">ILink[]</param>
        /// <param name="informationFeatures">List<IInformationFeature></param>
        /// <param name="metaFeatures">List<IMetaFeature></param>
        /// <param name="geoFeatures">List<IGeoFeature></param>
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
                    link.LinkedFeature = informationFeatures[foundInfoFeatureIndex].DeepClone();
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
                        link.LinkedFeature = metaFeatures[foundMetaFeatureIndex].DeepClone();
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
                            link.LinkedFeature = geoFeatures[foundGeoFeatureIndex].DeepClone();
                        }
                    }
                }
            }
        }
    }
}
