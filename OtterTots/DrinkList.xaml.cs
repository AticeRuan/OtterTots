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
using Windows.Media.Playback;
using Windows.Media.Core;
using System.Threading.Tasks;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OtterTots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DrinkList : Page
    {
        Dictionary<string, Songs> songDic = new Dictionary<string, Songs>()
        {
            {"The Farmer In The Dell", new Songs
            {
                Name = "The Farmer In The Dell",
                ImageName = "TheFarmerInTheDell.jpg",
                Lyric ="The farmer in the dell,\nThe farmer in the dell,\nHeigh-ho, the derry-o,\nThe farmer in the dell.\n\nThe farmer takes a wife,\nThe farmer takes a wife,\nHeigh-ho, the derry-o,\nThe farmer takes a wife.\n\nThe wife takes a child,\nThe wife takes a child,\nHeigh-ho, the derry-o,\nThe wife takes a child.\n\nThe child takes a nurse,\nThe child takes a nurse,\nHeigh-ho, the derry-o,\nThe child takes a nurse.\n\nThe nurse takes a dog,\nThe nurse takes a dog,\nHeigh-ho, the derry-o,\nThe nurse takes a dog.\n\nThe dog takes a cat,\nThe dog takes a cat,\nHeigh-ho, the derry-o,\nThe dog takes a cat.\n\nThe cat takes a rat,\nThe cat takes a rat,\nHeigh-ho, the derry-o,\nThe cat takes a rat.\n\nThe rat takes the cheese,\nThe rat takes the cheese,\nHeigh-ho, the derry-o,\nThe rat takes the cheese.\n\nThe cheese stands alone,\nThe cheese stands alone,\nHeigh-ho, the derry-o,\nThe cheese stands alone.",
                Path = "TheFarmerInTheDell.mp3"
            }
            },
            {"The Muffin Man", new Songs
            { Name="The Muffin Man",
                ImageName="TheMuffinMan.jpg",
                Lyric="Do you know the Muffin man,\nThe Muffin man, the Muffin man,\nOh, do you know the Muffin man,\nWho lives down jewellery lane?\n\nOh yes, we know the Muffin man,\nThe Muffin man, the Muffin man,\nOh yes, we know the Muffin man,\nWho lives down jewellery lane.\n\nOh, we all know the Muffin man,\nThe Muffin Man, the Muffin man,\nOh, we all know the Muffin man,\nWho lives down jewellery lane.",
                Path="TheMuffinMan.mp3"

            } 
            },
            {"Itsy Bitsy Spider", new Songs
            { Name="Itsy Bitsy Spide",
                ImageName="ItsyBitsySpider.jpg",
                Lyric="The itsy-bitsy spider\nClimbed up the water spout\nDown came the rain\nAnd washed the spider out\nOut came the sun\nAnd dried up all the rain\nAnd the itsy-bitsy spider\nClimbed up the spout again.",
                Path="ItsyBitsySpider.mp3"

            }
            },
            {"London Bridge", new Songs
            { Name="London Bridge",
                ImageName="LondonBridge.jpg",
                Lyric="London Bridge is falling down\nFalling down, falling down\nLondon Bridge is falling down\nMy fair lady\nBuild it up with iron bars\nIron bars, iron bars\nBuild it up with iron bars\nMy fair lady\nIron bars will bend and break\nBend and break, bend and break\nIron bars will bend and break\nMy fair lady\nBuild it up with gold and silver\nGold and silver, gold and silver\nBuild it up with gold and silver\nMy fair lady\nLondon Bridge is falling down\nFalling down, falling down\nLondon Bridge is falling down\nM-y f-a-i-r l-a-d-y.",
                Path="LondonBridge.mp3"

            }
            },
            {"Mary Had A Little Lamb", new Songs
            { Name="Mary Had A Little Lamb",
                ImageName="MaryHadALittleLamb.jpg",
                Lyric="Mary had a little lamb\nlittle lamb, little lamb\nMary had a little lamb\nIts fleece was white as snow\nAnd everywhere that Mary went\nMary went, Mary went\nAnd everywhere that Mary went\nThe lamb was sure to go\nIt followed her to school one day\nSchool one day, school one day\nIt followed her to school one day\nThat was against the rule\nMary had a little lamb\nlittle lamb, little lamb\nMary had a little lamb\nIts fleece was white as snow.",
                Path="MaryHadALittleLamb.mp3"

            }
            },
            {"This Old Man", new Songs
            { Name="This Old Man",
                ImageName="ThisOldMan.jpg",
                Lyric="This old man, he plays one\nHe plays one on his old drum, oh yes, yes-yes, uh-huh\nWell, he plays one on his old drum, uh-huh\nThis old man, he plays two\nHe plays two on his kazoo, oh yes, yes-yes, uh-huh\nHe plays two on his kazoo, uh-huh\nThis old man, he plays three\nHe plays three on his ukulele, uh-huh, yes, yes, uh-huh\nHe plays three on his ukulele, uh huh\nHear him play!\nThis old man, he plays four\nHe play four on his guitar, oh yes (knick knack pattywack)\nYes-yes, uh-huh (give a dog a bone, knick knack pattywack, give a dog a bone)\nHe plays four on his guitar, uh-huh\nThis old man, he plays five\nHe plays five with his friend Clive, oh yes\nYes, yes, uh-huh\nHe plays five with his friend Clive, uh-huh\nTake it, Clive!\nKnick knack! Paddywack!\nKnick knack! Paddywack!\nThis old man, he plays one\nThis old man, he plays two\nThis old man, he plays three\nThis old man, he plays four\nThis old man, he plays five\nKnick knack! Paddywack!",
                Path="ThisOldMan.mp3"

            }
            },
            {"Old MacDonald", new Songs
            { Name="Old MacDonald",
                ImageName="OldMacDonald.png",
                Lyric="Old MacDonald had a farm\nEe i ee i o\nAnd on his farm he had some cows\nEe i ee i oh\nWith a moo-moo here\nAnd a moo-moo there\nHere a moo, there a moo\nEverywhere a moo-moo\nOld MacDonald had a farm\nEe i ee i o\nOld MacDonald had a farm\nEe i ee i o\nAnd on his farm he had some chicks\nEe i ee i o\nWith a cluck-cluck here\nAnd a cluck-cluck there\nHere a cluck, there a cluck\nEverywhere a cluck-cluck\nOld MacDonald had a farm\nEe i ee i o\nOld MacDonald had a farm\nEe i ee i o\nAnd on his farm he had some pigs\nEe i ee i o\nWith an oink-oink here\nAnd an oink-oink there\nHere an oink, there an oink\nEverywhere an oink-oink\nOld MacDonald had a farm\nEe i ee i o",
                Path="OldMacDonald.mp3"

            }
            },
            {"Rock-a-bye Baby", new Songs
            { Name="Rock-a-bye Baby",
                ImageName="RockabyeBaby.jpg",
                Lyric="Rock a bye baby, on the tree top,\nWhen the wind blows the cradle will rock.\nWhen the bough breaks the cradle will fall,\nAnd down will come baby, cradle and all.\n\nRock a bye baby, gently you swing,\nOver the cradle, Mother will sing,\nSweet is the lullaby over your nest\nThat tenderly sings my baby to rest.\n\nFrom the high rooftops, down to the sea\nNo one's as dear as baby to me\nWee little hands, eyes shiny and bright\nNow sound asleep until morning light\n\nRock a bye baby, on the tree top,\nWhen the wind blows the cradle will rock.\nWhen the bough breaks the cradle will fall,\nAnd down will come baby, cradle and all.",
                Path="RockabyeBaby.mp3"

            }
            },
            {"The Alphabet Song", new Songs
            { Name="The Alphabet Song",
                ImageName="TheAlphabetSong.jpg",
                Lyric="A, B, C, D, E, F, G\n H, I, J, K, L, M, N, O, P\nQ, R, S,\n T, U, V\n W, X, Y, Z.\n Now I know my ABCs.\n Next time won't you sing with me",
                Path="TheAlphabetSong.mp3"

            }
            },
            {"Row Row Row Your Boat", new Songs
            { Name="Row Row Row Your Boat",
                ImageName="RowRowRowYourBoat.jpg",
                Lyric="Row, row, row your boat\nGently down the stream\nMerrily merrily, merrily, merrily\nLife is but a dream\nRow, row, row your boat\nGently down the stream\nMerrily merrily, merrily, merrily\nLife is but a dream\nRow, row, row your boat\nGently down the stream\nMerrily merrily, merrily, merrily\nLife is but a dream\nRow, row, row your boat\nGently down the stream\nMerrily merrily, merrily, merrily\nLife is but a dream",
                Path="RowRowRowYourBoat.mp3"

            }
            },
            {"Hickory Dickory Dock", new Songs
            { Name="Hickory Dickory Dock",
                ImageName="HickoryDickoryDock.jpg",
                Lyric="Hickory dickory dock. The mouse went up the clock\nThe clock struck one. The mouse went down\nHickory dickory dock\nTick tock, tick tock, tick tock, tick tock\nA snake\nHickory dickory dock. The snake went up the clock\nThe clock struck two. The snake went down\nHickory dickory dock\nTick tock, tick tock, tick tock, tick tock\nA squirrel\nHickory dickory dock. The squirrel went up the clock\nThe clock struck three. The squirrel went down\nHickory dickory dock\nTick tock, tick tock, tick tock, tick tock\nA cat\nHickory dickory dock. The cat went up the clock\nThe clock struck four. The cat went down\nHickory dickory dock\nTick tock, tick tock, tick tock, tick tock\nA monkey\nHickory dickory dock. The monkey went up the clock\nThe clock struck five. The monkey went down\nHickory dickory dock\nTick tock, tick tock, tick tock, tick tock\nAn elephant, oh no\nHickory dickory dock. The elephant went up the clock\nOh no\nHickory dickory dock",
                Path="HickoryDickoryDock.mp3"

            }
            },
            {"Hush Little Baby", new Songs
            { Name="Hush Little Baby",
                ImageName="HushLittleBaby.jpg",
                Lyric="Lulla-lulla-lullaby\nHush, little baby don't you\nHush, little baby don't say a word\nPapa's gonna buy you a mocking bird\nAnd if that mocking bird don't sing\nPapa's gonna buy you a diamond ring\nAnd if that diamond ring is brass\nPapa's gonna buy you a looking glass\nAnd if that looking glass gets broke\nPapa's gonna buy you a billy goat\nAnd if that billy goat don't pull\nPapa's gonna buy you a cart and bull\nAnd if that cart and bull turn over\nPapa's gonna buy you a dog called Rover\nAnd if that dog called Rover don't bark\nPapa's gonna buy you a horse and cart\nAnd if that horse and cart turn round\nYou'll still be the sweetest little babe in town (be the sweetest litte babe)\nStill be the sweetest little babe in town\nLa, la, la, la, la, la\nHush, little baby don't you cry...",
                Path="HushLittleBaby.mp3"

            }
            },
        };
        private MediaPlayer song=new MediaPlayer();
        MediaPlayer intro;
        public DrinkList()
        {
            this.InitializeComponent();
            Songs.ItemsSource = songDic.Keys;
            intro = new MediaPlayer();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            intro.Source = null;
            song.Source = null;
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

        private void TreasureHunt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Map));

        }

        private void Math_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiceGame));


        }

        private async void Songs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string songSelected = Songs.SelectedItem.ToString();
            
            Songs playing = songDic[songSelected];
            lyric.Text = playing.Lyric.ToString();
            SongImage.Source= new BitmapImage(
        new Uri("ms-appx:///Assets/songs/" + playing.ImageName.ToString(), UriKind.RelativeOrAbsolute));

            
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync(playing.Path.ToString());
            if (song.Source!=null)
            {
                song.Source=null;
            }          

             song.Source = MediaSource.CreateFromStorageFile(file);
             song.Play();
            song.Volume = 0.2;

        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            

            Windows.Storage.StorageFile Intro = await folder.GetFileAsync("introSongs.mp3");
            intro.Source = MediaSource.CreateFromStorageFile(Intro);
            intro.Play();
            intro.Volume = 0.5;
        }
    }
}
