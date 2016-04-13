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

        
        
        

        public void Slow()
        
        {
           //front slowing
            speed = speed - 0.5;
                if (speed <= 0) speed = 0;
                    LocationX -= (Math.Cos(Math.PI / 180 * (Angle + 90)))*speed;
                    LocationY -= (Math.Sin(Math.PI / 180 * (Angle + 90)))*speed;
            //backward slowing
            speed1 = speed1 - 0.5;
            if (speed1 <= 0) speed1 = 0;
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle - 90))) * speed1;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle - 90))) * speed1;


        }








        private readonly double MaxSpeed = 15.0;
        private readonly double MaxSpeed1 = 7.5;
        private readonly double Accelarate = 2.0;
       // public double Break { get; set; }
        private readonly double Break = 1.5;

        private double speed;
        private double speed1;

        // Cars angle
        private double Angle = 0;
        private readonly double AngleTier = 5;
        public Car1()
        {
            this.InitializeComponent();

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

        {
            if (speed > 1)
            {
                Angle += direction * AngleTier;
                Car_1_Angle.Angle = Angle;
            }
            if (speed1 > 1)
            {
                Angle += direction * AngleTier;
                Car_1_Angle.Angle = Angle;
            }
        }

        //forward movement
        public void Drive()
        {
           
            speed += Accelarate;
            if (speed > MaxSpeed) speed = MaxSpeed;
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle + 90))) * speed;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle + 90))) * speed;

           

        }



        // backwards
       public void Drive1()
        {
            speed1 += Break;
            if (speed1 > MaxSpeed1) speed1 = MaxSpeed1;
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle - 90))) * speed1;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle - 90))) * speed1;

        }
    }
}
