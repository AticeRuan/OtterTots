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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OtterTots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    
    public sealed partial class DiceGame : Page
    {
        Random num;
        MediaPlayer BGM, intro, overVoice;
        int[] option;
        int answer;
        int score = App.score;
        public DiceGame()
        {
            this.InitializeComponent();
            BGM = new MediaPlayer();
            intro = new MediaPlayer();
            overVoice = new MediaPlayer();            
            point.Text = score.ToString();

        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));          

        }

        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            BGM.Source = null;
            intro.Source = null;
        }

        private void Roll_Tapped(object sender, TappedRoutedEventArgs e)
        {
            newGame();
            Roll.Opacity = 0f;
            bird.Opacity = 0f;
            whiteBase.Opacity = 0f;
            Canvas.SetZIndex(Roll, -10);
            Canvas.SetZIndex(bird, -10);
            Canvas.SetZIndex(whiteBase, -10);
            setOption();
            App.playSound("gameStart.mp3");

        }

        private void Letters_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SlotMachine));
            BGM.Source = null;
            intro.Source = null;
        }

        private void Animals_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FourtuneTeller));
            BGM.Source = null;
            intro.Source = null;
        }

        private void ShapesAndColours_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Lotto));
            BGM.Source = null;
            intro.Source = null;
        }

        private void TreasureHunt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Map));
            BGM.Source = null;
            intro.Source = null;
        }

        private void Songs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DrinkList));
            BGM.Source = null;
            intro.Source = null;
        }

        private void Answer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _ = restart();

        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await folder.GetFileAsync("BGMDiceGame.mp3");
            BGM.Source = MediaSource.CreateFromStorageFile(file);
            BGM.Play();
            BGM.IsLoopingEnabled = true;
            BGM.Volume = 0.2;
            StorageFile Intro = await folder.GetFileAsync("introMath.mp3");
            intro.Source = MediaSource.CreateFromStorageFile(Intro);
            intro.Play();
            intro.Volume = 0.5;

        }


        private void option1_Tapped(object sender, TappedRoutedEventArgs e)
        {

            checkAnswer(Convert.ToInt32(option1.Content));
        }
        private void option2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            checkAnswer(Convert.ToInt32(option2.Content));
        }

        private void option3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            checkAnswer(Convert.ToInt32(option3.Content));
        }


        #region Methods


        //Method to shuffle numbers in array
        public void Shuffle<T>(T[] array)
        {
            num = new Random();
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = num.Next(0, i + 1);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        //Method to set equations
        private void newGame()
        {
            try
            {
                num = new Random();
                int num1 = num.Next(1, 10);
                int num2 = num.Next(1, 10);
                int op = num.Next(1, 3);

                if (op == 1)
                {
                    if (num1 - num2 >= 0)
                    {
                        Op.Source = new BitmapImage(new Uri("ms-appx:///Assets/numbers/sub.png", UriKind.RelativeOrAbsolute));
                        answer = num1 - num2;
                    }
                    else
                    {
                        Op.Source = new BitmapImage(new Uri("ms-appx:///Assets/numbers/sum.png", UriKind.RelativeOrAbsolute));
                        answer = num1 + num2;
                    }
                }
                else
                {
                    Op.Source = new BitmapImage(new Uri("ms-appx:///Assets/numbers/sum.png", UriKind.RelativeOrAbsolute));
                    answer = num1 + num2;
                }

                Num1.Source = new BitmapImage(new Uri("ms-appx:///Assets/numbers/" + num1.ToString() + ".png", UriKind.RelativeOrAbsolute));
                Num2.Source = new BitmapImage(new Uri("ms-appx:///Assets/numbers/" + num2.ToString() + ".png", UriKind.RelativeOrAbsolute));

                Answer1.Text = answer.ToString();
                Answer1.Opacity = 0f;
                Answer.Opacity = 1f;
                
            }
            catch (UriFormatException ex)
            {
                Debug.WriteLine("URI format error: " + ex.Message);
                // Handle URI format exceptions
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("Argument error: " + ex.Message);
                // Handle argument-related exceptions
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Null reference error: " + ex.Message);
                // Handle null reference exceptions
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Handle other exceptions
            }
        }

        private async Task restart()
        {
            try
            {
                Answer.Opacity = 0f;
                Answer1.Opacity = 1f;
                await Task.Delay(1000);
                newGame();
                setOption();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred in restart(): " + ex.Message);
                // Handle exceptions
            }
        }

        public void setOption()
        {
            try
            {
                num = new Random();
                option = new int[3];
                option[0] = answer;

                do
                {
                    option[1] = num.Next(1, 10);
                } while (option[1] == option[0]);

                do
                {
                    option[2] = num.Next(10, 19);
                } while (option[2] == option[0]);

                Shuffle(option);
                option1.Content = option[0].ToString();
                option2.Content = option[1].ToString();
                option3.Content = option[2].ToString();
                option1.Opacity = 1f;
                option2.Opacity = 1f;
                option3.Opacity = 1f;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred in setOption(): " + ex.Message);
                // Handle exceptions
            }
        }

        private async void checkAnswer(int userAnswer)
        {
            try
            {
                int selectedAnswer = userAnswer;

                if (score <= 0)
                {
                    over.Source = new BitmapImage(new Uri("ms-appx:///Assets/GameOver.png", UriKind.RelativeOrAbsolute));
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
                    if (selectedAnswer == answer)
                    {
                        Answer1.Opacity = 1f;
                        Answer.Opacity = 0f;
                        point.Text = App.winPoint(50);
                        _ = restart();
                        App.playSound("win.wav");
                    }
                    else
                    {
                        if (score < 30)
                        {
                            point.Text = App.losePoint(Convert.ToInt32(point.Text));
                        }
                        else
                        {
                            point.Text = App.losePoint(30);
                        }
                        App.playSound("lose.mp3");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred in checkAnswer(): " + ex.Message);
                // Handle exceptions
            }
        }

        #endregion

    }
}
