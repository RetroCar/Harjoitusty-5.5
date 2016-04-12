using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        // First car
        private Car1 car1;

        //enviroment
        private Asfalt asfalt;
        private Sand sand;
       
        // Canvas Width and Height 
        private double CanvasWidth;
        private double CanvasHeight;
        private double CanvasWidthMap;
        private double CanvasHeightMap;


        public double speed;
        public double Maxspeed = 7.5;
        


        // Controls
        private bool Up;
        private bool Left;
        private bool Right;
        private bool Down;

        // game timer
        private DispatcherTimer game;
        public Game()
        {
            this.InitializeComponent();

            // window size
            ApplicationView.PreferredLaunchWindowingMode
               = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);

            //get my own canvas size

            CanvasWidth = Track.Width;
            CanvasHeight = Track.Height;
            CanvasWidthMap = Track.Width;
            CanvasHeightMap = Track.Height;

            //add map
            asfalt = new Asfalt { LocationX = CanvasHeightMap, LocationY = CanvasHeightMap };
            Track.Children.Add(asfalt);

            sand = new Sand { LocationX = CanvasHeightMap, LocationY = CanvasHeightMap };
            Track.Children.Add(sand);

            //add first car

            car1 = new Car1 { LocationX = CanvasWidth / 4, LocationY = CanvasHeight / 4 };
            Track.Children.Add(car1);
            car1.Updateposition();


            // key listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            // Game fps
            game = new DispatcherTimer();
            game.Tick += Game_Timer;
            game.Interval = new TimeSpan(0, 0, 0, 0, 600 / 70);
            game.Start();
        }
               private void Game_Timer(object sender, object e)
        {
            //move

            if (Up) car1.Drive();
            if (Down) car1.Drive1();
            //spinning
            if (Left) car1.Rotate(-3);
            if (Right) car1.Rotate(3);

            SandCollision();


            car1.Updateposition();
        }


        public void SandCollision()
        {
            //get rect from sand

            Rect car = new Rect(car1.LocationX, car1.LocationY, car1.ActualWidth, car1.ActualHeight);
            Rect obstacle = new Rect(sand.LocationX, sand.LocationY, sand.ActualWidth, sand.ActualHeight);

            car.Intersect(obstacle);

            if (!car.IsEmpty) 
            {
                car1.MaxSpeed = 5;
            }
            else
            {
                car1.MaxSpeed = 15;
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

              private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null) return;

            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null) return;

            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }
    }
    }

