using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Harjoitustyö
{
    public sealed partial class Game : Page
    {
        private Windows.Storage.StorageFile sampleFile;
        // First car
        private Car1 car1;

        //enviroment
        private Asfalt asfalt;
        private Sand sand;
        private Line startLine;
        private UserControl finishLine;
        //private UserControl wallCollision;

        // Canvas Width and Height 
        public double CanvasWidth;
        public double CanvasHeight;
        public double CanvasWidthMap;
        public double CanvasHeightMap;


        public double speed;
        public double Maxspeed = 7.5;



        // Controls
        private bool Up;
        private bool Left;
        private bool Right;
        private bool Down;
        private Stopwatch stopwatch;
        private MediaElement racemusa;
        // game timer
        private DispatcherTimer game;
        public Game()
        {
            this.InitializeComponent();

            // window size
            ApplicationView.PreferredLaunchWindowingMode
               = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);

            // New stopwatch created

            stopwatch = new Stopwatch();

            // get owen canvas sizes
            CanvasWidth = Track.Width;
            CanvasHeight = Track.Height;
            CanvasWidthMap = Track.Width;
            CanvasHeightMap = Track.Height;

            //add map
            asfalt = new Asfalt { LocationX = CanvasWidthMap, LocationY = CanvasHeightMap };
            Track.Children.Add(asfalt);
          

            // add sand usercontrol
            sand = new Sand { LocationX = CanvasWidthMap, LocationY = CanvasHeightMap };
            Track.Children.Add(sand);

            //add first car

            car1 = new Car1 { LocationX = CanvasWidth / 4, LocationY = CanvasHeight / 4 };
            Track.Children.Add(car1);
            car1.Updateposition();

            //Finishline UsewrControl
            finishLine = asfalt.Time;
           // wallCollision = asfalt.Wall;

            // key listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            Highscore();

            ReadFile();



            // Game fps
            game = new DispatcherTimer();
            game.Tick += Game_Timer;
            game.Interval = new TimeSpan(0, 0, 0, 0, 600 / 70);
            game.Start();

            // Timer to the game
            //  stopwatch.Start();
            // Audio to the game
            InitAudio();

            //startLine = new Line();
            //Track.Children.Add(startLine);

        }

        // Sounds to the game
        private async void InitAudio()
        {
            racemusa = new MediaElement();
            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file = await folder.GetFileAsync("Race.mp3");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            racemusa.AutoPlay = true;
            racemusa.IsLooping = true;
            racemusa.SetSource(stream, file.ContentType);
            //racemusa.Position = TimeSpan.Zero;
            //racemusa.Play();

        }

        private async void Highscore()
        {

            Windows.Storage.StorageFolder highScore = Windows.Storage.ApplicationData.Current.LocalFolder;
            sampleFile = 
                await highScore.CreateFileAsync("HighScore.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);

        }
        private async void ReadFile()
        {
            timerLog.Text =
                await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
        }

        private void Game_Timer(object sender, object e)
        {
            //move

            if (Up) car1.Drive(1);
            if (Down) car1.Drive(-1);
            //spinning
            if (Left) car1.Rotate(-3);
            if (Right) car1.Rotate(3);
            car1.Slow();

            SandCollision();
            FinishLineCollision();
            // Timer running to textbox
            timerLog.Text = stopwatch.ElapsedMilliseconds.ToString();

            car1.Updateposition();
        }

       /*   public void WallCollision()
        {
            Rect car = new Rect(car1.LocationX, car1.LocationY, car1.ActualWidth, car1.ActualHeight);
            var x = this.wallCollision.Margin;
            var wallCollisionRectangle = new Rect(x.Left, x.Top, this.wallCollision.Width, this.wallCollision.Height);
            if (!car.IsEmpty)
            {
                car1.MaxSpeed = 0;
                car1.MinSpeed = 0;
            }
            else
            {
                car1.MaxSpeed = 10;
                car1.MaxSpeed = -5;
            }

                    
            }
        */
        
        // checks when car passes finishline and starts timer
        public void FinishLineCollision()
        {
            Rect car = new Rect(car1.LocationX, car1.LocationY, car1.ActualWidth, car1.ActualHeight);
            var x = this.finishLine.Margin;
            var finishLineRectangle = new Rect(x.Left, x.Top, this.finishLine.Width, this.finishLine.Height);
            car.Intersect(finishLineRectangle);
            if (!car.IsEmpty)
            {
                this.stopwatch.Restart();
                

            }


        // this.stopwatch.ToString();
        // string foldername = @"D:\K3295\Harjoitusty-5.5\Harjoitusty-5.5-master\Harjoitustyö\HighScore";
        // string[] lines = { "First line", "Second line", "Third line" };
        // System.IO.File.WriteAllLines(@"D:\K3295\Harjoitusty-5.5\Harjoitusty-5.5-master\Harjoitustyö\HighScore", lines);

        private async void HighScore()
        {
            Windows.Storage.StorageFolder highScore = Windows.Storage.ApplicationData.Current.LocalFolder;
            sampleFile = await highScore.CreateFileAsync("HighScore.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
        }



        private async void Readfile()
        {
           
            timerLog.Text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            Debug.WriteLine(timerLog);
        }







        public void SandCollision()
        {
            //get rect from sand

            Rect car = new Rect(car1.LocationX, car1.LocationY, car1.ActualWidth, car1.ActualHeight);
            Rect obstacle = new Rect(128, 135, 976, 466);

            car.Intersect(obstacle);

            if (!car.IsEmpty)
            {
                car1.MaxSpeed = 2;
                car1.MinSpeed = -1.5;
            }
            else
            {
                car1.MaxSpeed = 10;
                car1.MinSpeed = -5;
            }
        }











        // see if the controls are not pressed
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Up:
                    Up = false;
                    break;

                case VirtualKey.Left:
                    Left = false;
                    break;

                case VirtualKey.Right:
                    Right = false;
                    break;

                case VirtualKey.Down:
                    Down = false;
                    break;

                default:
                    break;
            }
        }

        // if controls are pressed
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Up:
                    Up = true;
                    break;

                case VirtualKey.Left:
                    Left = true;
                    break;

                case VirtualKey.Right:
                    Right = true;
                    break;

                case VirtualKey.Down:
                    Down = true;
                    break;

                default:
                    break;
            }
        }



        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null) return;

            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                racemusa.IsLooping = false;
                racemusa.Stop();
            }
        }
             private void timerLog_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        }
    }

    

