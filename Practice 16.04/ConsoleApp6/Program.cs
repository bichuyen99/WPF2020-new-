using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionMax
{
    class Motion
    {
        double g = 9.8;
        double a;
        double tmax;
        double Hmax;
        double t2;
        double t;
        double xmax;
        public Motion(double angle, double t1, double v0, double x0, double y0)
        {
            a = angle * Math.PI / 180;
            tmax = v0 * Math.Sin(a) / g;
            Hmax = Math.Pow(v0 * Math.Sin(a), 2) / (2 * g)+ y0;
            t2 = Math.Sqrt(2 * (y0 + Hmax) / g);
            t = t1 + t2;
            xmax = x0+Math.Pow(v0, 2) * Math.Sin(2 * a) / (2 * g) + v0 * Math.Cos(a) * Math.Sqrt(2 * (y0 + Hmax) / g);
        }
        public double Angle
        {
            get { return Angle = a;}
            set { a = value; }
        }
        public double Timemax
        {
            get { return Timemax = tmax; }
            set { tmax = value; }
        }
        public double highest
        {
            get { return highest = Hmax; }
            set { Hmax = value; }
        }
        public double Groundtime
        {
            get { return Groundtime = t2; }
            set { t2 = value; }
        }
        public double Sumtime
        {
            get { return Sumtime = t; }
            set { t = value; }
        }
        public double distance
        {
            get { return distance = xmax; }
            set { xmax = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input;
            double angle;
            double t1;
            double v0;
            double x0;
            double y0;
            Console.Write("Enter start velocity:");
            input = Console.ReadLine();
            v0 = double.Parse(input);
            Console.Write("Enter angle:");
            input = Console.ReadLine();
            angle = double.Parse(input);
            Console.Write("Enter start time:");
            input = Console.ReadLine();
            t1 = double.Parse(input);
            Console.Write("Enter start point Ox:");
            input = Console.ReadLine();
            x0 = double.Parse(input);
            Console.Write("Enter start point in Oy:");
            input = Console.ReadLine();
            y0 = double.Parse(input);
            Motion m = new Motion(angle, t1, v0, x0, y0);
            Console.WriteLine("Angle:{0}", m.Angle);
            Console.WriteLine("time, when object in the highest place:{0}", m.Timemax);
            Console.WriteLine("highest place:{0}", m.highest);
            Console.WriteLine("time, when object flies from the highest place to the ground:{0}", m.Groundtime);
            Console.WriteLine("time of all process:{0}", m.Sumtime);
            Console.WriteLine("the farthest distance:{0}", m.distance);
        }
    }

}
   