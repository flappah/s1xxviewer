using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace S1xxViewer.Model
{
    public class S111DataParser : DataParserBase
    {
        private readonly IGeometryBuilderFactory _geometryBuilderFactory;
        private readonly IFeatureFactory _featureFactory;

        /// <summary>
        ///     For autofac initialization
        /// </summary>
        public S111DataParser(IGeometryBuilderFactory geometryBuilderFactory, IFeatureFactory featureFactory)
        {
            _geometryBuilderFactory = geometryBuilderFactory;
            _featureFactory = featureFactory;
        }

        /// <summary>
        ///     Parses specified XMLDocument
        /// </summary>
        /// <param name="xmlDocument">XmlDocument</param>
        /// <returns>IS1xxDataPackage</returns>
        public override IS1xxDataPackage Parse(XmlDocument xmlDocument)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Parses HDF5 file specified by id
        /// </summary>
        /// <param name="hdf5FileId">HDF5 file-id</param>
        /// <returns>IS1xxDataPackage</returns>
        public override Task<IS1xxDataPackage> Parse(long hdf5FileId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Parses specified XMLDocument
        /// </summary>
        /// <param name="xmlDocument">XmlDocument</param>
        /// <returns>IS1xxDataPackage</returns>
        public override Task<IS1xxDataPackage> ParseAsync(XmlDocument xmlDocument)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Parses HDF5 file specified by id
        /// </summary>
        /// <param name="hdf5FileId">HDF5 file-id</param>
        /// <returns>IS1xxDataPackage</returns>
        public override Task<IS1xxDataPackage> ParseAsync(long hdf5FileId)
        {
            throw new NotImplementedException();
        }
    }
}
