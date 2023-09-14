using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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
using Windows.Storage;
using Windows.Media.SpeechSynthesis;
using System.Diagnostics;

namespace OtterTots
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public static int score;
        public static MediaPlayer BGM, intro,effect;
        public static Random randomNum; 
        public static MediaElement mediaElement;
        public static SpeechSynthesizer synthesizer;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            startScore();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
                   


        }

        #region Methods to create/load/save score value from/to txt file in local folder
        /// <summary>
        /// load score, save score, and start score methods
        /// </summary>

        public static async void loadScore()
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile scoreFile = await folder.GetFileAsync("point.txt");
                string scoreText = await FileIO.ReadTextAsync(scoreFile);

                if (int.TryParse(scoreText, out int Score))
                {
                    score = Score;
                }
                else
                {
                    score = 0;
                }
            }
            catch (FileNotFoundException)
            {
                score = 0;
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("Unauthorized access error: " + ex.Message);
                // Handle unauthorized access scenario
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while loading score: " + ex.Message);
                // Handle other exceptions
            }
        }

        public static async void saveScore()
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile scoreFile = await folder.CreateFileAsync("point.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(scoreFile, score.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("Unauthorized access error: " + ex.Message);
                // Handle unauthorized access scenario
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while saving score: " + ex.Message);
                // Handle other exceptions
            }
        }



        public static async Task<bool> IsFilePresent(string fileName)
        {
            try
            {
                var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
                return item != null;
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("Unauthorized access error: " + ex.Message);
                // Handle unauthorized access scenario
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while checking file presence: " + ex.Message);
                // Handle other exceptions
                return false;
            }
        }

        public static async void startScore()
        {
            try
            {
                if (!await IsFilePresent("point.txt"))
                {
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile scoreFile = await storageFolder.CreateFileAsync("point.txt",
                        CreationCollisionOption.ReplaceExisting);
                    score = 1000;
                    await FileIO.WriteTextAsync(scoreFile, score.ToString());
                }
                else
                {
                    loadScore();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("Unauthorized access error: " + ex.Message);
                // Handle unauthorized access scenario
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while starting score: " + ex.Message);
                // Handle other exceptions
            }
        }

        #endregion

        #region Methods modify score value
        public static string winPoint(int count)
        {

            score += count;
            return score.ToString();
            
            
        }

        public static string losePoint(int count)
        {
            score -= count;
            return score.ToString();
            
        }
        #endregion

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            saveScore();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        public static async void playSound(string audio)
        {
            try
            {
                effect = new MediaPlayer();
                StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                StorageFile file = await folder.GetFileAsync(audio);
                effect.Source = MediaSource.CreateFromStorageFile(file);
                effect.Play();
                effect.Volume = 0.2;
            }
            catch (FileNotFoundException ex)
            {
                Debug.WriteLine("File not found exception in playSound(): " + ex.Message);
                // Handle file not found
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred in playSound(): " + ex.Message);
                // Handle other exceptions
            }
        }






    }
}
