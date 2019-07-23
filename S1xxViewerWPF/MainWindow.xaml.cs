using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using S1xxViewer.Model.Interfaces;
using System.Xml;
using Autofac;
using S1xxViewer.Types.Interfaces;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Mapping;
using System.Drawing;

namespace S1xxViewerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IContainer _container;

        public MainWindow()
        {
            InitializeComponent();
            _container = AutofacInitializer.Initialize();
        }

        public void AppExit_Click(object obj, EventArgs e)
        {
            this.Close();
        }

        private void AppOpen_Click(object sender, RoutedEventArgs e)
        {
            MyMapView.Map.OperationalLayers.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML/GML files (*.xml;*.gml)|*.xml;*.gml|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);

                var dataParser = _container.Resolve<IDataPackageParser>();
                IS1xxDataPackage dataPackage = dataParser.Parse(xmlDoc);

                CreateFeatureCollection(dataPackage);
            }
        }

        private async void CreateFeatureCollection (IS1xxDataPackage dataPackage)
        {
            List<Field> polyFields = new List<Field>();
            Field idField1 = new Field(FieldType.Text, "FeatureId", "Id", 50);
            polyFields.Add(idField1);

            List<Field> pointFields = new List<Field>();
            Field idField2 = new Field(FieldType.Text, "FeatureId", "Id", 50);
            pointFields.Add(idField2);

            FeatureCollectionTable polysTable = new FeatureCollectionTable(polyFields, GeometryType.Polygon, SpatialReferences.Wgs84);
            polysTable.Renderer = CreateRenderer(GeometryType.Polygon);

            FeatureCollectionTable pointTable = new FeatureCollectionTable(pointFields, GeometryType.Point, SpatialReferences.Wgs84);
            pointTable.Renderer = CreateRenderer(GeometryType.Point);

            FeatureCollection featuresCollection = new FeatureCollection();
            foreach (IFeature feature in dataPackage.GeoFeatures)
            {
                if (feature is IGeoFeature)
                {
                    if (((IGeoFeature)feature).Geometry is MapPoint)
                    {
                        Feature pointFeature = pointTable.CreateFeature();
                        pointFeature.SetAttributeValue(idField2, feature.Id);
                        pointFeature.Geometry = ((IGeoFeature)feature).Geometry;

                        await pointTable.AddFeatureAsync(pointFeature);
                    }
                    else
                    {
                        Feature polyFeature = polysTable.CreateFeature();
                        polyFeature.SetAttributeValue(idField1, feature.Id);
                        polyFeature.Geometry = ((IGeoFeature)feature).Geometry;

                        await polysTable.AddFeatureAsync(polyFeature);
                    }
                }
            }

            featuresCollection.Tables.Add(pointTable);
            featuresCollection.Tables.Add(polysTable);
            FeatureCollectionLayer collectionLayer = new FeatureCollectionLayer(featuresCollection);

            // When the layer loads, zoom the map view to the extent of the feature collection
            collectionLayer.Loaded += (s, e) => Dispatcher.Invoke(() => { MyMapView.SetViewpointAsync(new Viewpoint(collectionLayer.FullExtent)); });

            // Add the layer to the Map's Operational Layers collection
            MyMapView.Map.OperationalLayers.Add(collectionLayer);
        }

        private Renderer CreateRenderer(GeometryType rendererType)
        {
            // Return a simple renderer to match the geometry type provided
            Symbol sym = null;
            
            switch (rendererType)
            {
                case GeometryType.Point:
                case GeometryType.Multipoint:
                    // Create a marker symbol
                    sym = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.X, System.Drawing.Color.Red, 10);

                    break;
                case GeometryType.Polyline:
                    // Create a line symbol
                    sym = new SimpleLineSymbol(SimpleLineSymbolStyle.Dash, System.Drawing.Color.Gray, 3);

                    break;
                case GeometryType.Polygon:
                    // Create a fill symbol
                    var lineSym = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.DarkGray, 2);
                    sym = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.FromArgb(50, System.Drawing.Color.LightGray), lineSym);
                   
                    break;
                default:
                    break;
            }

            // Return a new renderer that uses the symbol created above
            return new SimpleRenderer(sym);
        }
        // Map initialization logic is contained in MapViewModel.cs
    }
}
