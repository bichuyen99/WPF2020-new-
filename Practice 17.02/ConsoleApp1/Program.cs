using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class motion
    {
        double g = 9.8;
        double angle;
        double velocity;
        double time;
        double verticaldistance;
        double horizontaldistance;

        public motion(double v, double t, double a)
        {
            velocity = v;
            time = t;
            angle = a;
            double radangle = angle * (Math.PI / 180);
            horizontaldistance = v * Math.Cos(a) * t;
            verticaldistance = v * Math.Sin(a) * t - 0.5 * g * Math.Pow(t,2);
        }
        public double Velocity
        {
            get { return velocity; }
        }
        public double Angle
        {
            get { return angle; }
        }
        public double Time
        {
            get { return time; }
        }
        public double Horizontaldistance
        {
            get { return horizontaldistance; }
            set { horizontaldistance = value; }
        }
        public double Verticaldistance
        {
            get { return verticaldistance; }
            set { verticaldistance = value; }
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            string input;
            double angle;
            double velocity;
            double time;
            double x;
            double y;
            Console.Write("Enter velocity:");
            input = Console.ReadLine();
            velocity = double.Parse(input);
            Console.Write("Enter time:");
            input = Console.ReadLine();
            time = double.Parse(input);
            Console.Write("Enter angle:");
            input = Console.ReadLine();
            angle = double.Parse(input);
            Console.WriteLine("Launch velocity: {0}", velocity);
            Console.WriteLine("Launch time: {0}", time);
            Console.WriteLine("Launch angle: {0}", angle);
            motion m = new motion(velocity, time, angle);
            x = m.Horizontaldistance;
            y = m.Verticaldistance;
            Console.WriteLine("Horizontal distance:{0}", x);
            Console.WriteLine("Vertical distance:{0}", y);
        }
    }
}