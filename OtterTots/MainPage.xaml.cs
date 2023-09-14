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
using Windows.UI.ViewManagement;    // Setup the device sizing for the application

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

#region You do not need to edit this page - it sets up the clock, navigation and device size selection for you

namespace OtterTots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Timer code for the Clock
        DispatcherTimer dispatcherTimer;
        #endregion
        public MainPage()
        {
            this.InitializeComponent();

            #region Setup the device sizing for the application         
            ApplicationView.GetForCurrentView().TryResizeView(new Size(1038, 636));
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1038, 636));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            #endregion

            #region Timer code for the Clock
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.Start();
            #endregion

            #region Loading the first of the application
            MyFrame.Navigate(typeof(Home));
            #endregion
        }

        #region Timer code for the Clock
        void dispatcherTimer_Tick(object sender, object e)
        {
            TimeTextBlock.Text = DateTime.Now.ToShortTimeString();
        }
        #endregion

        #region Setup the device sizing for the application  
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size(1038, 636));
        }
        #endregion

        #region Navigation code
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoForward)
            {
                MyFrame.GoForward();
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Home));
        }
        #endregion



        //private void CameraButton_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}

#endregion
