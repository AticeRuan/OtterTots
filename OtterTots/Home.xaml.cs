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
using System.Threading.Tasks;




// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OtterTots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        MediaPlayer BGM;
        MediaPlayer Intro;

       
        public Home()
        {
            this.InitializeComponent();
            BGM = new MediaPlayer();
            Intro = new MediaPlayer();
            point.Text = App.score.ToString();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            BGM.Source = null;
            Intro.Source = null;
        }

        private void Info_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(CompanyDetail));

        }

        private void Math_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiceGame));

        }

        private void ShapesAndColours_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Lotto));

        }

        private void TreasureHunt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Map));

        }

        private void Animals_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FourtuneTeller));

        }

        private void Letters_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SlotMachine));

        }

        private void Songs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DrinkList));

        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file1 = await folder.GetFileAsync("BGM.MP3");
            Windows.Storage.StorageFile file2 = await folder.GetFileAsync("intro.MP3");
            BGM.Source = MediaSource.CreateFromStorageFile(file1);
            Intro.Source = MediaSource.CreateFromStorageFile(file2);
            BGM.Play();
            Intro.Play();
            BGM.IsLoopingEnabled = true;
            BGM.Volume = 0.2;

        }

    }
}
