using Autofac;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Hydrography;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI.Controls;
using Microsoft.Win32;
using S1xxViewer.Base;
using S1xxViewer.Model.Interfaces;
using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace S1xxViewerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Autofac.IContainer _container;
        private List<IS1xxDataPackage> _dataPackages = new List<IS1xxDataPackage>();

        public MainWindow()
        {
            InitializeComponent();
            _container = AutofacInitializer.Initialize();

            this.Title += " v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Task.Factory.StartNew(() =>
            {
                var fileNames = RetrieveRecentFiles();
                int i = 1;
                foreach (var fileName in fileNames)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        var newMenuItem = new MenuItem
                        {
                            Name = $"MenuItem{i++}",
                            Header = fileName
                        };
                        newMenuItem.Click += AutoOpen_Click;

                        RecentFilesMenuItem.Items.Add(newMenuItem);
                    });
                }
            });
        }

        #region Menu Handlers

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
        public void AppOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML/GML files (*.xml;*.gml)|*.xml;*.gml|ENC files (*.000)|*.031|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;

                if (fileName.ToLower().Contains(".xml") || fileName.ToLower().Contains(".gml"))
                {
                    LoadGMLFile(fileName);
                }
                else if (fileName.Contains(".031"))
                {
                    LoadENCFile(fileName);
                }
            }
        }

        /// <summary>
        /// Clears all layers and resets to initial program open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ResetLayers_Click(object sender, RoutedEventArgs e)
        {
            _dataPackages.Clear();
            dataGrid.ItemsSource = null;
            treeView.Items.Clear();
            MyMapView.Map.OperationalLayers.Clear();
        }

        /// <summary>
        ///     Show options menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OptionsMenu_Click(object sender, RoutedEventArgs e)
        {
            var newOptionsMenu = new OptionsWindow();
            newOptionsMenu.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AutoOpen_Click(object sender, RoutedEventArgs e)
        {
            var fileName = ((MenuItem)sender).Header.ToString();
            if (fileName.ToLower().Contains(".xml") || fileName.ToLower().Contains(".gml"))
            {
                LoadGMLFile(fileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snder"></param>
        /// <param name="e"></param>
        public void TreeviewItem_Click(object sender, RoutedEventArgs e)
        {
            var itemDataTable = ((TreeViewItem)sender).Tag as DataTable;
            if (itemDataTable != null)
            {
                dataGrid.ItemsSource = itemDataTable.AsDataView();
            }
        }

        #endregion

        #region UI logic

        /// <summary>
        /// Loads the specified ENC file
        /// </summary>
        /// <param name="fileName">fileName</param>
        private async void LoadENCFile(string fileName)
        {
            List<Layer> nonEncLayers =
                MyMapView.Map.OperationalLayers.ToList().FindAll(tp => !tp.GetType().ToString().Contains("EncLayer"));
            MyMapView.Map.OperationalLayers.Clear();

            var myEncExchangeSet = new EncExchangeSet(fileName);
            // Wait for the exchange set to load
            await myEncExchangeSet.LoadAsync();

            // Store a list of data set extent's - will be used to zoom the mapview to the full extent of the Exchange Set
            List<Envelope> dataSetExtents = new List<Envelope>();

            // Add each data set as a layer
            foreach (EncDataset myEncDataset in myEncExchangeSet.Datasets)
            {
                // Create the cell and layer
                EncLayer myEncLayer = new EncLayer(new EncCell(myEncDataset));

                // Add the layer to the map
                MyMapView.Map.OperationalLayers.Add(myEncLayer);

                // Wait for the layer to load
                await myEncLayer.LoadAsync();

                // Add the extent to the list of extents
                dataSetExtents.Add(myEncLayer.FullExtent);
            }

            if (nonEncLayers.Any())
            {
                foreach (var nonEncLayer in nonEncLayers)
                {
                    MyMapView.Map.OperationalLayers.Add(nonEncLayer);
                }
            }

            // Use the geometry engine to compute the full extent of the ENC Exchange Set
            Envelope fullExtent = GeometryEngine.CombineExtents(dataSetExtents);

            // Set the viewpoint
            MyMapView.SetViewpoint(new Viewpoint(fullExtent));
        }

        /// <summary>
        /// Loads the specified GML file
        /// </summary>
        /// <param name="fileName">fileName</param>
        private async void LoadGMLFile(string fileName)
        {
            Title = $"S1xx Viewer ({fileName.LastPart(@"\")})";
            _dataPackages.Clear();
            dataGrid.ItemsSource = null;
            treeView.Items.Clear();

            Layer encLayer = MyMapView.Map.OperationalLayers.ToList().Find(tp => tp.GetType().ToString().Contains("EncLayer"));
            MyMapView.Map.OperationalLayers.Clear();

            if (encLayer != null)
            {
                MyMapView.Map.OperationalLayers.Add(encLayer);
            }

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                SaveRecentFile(fileName);

                var dataParser = _container.Resolve<IDataPackageParser>();
                IS1xxDataPackage dataPackage = await dataParser.ParseAsync(xmlDoc);
                CreateFeatureCollection(dataPackage);

                _dataPackages.Add(dataPackage);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve recent file from recentfiles.txt
        /// </summary>
        /// <returns>string[]</returns>
        private string[] RetrieveRecentFiles()
        {
            var tempPath = Path.GetTempPath();
            var recentFileFileName = $@"{tempPath}\recentfiles.txt";

            if (File.Exists(recentFileFileName))
            {
                string[] fileContent = File.ReadAllLines(recentFileFileName);
                return fileContent;
            }

            return new string[0];
        }

        /// <summary>
        /// Save entry to the recent files
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveRecentFile(string fileName)
        {
            var tempPath = Path.GetTempPath();
            var fileContent = new string[0];

            var recentFileFileName = $@"{tempPath}\recentfiles.txt";
            if (File.Exists(recentFileFileName))
            {
                fileContent = File.ReadAllLines(recentFileFileName);
            }

            lock (this)
            {
                if (!fileContent.ToList().Contains(fileName))
                {
                    var newFileNames = new List<string>() { fileName };
                    for (int j = 0; j < (fileContent.Length > 4 ? 4 : fileContent.Length); j++)
                    {
                        newFileNames.Add(fileContent[j]);
                    }

                    File.Delete(recentFileFileName);
                    File.WriteAllLines(recentFileFileName, newFileNames);

                    RecentFilesMenuItem.Items.Clear();
                    int i = 1;
                    foreach (var newFileName in newFileNames)
                    {
                        var newMenuItem = new MenuItem
                        {
                            Name = $"MenuItem{i++}",
                            Header = newFileName
                        };
                        newMenuItem.Click += AutoOpen_Click;
                        RecentFilesMenuItem.Items.Add(newMenuItem);
                    }
                }
            }
        }

        /// <summary>
        /// Finds the specified feature in the S1xx datapackage
        /// </summary>
        /// <param name="featureId">Id value of the feature</param>
        /// <returns>IFeature</returns>
        private IFeature FindFeature(string featureId)
        {
            foreach(IS1xxDataPackage dataPackage in _dataPackages)
            {
                var resultInGeoFeature =
                    dataPackage.GeoFeatures.ToList().Find(ftr => ftr.Id == featureId);

                if (resultInGeoFeature != null)
                {
                    return resultInGeoFeature;
                }

                var resultInMetaFeature =
                    dataPackage.MetaFeatures.ToList().Find(f => f.Id == featureId);

                if (resultInMetaFeature != null)
                {
                    return resultInMetaFeature;
                }

                var resultInInfoFeatures =
                    dataPackage.InformationFeatures.ToList().Find(f => f.Id == featureId);

                if (resultInInfoFeatures != null)
                {
                    return resultInInfoFeatures;
                }
            }

            return null;
        }

        #endregion

        #region Methods that have connections to ArcGIS

        /// <summary>
        /// Creation a feature collection for rendering on the map
        /// </summary>
        /// <param name="dataPackage">S1xx dataPackage</param>
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
            LabelDefinition idLabelDefinition = LabelDefinition.FromJson(theJSON_String);
            
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
            polysTable.DisplayName = "Polygons";

            FeatureCollectionTable linesTable = new FeatureCollectionTable(lineFields, GeometryType.Polyline, SpatialReferences.Wgs84);
            linesTable.Renderer = CreateRenderer(GeometryType.Polyline);
            linesTable.DisplayName = "Lines";

            FeatureCollectionTable pointTable = new FeatureCollectionTable(pointFields, GeometryType.Point, SpatialReferences.Wgs84);
            pointTable.Renderer = CreateRenderer(GeometryType.Point);
            pointTable.DisplayName = "Points";

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
            featuresCollection.Tables.Clear();

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

            var collectionLayer = new FeatureCollectionLayer(featuresCollection);

            // When the layer loads, zoom the map view to the extent of the feature collection
            collectionLayer.Loaded += (s, e) => Dispatcher.Invoke(() => 
            {
                try
                {
                    foreach(FeatureLayer layer in collectionLayer.Layers)
                    {
                        layer.LabelDefinitions.Add(idLabelDefinition);
                        layer.LabelsEnabled = true;

                        MyMapView.SetViewpointAsync(new Viewpoint(layer.FullExtent));
                    }
                }
                catch (Exception) { }
            });

            // Add the layer to the Map's Operational Layers collection
            MyMapView.Map.OperationalLayers.Add(collectionLayer);
            MyMapView.GeoViewTapped += OnMapViewTapped;
        }

        /// <summary>
        /// If the mapview is tapped
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
                double pixelTolerance = 1.0;
                bool returnPopupsOnly = false;
                int maxResults = 5;

                // Identify a  group layer using MapView, passing in the layer, the tap point, tolerance, types to return, and max results.

                var results = new Dictionary<string, DataTable>();
                foreach (Layer operationalLayer in MyMapView.Map.OperationalLayers)
                {
                    var collectionLayer = operationalLayer as FeatureCollectionLayer;
                    if (collectionLayer != null)
                    {
                        IdentifyLayerResult groupLayerResult =
                            await MyMapView.IdentifyLayerAsync(collectionLayer, tapScreenPoint, pixelTolerance, returnPopupsOnly, maxResults);

                        if (groupLayerResult.SublayerResults.Count > 0)
                        {
                            // Iterate each set of child layer results.
                            foreach (IdentifyLayerResult subLayerResult in groupLayerResult.SublayerResults)
                            {
                                // clear featureselection in all layers
                                collectionLayer?.Layers.ToList().ForEach(l => l.ClearSelection());

                                // Iterate each geoelement in the child layer result set.
                                foreach (GeoElement idElement in subLayerResult.GeoElements)
                                {
                                    // cast the result GeoElement to Feature
                                    Feature idFeature = idElement as Feature;

                                    // select this feature in the feature layer
                                    var layer = subLayerResult.LayerContent as FeatureLayer;
                                    if (layer != null)
                                    {
                                        layer.SelectFeature(idFeature);
                                    }

                                    if (idElement.Attributes.ContainsKey("FeatureId"))
                                    {
                                        if (!String.IsNullOrEmpty(idElement.Attributes["FeatureId"]?.ToString()))
                                        {
                                            IFeature feature = FindFeature(idElement.Attributes["FeatureId"].ToString());
                                            if (feature != null)
                                            {
                                                DataTable featureAttributesDataTable = feature.GetData();
                                                string key = (feature is IGeoFeature ? ((IGeoFeature)feature).FeatureName.First()?.Name : feature.Id.ToString()) ?? "No named feature";

                                                if (results.ContainsKey(key))
                                                {
                                                    int i = 0;
                                                    while (results.ContainsKey($"{key} ({++i})")) ;
                                                    key = $"{key} ({i})";
                                                }

                                                results.Add(key, featureAttributesDataTable);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (results != null && results.Count > 0)
                {
                    dataGrid.ItemsSource = results.First().Value.AsDataView();
                    treeView.Items.Clear();

                    var parentTreeNode = new TreeViewItem
                    {
                        Header = $"Selected feature{(results.Count == 1 ? "" : "s")}",
                        Tag = null,
                        IsExpanded = true
                    };
                    treeView.Items.Add(parentTreeNode);

                    foreach (var result in results)
                    {
                        TreeViewItem treeNode = new TreeViewItem();
                        treeNode.MouseUp += TreeviewItem_Click;
                        treeNode.Header = result.Key;
                        treeNode.Tag = result.Value;                        

                        parentTreeNode.Items.Add(treeNode);
                    }

                    ((TreeViewItem)parentTreeNode.Items[0]).IsSelected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sample error", ex.ToString());
            }
        }

        /// <summary>
        /// Creates the renderer for features on the map
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
                    var lineSym = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.FromArgb(255, 50, 50, 50), 1);
                    sym = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.FromArgb(25, System.Drawing.Color.LightGray), lineSym);
                   
                    break;
                default:
                    break;
            }

            // Return a new renderer that uses the symbol created above
            return new SimpleRenderer(sym);
        }

        #endregion

        // Map initialization logic is contained in MapViewModel.cs
    }
}
