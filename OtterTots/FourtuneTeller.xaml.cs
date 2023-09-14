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
using Windows.Media.SpeechSynthesis;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OtterTots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FourtuneTeller : Page
    {
        private MediaPlayer BGM,intro;
        private MediaPlayer effect;
        private Random gen;
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private MediaElement mediaElement = new MediaElement();
        #region Arrays
        string[] animals =
        {
            "Mrs Cow","Mr Bull","Mr Rooster","Miss Pig","Mrs Duck"
        };

        string[] sounds =
        {
            "Moo moo","Growl growl","COCK-A-DOODLE-DOO","Oink Oink","Quack,quack"
        };
        string[] setting = 
        {
        "on a balcony.",
        "in a stately home.",
        "in a psychiatric hospital.",
        "in a newspaper office.",
        "in an art gallery.",
        "in a registry office.",
        "in a lift.",
        "in a marina.",
        "on a farm."
        };
        private string[] situation =
        {
            "A phone call devastates the family.",
            "Someone is being blackmailed.",
            "A blind date is the start of something big.",
            "The family celebrates a homecoming.",
            "Something precious has been lost.",
            "Someone thought dead is discovered alive.",
            "Someone receives an inheritance.",
            "Someone hitches a ride home during a train strike.",
            "The wedding anniversary is forgotten."


        };
        private string[] action =
        {
            "is tested to the limits of physical endurance",
            "sets out to change everyone's opinion",
            "takes control of the situation",
            "sets out to change everyone's opinion",
            "discovers some unpleasant truths",
            "has to resort to underhand methods to achieve results",
            "has some questions to answer",
            "is determined to get to the truth",
            "offers to lend a helping hand"

        };
        private string[] theme =
        {
            "stubbornness",
            "opportunity",
            "an adventure",
            "the supernatural",
            "jealousy",
            "secrecy",
            "deception",
            "freedom",
            "learning from mistakes"
        };
        #endregion


         public FourtuneTeller()
        {
            this.InitializeComponent();
            BGM = new MediaPlayer();
            intro = new MediaPlayer();
            
            
        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));

        }

        private void Letters_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SlotMachine));

        }

        private void Maths_Tapped(object sender, TappedRoutedEventArgs e)
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

        private void Songs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DrinkList));

        }

        private async void  Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("BGMFourtuneTeller.mp3");
            BGM.Source = MediaSource.CreateFromStorageFile(file);
            BGM.Play();
            BGM.IsLoopingEnabled = true;
            BGM.Volume = 0.2;

            
            Windows.Storage.StorageFile Intro = await folder.GetFileAsync("introAnimals.mp3");
            intro.Source = MediaSource.CreateFromStorageFile(Intro);
            intro.Play();
            intro.Volume = 0.5;

        }

        private void Tap1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.playSound("1.mp3");
            story.Text=getStory(0);
            textToSpeech(story.Text);
            intro.Source = null;


        }

        private void Tap2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.playSound("2.mp3");
            story.Text = getStory(1);
            textToSpeech(story.Text);
            intro.Source = null;

        }

        private void Tap3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.playSound("3.mp3");
            story.Text = getStory(2);
            textToSpeech(story.Text);
            intro.Source = null;
        }

        private void Tap4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.playSound("4.mp3");
            story.Text = getStory(3);
            textToSpeech(story.Text);
            intro.Source = null;
        }

        private void Tap5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.playSound("5.mp3");
            story.Text = getStory(4);
            textToSpeech(story.Text);
            intro.Source = null;
        }

        #region Methods

        /// Method to get and set story string
        private string getStory(int count)
        {
            try
            {
                gen = new Random();
                string Setting = setting[gen.Next(0, setting.Length)];
                string Situation = situation[gen.Next(0, situation.Length)];
                string Action = action[gen.Next(0, action.Length)];
                string Theme = theme[gen.Next(0, theme.Length)];
                string Animal = animals[count];
                string Sound = sounds[count];

                return "This is " + Animal + ", it says " + Sound + ". This is a story about " + Theme + ". It begins " + Setting + " " + Animal + " finds that " + Situation + " " + Animal + " " + Action + ". The end.";
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.WriteLine("Index out of range exception in getStory(): " + ex.Message);
                return "An error occurred while generating the story.";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred in getStory(): " + ex.Message);
                return "An error occurred while generating the story.";
            }
        }

        

        // Method for text to speech
        private async void textToSpeech(string textToSpeech)
        {
            try
            {
                string speech = textToSpeech;

                SpeechSynthesisStream synthesisStream = await synthesizer.SynthesizeTextToStreamAsync(speech);
                synthesizer.Options.SpeakingRate = 0.8;

                mediaElement.SetSource(synthesisStream, synthesisStream.ContentType);
                mediaElement.Play();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred in textToSpeech(): " + ex.Message);
                // Handle exceptions
            }
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            mediaElement.Pause();
            BGM.Source = null;
            intro.Source = null;

        }

        #endregion
    }
}
