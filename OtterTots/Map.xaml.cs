using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Services.Maps;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OtterTots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        MediaPlayer BGM, intro;
        int score = App.score;
        List<int> picNumCollection = new List<int>();
        List<Geopoint> locationCollection=new List<Geopoint>();


        List<cityLocations> cities = new List<cityLocations>()
        {
            new cityLocations("London",51.5,5,-0.1,0.6),
            new cityLocations("Paris",48.8,49.3,2.2,2.7),
            new cityLocations("Venice",45.4,45.9,12.3,12.8),
            new cityLocations("San Francisco",37.8,38.3,-122.4,-121.9),
            new cityLocations("New York",40.7,41.2,-73.9,-73.4),
            new cityLocations("Florence",43.7,44.2,-122.4,-121.9),
            new cityLocations("Tokyo",35.6,36.1,139.8,140.3)
        };

        public Map()
        {
            this.InitializeComponent();
            BGM = new MediaPlayer();
            intro = new MediaPlayer();
            point.Text = score.ToString();
            App.randomNum = new Random();
            generateBadge();
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            BGM.Source = null;
            intro.Source = null;
        }
        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));

        }

        private void Letters_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SlotMachine));

        }

        private void Animals_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FourtuneTeller));

        }

        private void ShapesAndColours_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Lotto));

        }

        private void Math_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiceGame));

        }

        private void Songs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DrinkList));

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("BGMMap.MP3");
            BGM.Source = MediaSource.CreateFromStorageFile(file);
            BGM.Play();
            BGM.IsLoopingEnabled = true;
            BGM.Volume = 0.2;

            Windows.Storage.StorageFile Intro = await folder.GetFileAsync("introTh.mp3");
            intro.Source = MediaSource.CreateFromStorageFile(Intro);
            intro.Play();
            intro.Volume = 0.5;
            



        }

        private async void huntMap_Loaded(object sender, RoutedEventArgs e)
        {
            //check for streetside support
            //if (huntMap.IsStreetsideSupported)
            //{
            //    //check that map is available for lat and long
            //    var panorama = await StreetsidePanorama.FindNearbyAsync(huntMap.Center);

            //    if (panorama != null)
            //    {
            //        //create custom view
            //        huntMap.CustomExperience = new StreetsideExperience(panorama);
            //    }
            //}


            //check for 3D support
            if (huntMap.Is3DSupported)
                {
                //set style to 3D
                huntMap.Style = MapStyle.Aerial3DWithRoads;

                    //create a mapscene for lat and long
                    //facing East (90 deg) and pitched 45 deg
                    var mapScene = MapScene.CreateFromLocationAndRadius(huntMap.Center, 500, 90, 45);
                    await huntMap.TrySetSceneAsync(mapScene);
                }
        }

        

        //Tap mapicons to remove from map
        private void huntMap_MapElementClick(MapControl sender, MapElementClickEventArgs args)
                {
                    try
                    {
                        var Elements = args.MapElements;
                        var elementsToRemove = new List<MapElement>();
                        foreach (var item in args.MapElements)
                        {
                            elementsToRemove.Add(item);
                            int itemIndex = Convert.ToInt32(item.Tag);
                            point.Text=App.winPoint(Convert.ToInt32(picNumCollection[itemIndex])*10);
                            App.playSound("win.wav");
                            locationCollection.RemoveAt(itemIndex);
                            picNumCollection.RemoveAt(itemIndex);


                        }

                        foreach (var item in elementsToRemove)
                        {
                            huntMap.MapElements.Remove(item);
                    

                        }
                
               
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error in huntMap_Tapped: " + ex.Message);
                    }
                }

        #region Methods


        //set minimum zoom level
        private void huntMap_ZoomLevelChanged(MapControl sender, object args)
        {
            int minZoom = 17;
            if (huntMap.ZoomLevel < minZoom)
            {
                huntMap.ZoomLevel = minZoom;
            }
        }
        //Method generate MapIcons,centre at random location within range
        private void generateBadge()
        {


            try
            {
                double[] latitude = new double[1000];
                double[] longitude = new double[1000];
                
                int cityNum = App.randomNum.Next(0, cities.Count);
                huntMap.MapElements.Clear();
                picNumCollection.Clear();
                locationCollection.Clear();

                for (int i = 0; i < latitude.Length; i++)
                {
                    MapIcon badge = new MapIcon();
                    int picNum = App.randomNum.Next(1, 11);
                    // Generate latitude and longitude values
                    latitude[i] = App.randomNum.NextDouble() * (cities[cityNum].maxLatitude - cities[cityNum].minLatitude) + cities[cityNum].minLatitude;
                    longitude[i] = App.randomNum.NextDouble() * (cities[cityNum].maxLongitude - cities[cityNum].minLongitude) + cities[cityNum].minLongitude;

                    badge.Location = new Geopoint(new BasicGeoposition { Latitude = latitude[i], Longitude = longitude[i] });

                    // Use a URI based on the picNum 
                    badge.Image = RandomAccessStreamReference.CreateFromUri(new Uri($"ms-appx:///Assets/map/{picNum}.png"));

                    badge.ZIndex = 10;
                    badge.Tag = i.ToString();
                    badge.Visible = true;
                    huntMap.MapElements.Add(badge);
                    badge.Visible = true;
                    picNumCollection.Add(picNum);
                    locationCollection.Add(badge.Location);
                }

                int locationNum = App.randomNum.Next(1, locationCollection.Count + 1);
                huntMap.Center = locationCollection[locationNum];
                huntMap.ZoomLevel = 20;
                huntMap.DesiredPitch = 68;
                locationName.Text = cities[cityNum].name;
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.WriteLine("IndexOutOfRangeException: " + ex.Message);
                // Handle the index out of range exception
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("InvalidOperationException: " + ex.Message);
                // Handle the invalid operation exception
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred" + ex.Message);
                // Handle other exceptions
            }

        }
        //shift to another city and re-render badges
        private void shift_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
                        generateBadge();
            App.playSound("lose.mp3");
            point.Text = App.losePoint(100);
        }

        //tap to re-centre map to location of any MapIcon
        private  void hint_Tapped(object sender, TappedRoutedEventArgs e)
        {

            int locationNum = App.randomNum.Next(1, locationCollection.Count + 1);

            huntMap.Center = locationCollection[locationNum];

            //    MapRouteFinderResult routeResult =
            //await MapRouteFinder.GetDrivingRouteAsync(huntMap.Center, locationCollection[locationNum],
            //MapRouteOptimization.Time,
            //MapRouteRestrictions.None);

            //    if (routeResult.Status == MapRouteFinderStatus.Success)
            //    {
            //        // Use the route to initialize a MapRouteView.
            //        MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
            //        viewOfRoute.RouteColor = Colors.Yellow;
            //        viewOfRoute.OutlineColor = Colors.Black;

            //        // Add the new MapRouteView to the Routes collection
            //        // of the MapControl.
            //        huntMap.Routes.Add(viewOfRoute);

            //        // Fit the MapControl to the route.
            //        await huntMap.TrySetViewBoundsAsync(
            //              routeResult.Route.BoundingBox,
            //              null,
            //              MapAnimationKind.None);
            //    }



            App.playSound("lose.mp3");


            point.Text = App.losePoint(50);
        }







        //set maximum pitch value
        private void huntMap_PitchChanged(MapControl sender, object args)
        {
            int maxPitch = 45;
            if (huntMap.DesiredPitch > maxPitch)
            {
                huntMap.DesiredPitch = maxPitch;
            }
        }




        #endregion

    }
}
