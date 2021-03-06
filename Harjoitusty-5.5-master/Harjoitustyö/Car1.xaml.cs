﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Harjoitustyö
{
    public sealed partial class Car1 : UserControl
    {
        private DispatcherTimer timer;
        private int currentFrame = 0;
        private int direction = 1;

        //location
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        //Speed values             

        public double MaxSpeed { get; set; }
        public double MinSpeed { get; set; }
        private readonly double Accelarate = 2.0;
        // public double Break { get; set; }
        private readonly double Break = 1.5;

        
       
        
        public double speed;
        public double speed1;
       public enum AccelerationDirection
        {
            Forward = 1,
            Backward = -1
        }
        // Cars angle
        private double Angle = 0;
        private readonly double AngleTier = 5;

        public double CanvasWidth;
        public double CanvasHeight;
        public double CanvasWidthMap;
        public double CanvasHeightMap;


        public Car1()
        {
            this.InitializeComponent();


           
            //maxspeed value
            MaxSpeed = 10;
            MinSpeed = -5;

            //starts the animation ?
            timer = new DispatcherTimer();
            timer.Tick += Timer_Car;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 150);
            timer.Start();
        }

        private void Timer_Car(object sender, object e)
        {
            //frame: 0,1,2,3,4
            if (direction == 1) currentFrame++;
            else currentFrame--;
            // direction
            if (currentFrame == 0 || currentFrame == 4) direction *= -1;
        }

        // Update position
        public void Updateposition()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        public void Rotate(int direction)

                //car rotation angle
        {
            if (speed > 1)
            {
                Angle += direction * AngleTier;
                Car_1_Angle.Angle = Angle;
            }
            if (speed < -1)
            {
                Angle += direction * AngleTier;
                Car_1_Angle.Angle = Angle;
            }
        }

       

        //forward movement
        public void Drive(int direction)
        {
            speed += Accelarate*direction;
            Debug.WriteLine("speed = " + speed);
            if (speed > MaxSpeed)
                speed = MaxSpeed;

        
            if (speed < MinSpeed)
                speed = MinSpeed;



            //if (speed > MaxSpeed) speed = MaxSpeed;
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle + 90))) * speed;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle + 90))) * speed;


            //LocationX -= (Math.Cos(Math.PI / 180 * (Angle - 90 * (double)accelerationDirection1))) * speed1;
            //LocationY -= (Math.Sin(Math.PI / 180 * (Angle - 90 * (double)accelerationDirection1))) * speed1;

        }



        // backwards
     /*   public void Drive1()
        {
            speed1 += Break;
            if (speed1 > MaxSpeed1) speed1 = MaxSpeed1;
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle - 90))) * speed1;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle - 90))) * speed1;


        }
        */
            // car slow downs when you don't push buttons
        public void Slow()

        {

            speed = speed - 0.5;
            if (speed <= 0) speed = 0;
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle + 90))) * speed;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle + 90))) * speed;

            speed1 = speed1 - 0.5;
            if (speed1 <= 0) speed1 = 0;
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle - 90))) * speed1;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle - 90))) * speed1;
        }

      

    }

}
