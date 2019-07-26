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
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Windows;
using System.Drawing;
using S1xxViewer.Model.Interface;

namespace S1xxViewerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IContainer _container;
        private FeatureCollectionLayer _collectionLayer;

        public MainWindow()
        {
            InitializeComponent();
            _container = AutofacInitializer.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void AppExit_Click(object obj, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPackage"></param>
        private async void CreateFeatureCollection (IS1xxDataPackage dataPackage)
        {
            string theJSON_String =
             @"{
                    ""labelExpressionInfo"":{""expression"":""return $feature.FeatureName""},
                    ""labelPlacement"":""esriServerPolygonPlacementAlwaysHorizontal"",
                    ""symbol"":
                        { 
                            ""angle"":0,
                            ""backgroundColor"":[0,0,0,0],
                            ""borderLineColor"":[0,0,0,0],
                            ""borderLineSize"":0,
                            ""color"":[0,0,255,255],
                            ""font"":
                                {
                                    ""decoration"":""none"",
                                    ""size"":8,
                                    ""style"":""normal"",
                                    ""weight"":""normal""
                                },
                            ""haloColor"":[255,255,255,255],
                            ""haloSize"":0.1,
                            ""horizontalAlignment"":""center"",
                            ""kerning"":false,
                            ""type"":""esriTS"",
                            ""verticalAlignment"":""middle"",
                            ""xoffset"":0,
                            ""yoffset"":0
                        }
               }";

            // Create a label definition from the JSON string. 
            LabelDefinition highwaysLabelDefinition = LabelDefinition.FromJson(theJSON_String);
            
            List<Field> polyFields = new List<Field>();
            Field idField = new Field(FieldType.Text, "FeatureId", "Id", 50);
            Field nameField = new Field(FieldType.Text, "FeatureName", "Name", 255);
            polyFields.Add(idField);
            polyFields.Add(nameField);

            List<Field> pointFields = new List<Field>();
            pointFields.Add(idField);
            pointFields.Add(nameField);

            List<Field> lineFields = new List<Field>();
            lineFields.Add(idField);
            lineFields.Add(nameField);

            FeatureCollectionTable polysTable = new FeatureCollectionTable(polyFields, GeometryType.Polygon, SpatialReferences.Wgs84);
            polysTable.Renderer = CreateRenderer(GeometryType.Polygon);

            FeatureCollectionTable linesTable = new FeatureCollectionTable(lineFields, GeometryType.Polyline, SpatialReferences.Wgs84);
            linesTable.Renderer = CreateRenderer(GeometryType.Polyline);

            FeatureCollectionTable pointTable = new FeatureCollectionTable(pointFields, GeometryType.Point, SpatialReferences.Wgs84);
            pointTable.Renderer = CreateRenderer(GeometryType.Point);

            foreach (IFeature feature in dataPackage.GeoFeatures)
            {
                if (feature is IGeoFeature)
                {
                    if (((IGeoFeature)feature).Geometry is Esri.ArcGISRuntime.Geometry.MapPoint)
                    {
                        Feature pointFeature = pointTable.CreateFeature();
                        pointFeature.SetAttributeValue(idField, feature.Id);
                        pointFeature.SetAttributeValue(nameField, ((IGeoFeature)feature).FeatureName?.First()?.Name);
                        pointFeature.Geometry = ((IGeoFeature)feature).Geometry;

                        await pointTable.AddFeatureAsync(pointFeature);
                    }
                    else if (((IGeoFeature)feature).Geometry is Esri.ArcGISRuntime.Geometry.Polyline)
                    {
                        Feature lineFeature = linesTable.CreateFeature();
                        lineFeature.SetAttributeValue(idField, feature.Id);
                        lineFeature.SetAttributeValue(nameField, ((IGeoFeature)feature).FeatureName?.First()?.Name);
                        lineFeature.Geometry = ((IGeoFeature)feature).Geometry;

                        await linesTable.AddFeatureAsync(lineFeature);
                    }
                    else
                    { 
                        Feature polyFeature = polysTable.CreateFeature();
                        polyFeature.SetAttributeValue(idField, feature.Id);
                        polyFeature.SetAttributeValue(nameField, ((IGeoFeature)feature).FeatureName?.First()?.Name);
                        polyFeature.Geometry = ((IGeoFeature)feature).Geometry;

                        await polysTable.AddFeatureAsync(polyFeature);
                    }
                }
            }

            FeatureCollection featuresCollection = new FeatureCollection();
            if (pointTable.Count() > 0)
            {
                featuresCollection.Tables.Add(pointTable);
            }
            if (polysTable.Count() > 0)
            {
                featuresCollection.Tables.Add(polysTable);
            }
            if (linesTable.Count() > 0)
            {
                featuresCollection.Tables.Add(linesTable);
            }
            _collectionLayer = new FeatureCollectionLayer(featuresCollection);

            // When the layer loads, zoom the map view to the extent of the feature collection
            _collectionLayer.Loaded += (s, e) => Dispatcher.Invoke(() => 
            {
                try
                {
                    MyMapView.SetViewpointAsync(new Viewpoint(_collectionLayer.FullExtent));

                    foreach(FeatureLayer layer in _collectionLayer.Layers)
                    {
                        layer.LabelDefinitions.Add(highwaysLabelDefinition);
                        layer.LabelsEnabled = true;
                    }
                }
                catch(Exception) { }
            });

            // Add the layer to the Map's Operational Layers collection
            MyMapView.Map.OperationalLayers.Add(_collectionLayer);
            MyMapView.GeoViewTapped += OnMapViewTapped;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnMapViewTapped(object sender, GeoViewInputEventArgs e)
        {
            try
            {
                // get the tap location in screen units
                System.Windows.Point tapScreenPoint = e.Position;

                // Specify identify properties.
                double pixelTolerance = 20.0;
                bool returnPopupsOnly = false;
                int maxResults = 5;

                // Identify a  group layer using MapView, passing in the layer, the tap point, tolerance, types to return, and max results.
                IdentifyLayerResult groupLayerResult = 
                    await MyMapView.IdentifyLayerAsync(_collectionLayer, tapScreenPoint, pixelTolerance, returnPopupsOnly, maxResults);

                // Iterate each set of child layer results.
                foreach (IdentifyLayerResult subLayerResult in groupLayerResult.SublayerResults)
                {
                    // Display the name of the sublayer.
                    Console.WriteLine("\nResults for child layer: " + subLayerResult.LayerContent.Name);
                    // Iterate each geoelement in the child layer result set.
                    foreach (GeoElement idElement in subLayerResult.GeoElements)
                    {
                        // cast the result GeoElement to Feature
                        Feature idFeature = idElement as Feature;
                        // select this feature in the feature layer
                        foreach (FeatureLayer layer in _collectionLayer.Layers)
                        {
                            layer.ClearSelection();
                            layer.SelectFeature(idFeature);
                        }

                        // Loop through and display the attribute values.
                        Console.WriteLine("  ** Attributes **");
                        foreach (KeyValuePair<string, object> attribute in idElement.Attributes)
                        {
                            Console.WriteLine("    - " + attribute.Key + " = " + attribute.Value.ToString());
                        }
                    }
                }
                        //// Define the selection tolerance (half the marker symbol size so that any click on the symbol will select the feature)
                        //double tolerance = 10;

                        //// Convert the tolerance to map units
                        //double mapTolerance = tolerance * MyMapView.UnitsPerPixel;

                        //// Get the tapped point
                        //MapPoint geometry = e.Location;

                        //// Normalize the geometry if wrap-around is enabled
                        ////    This is necessary because of how wrapped-around map coordinates are handled by Runtime
                        ////    Without this step, querying may fail because wrapped-around coordinates are out of bounds.
                        //if (MyMapView.IsWrapAroundEnabled) { geometry = GeometryEngine.NormalizeCentralMeridian(geometry) as MapPoint; }

                        //// Define the envelope around the tap location for selecting features
                        //var selectionEnvelope = new Envelope(geometry.X - mapTolerance, geometry.Y - mapTolerance, geometry.X + mapTolerance,
                        //    geometry.Y + mapTolerance, MyMapView.Map.SpatialReference);

                        //// Define the query parameters for selecting features
                        //var queryParams = new QueryParameters();

                        //// Set the geometry to selection envelope for selection by geometry
                        //queryParams.Geometry = selectionEnvelope;

                        //// Select the features based on query parameters defined above
                        //foreach (FeatureLayer layer in _collectionLayer.Layers)
                        //{
                        //    var result = await layer.SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
                        //}
                    }
            catch (Exception ex)
            {
                MessageBox.Show("Sample error", ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rendererType"></param>
        /// <returns></returns>
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
                    sym = new SimpleLineSymbol(SimpleLineSymbolStyle.Dash, System.Drawing.Color.DarkGray, 3);

                    break;
                case GeometryType.Polygon:
                    // Create a fill symbol
                    var lineSym = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.DarkGray, 1);
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
