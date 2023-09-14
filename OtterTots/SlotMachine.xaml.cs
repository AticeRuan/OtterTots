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
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using System.Diagnostics;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OtterTots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SlotMachine : Page
    {
        private MediaPlayer BGM, intro, overVoice;
        private Random randomNum;
        private string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private int reelIndex;
        private List<string> englishWords = new List<string>();
        private string wordToCheck="";
        private MediaElement mediaElement = new MediaElement();
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private int score= App.score;
        public SlotMachine()
        {
            this.InitializeComponent();
            BGM = new MediaPlayer();
            intro = new MediaPlayer();
            overVoice = new MediaPlayer();
            LoadEnglishWords();
            point.Text = score.ToString();
            
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

        private void Math_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiceGame));

        }

        private void Animals_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FourtuneTeller));

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

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await folder.GetFileAsync("BGMSlotMachine.MP3");
            BGM.Source = MediaSource.CreateFromStorageFile(file);
            BGM.Play();
            BGM.IsLoopingEnabled = true;
            BGM.Volume = 0.2;

            StorageFile Intro = await folder.GetFileAsync("introLetters.mp3");
            intro.Source = MediaSource.CreateFromStorageFile(Intro);
            intro.Play();
            intro.Volume = 0.5;
        }

        private void reel1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selectedLetter(reel1, reel1.Tag.ToString());
        }

        private void tap_Tapped(object sender, TappedRoutedEventArgs e)
        {
            setLetter(reel1);
            setLetter(reel2);
            setLetter(reel3);
            point.Text=App.losePoint(10);
            App.playSound("lose.mp3");

        }

        private void reel3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selectedLetter(reel3, reel3.Tag.ToString());
        }

        private void reel2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selectedLetter(reel2, reel2.Tag.ToString());
        }

        private void check_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                if (wordToCheck != "")
                {
                    if (englishWords.Contains(wordToCheck))
                    {
                        check.Source = new BitmapImage(new Uri("ms-appx:///Assets/shapes/yes.png", UriKind.RelativeOrAbsolute));
                        textToSpeech(wordToCheck);
                        point.Text = App.winPoint(100);
                        App.playSound("win.wav");
                    }
                    else
                    {
                        if (score < 50)
                        {
                            point.Text = App.losePoint(Convert.ToInt32(point.Text));
                        }
                        else
                        {
                            point.Text = App.losePoint(50);
                        }
                        check.Source = new BitmapImage(new Uri("ms-appx:///Assets/shapes/no.png", UriKind.RelativeOrAbsolute));
                        textToSpeech("Try Again!");
                        App.playSound("lose.mp3");
                    }
                }
                _ = restart();
            }
            catch (FormatException ex)
            {
                Debug.WriteLine("Format Exception: " + ex.Message);
                // Handle FormatException (e.g., if there's an issue with Convert.ToInt32)
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("Invalid Operation Exception: " + ex.Message);
                // Handle InvalidOperationException (e.g., if there's an issue with collections)
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Handle other exceptions as needed
            }
        }

        #region Methods
        //Method to select letter from slot to write on blackboard
        private void selectedLetter(Image letter,string tag)
        {
            
            if (letter.Tag.ToString()!="?")
            {
                int count = Convert.ToInt32(tag);
                word.Text += alphabet[count];
                letter.Source = null;
                wordToCheck += alphabet[count].ToLower();
                textToSpeech(alphabet[count]);
            }
            
            
        }

        //method to set letters at slots
        private async void setLetter(Image letter)
        {
            if (score<10)
            {
                over.Source = new BitmapImage(new Uri("ms-appx:///Assets/GameOver.png", uriKind: UriKind.RelativeOrAbsolute));
                Canvas.SetZIndex(over, 11);
                white.Opacity = 0.8f;
                Canvas.SetZIndex(white, 10);

                StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                StorageFile gameOver = await folder.GetFileAsync("over.mp3");
                overVoice.Source = MediaSource.CreateFromStorageFile(gameOver);
                overVoice.Play();
                overVoice.Volume = 0.5;
                point.Text = "0";

            }
            else
            {
            randomNum = new Random();
            reelIndex = randomNum.Next(0, 26);
            
            letter.Source = new BitmapImage(new Uri("ms-appx:///Assets/letters/" + reelIndex.ToString() + ".png", uriKind: UriKind.RelativeOrAbsolute));
            letter.Tag = reelIndex.ToString();
            
            }
            
            
        }
        //Method to load word list from txt file from Assets to List Collection
        private async void LoadEnglishWords()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/wordList.txt"));
            using (StreamReader reader = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                while (!reader.EndOfStream)
                {
                    string word = reader.ReadLine();
                    englishWords.Add(word);
                }
            }
           
        }


        //Method to reset the reels
        private async Task restart()
        {
            await Task.Delay(2000);
            word.Text = "";
            check.Source = new BitmapImage(new Uri("ms-appx:///Assets/letters/MagGlass.png", uriKind: UriKind.RelativeOrAbsolute));
            setLetter(reel1);
            setLetter(reel2);
            setLetter(reel3);
            wordToCheck = "";
        }

        //Method of Text to Speech
        private async void textToSpeech(string textToSpeach)
        {
            string speech = textToSpeach;

            SpeechSynthesisStream synthesisStream = await synthesizer.SynthesizeTextToStreamAsync(speech);

            synthesizer.Options.SpeakingRate = 0.5;
            
            mediaElement.SetSource(synthesisStream, synthesisStream.ContentType);
            mediaElement.Play();

        }
        #endregion
    }
}
