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
using Windows.UI.Xaml.Media.Imaging;
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
    public sealed partial class Lotto : Page
    {
        private int[] nums = new int[12];
        private Image[] imageArray;
        private int[] numberArray;
        private int currentIndex = 0;
        private string controlName;
        private MediaPlayer BGM, intro, overVoice;
        private int score= App.score;

        public Lotto()
        {
            this.InitializeComponent();
            BGM = new MediaPlayer();
            intro = new MediaPlayer();
            overVoice = new MediaPlayer();
            imageArray = new Image[] { shape1, shape2,shape3, shape4, shape5, shape6, shape7, shape8, shape9, shape10, shape11, shape12};
            numberArray = Enumerable.Range(1, 24).ToArray(); // Generate an array of numbers from 1 to 24
            ShuffleNumberArray();
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




            private void Letters_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SlotMachine));

        }

        private void Animals_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FourtuneTeller));

        }

        private void Math_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiceGame));

        }

        private void TreasureHunt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Map));

        }

        private void Songs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DrinkList));

        }



        private void roll_Tapped(object sender, TappedRoutedEventArgs e)
        {

            newGame();
            roll.Source = null;
            App.playSound("gameStart.mp3");

        }
        


        

        private void shape1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape1 != null)
            {
                controlName = GetControlNameFromImage(shape1);

            }
            _ = MatchShape();
        }

        private void shape2_Tapped(object sender, TappedRoutedEventArgs e)
        {
           

            if (shape2 != null)
            {
                controlName = GetControlNameFromImage(shape2);

            }
            _ = MatchShape();
        }

        private void shape3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            

            if (shape3 != null)
            {
                controlName = GetControlNameFromImage(shape3);

            }
            _ = MatchShape();
        }

        private void shape4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            

            if (shape4 != null)
            {
                controlName = GetControlNameFromImage(shape4);


            }
            _ = MatchShape();
        }

        private void shape5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape5 != null)
            {
                controlName = GetControlNameFromImage(shape5);


            }
            _ = MatchShape();
        }

        private void shape6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape6 != null)
            {
                controlName = GetControlNameFromImage(shape6);


            }
            _ = MatchShape();
        }

        private void shape7_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape7 != null)
            {
                controlName = GetControlNameFromImage(shape7);

            }
            _ = MatchShape();
        }

        private void shape8_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape8 != null)
            {
                controlName = GetControlNameFromImage(shape8);

            }
            _ = MatchShape();
        }

        private void shape9_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
            controlName = GetControlNameFromImage(shape9);

            if (shape9 != null)
            {
                

            }
            _ = MatchShape();
        }

        private void shape10_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape10 != null)
            {
                controlName = GetControlNameFromImage(shape10);


            }
            _ = MatchShape();
        }

        private void shape11_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape11 != null)
            {
                controlName = GetControlNameFromImage(shape11);

            }
            _ = MatchShape();
        }

        private void shape12_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (shape12 != null)
            {
                controlName = GetControlNameFromImage(shape12);

            }
            _ = MatchShape();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("BGMLotto.MP3");
            BGM.Source = MediaSource.CreateFromStorageFile(file);
            BGM.Play();
            BGM.IsLoopingEnabled = true;
            BGM.Volume = 0.2;

            Windows.Storage.StorageFile Intro = await folder.GetFileAsync("introSC.mp3");
            intro.Source = MediaSource.CreateFromStorageFile(Intro);
            intro.Play();
            intro.Volume = 0.5;
        }

        #region Methods 
        //Method to reset the shape grid
        private void newGame()
        {
            nums = GetNextUniqueNumbers(12); // Returns an array of 12 unique numbers


            for (int i = 0; i < imageArray.Length; i++)
            {
                imageArray[i].Source = new BitmapImage(new Uri("ms-appx:///Assets/shapes/" + nums[i].ToString() + ".png", uriKind: UriKind.RelativeOrAbsolute));
                imageArray[i].Tag = nums[i].ToString();

            }

            Random path = new Random();
            int shapeNum = path.Next(0, 12);
            atStart.Source = null;
            matchAnswer.Source = null;
            shape.Source = new BitmapImage(new Uri("ms-appx:///Assets/shapes/" + nums[shapeNum].ToString() + ".png", uriKind: UriKind.RelativeOrAbsolute));
            shape.Tag = nums[shapeNum].ToString();
            
        }

        //Methods to generate non-repeative numbers
        private void ShuffleNumberArray()
        {
            Random random = new Random();

            for (int i = numberArray.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = numberArray[i];
                numberArray[i] = numberArray[j];
                numberArray[j] = temp;
            }
        }

        private int[] GetNextUniqueNumbers(int count)
        {
            if (currentIndex + count > numberArray.Length)
            {
                ShuffleNumberArray();
                currentIndex = 0;
            }

            int[] uniqueNumbers = new int[count];
            Array.Copy(numberArray, currentIndex, uniqueNumbers, 0, count);
            currentIndex += count;
            return uniqueNumbers;
        }
        
        // Method to get image tag
        private string GetControlNameFromImage(Image imageControl)
        {
            string Name = imageControl.Tag.ToString();
            return Name;
        }

        //Method Compare tapped shape to main shape
        private async Task MatchShape()
        {
            try
            {
                if (score > 0)
                {
                    if (controlName == null)
                    {
                        matchAnswer.Source = null;
                    }
                    if (controlName == shape.Tag.ToString())
                    {
                        matchAnswer.Source = new BitmapImage(new Uri("ms-appx:///Assets/shapes/yes.png", uriKind: UriKind.RelativeOrAbsolute));
                        await Task.Delay(1000);
                        App.playSound("win.wav");
                        point.Text = App.winPoint(50);
                        newGame();
                        
                        
                    }
                    else
                    {
                        if (score < 30)
                        {
                            matchAnswer.Source = new BitmapImage(new Uri("ms-appx:///Assets/shapes/no.png", uriKind: UriKind.RelativeOrAbsolute));
                            point.Text = App.losePoint(Convert.ToInt32(point.Text));
                        }
                        else
                        {
                            point.Text = App.losePoint(30);
                        }

                        matchAnswer.Source = new BitmapImage(new Uri("ms-appx:///Assets/shapes/no.png", uriKind: UriKind.RelativeOrAbsolute));
                        App.playSound("lose.mp3");
                    }
                }
                else
                {
                    over.Source = new BitmapImage(new Uri("ms-appx:///Assets/GameOver.png", uriKind: UriKind.RelativeOrAbsolute));
                    Canvas.SetZIndex(over, 11);
                    white.Opacity = 0.6f;
                    Canvas.SetZIndex(white, 10);

                    StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                    StorageFile gameOver = await folder.GetFileAsync("over.mp3");
                    overVoice.Source = MediaSource.CreateFromStorageFile(gameOver);
                    overVoice.Play();
                    overVoice.Volume = 0.5;
                    point.Text = "0";
                }
            }
            catch (NullReferenceException nullEx)
            {
                Debug.WriteLine("Null Reference Exception: " + nullEx.Message);
                // Handle null reference exception
            }
            catch (FormatException formatEx)
            {
                Debug.WriteLine("Format Exception: " + formatEx.Message);
                // Handle format exception
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Handle other exceptions as needed
            }
           
        }






    }
    #endregion

}

