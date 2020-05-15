using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp16
{

    class motion
    {
        double g = 9.8;
        double angle;
        double[] velocityOx;
        double[] velocityOy;
        double velocity;
        double deltatime;
        double[] time;
        double[] verticaldistance;
        double[] horizontaldistance;
        double[] wind;
        double weight;
        int subinterval;

        public motion(double v, double x, double y, double dt, double m, double a, int n, double t)
        {
            deltatime = dt;
            angle = a;
            weight = m;
            velocity = v;
            subinterval = n;
            time[0] = t;
            double radangle = angle * (Math.PI / 180);
            velocityOx[0] = v * Math.Cos(radangle);
            velocityOy[0] = v * Math.Sin(radangle);
            horizontaldistance[0] = x;
            verticaldistance[0] = y;
            for (int i = 0; i <= n; i++)
            {
                wind[i] = Math.Sin(10 * time[i]);
                horizontaldistance[i + 1] = horizontaldistance[i] + dt * velocityOx[i];
                verticaldistance[i + 1] = verticaldistance[i] + dt * velocityOy[i];
                velocityOx[i + 1] = velocityOx[i] - dt * wind[i] * velocityOx[i] / m;
                velocityOy[i + 1] = velocityOy[i] - dt * (g + (wind[i] * velocityOy[i]) / m);
                time[i + 1] = time[i] + dt;
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
                double deltatime;
                double weight;
                double x;
                double y;
                int subintervals;

                Console.Write("The number of subintervals is divided:");
                input = Console.ReadLine();
                subintervals = int.Parse(input);
                Console.Write("Delta-time:");
                input = Console.ReadLine();
                deltatime = int.Parse(input);
                Console.Write("Enter time:");
                input = Console.ReadLine();
                time = double.Parse(input);
                Console.Write("Enter angle:");
                input = Console.ReadLine();
                angle = double.Parse(input);
                Console.Write("Enter velocity:");
                input = Console.ReadLine();
                velocity = double.Parse(input);
                Console.Write("Enter weight:");
                input = Console.ReadLine();
                weight = double.Parse(input);
                Console.Write("Enter starting point in Ox:");
                input = Console.ReadLine();
                x = double.Parse(input);
                Console.Write("Enter starting point in Oy:");
                input = Console.ReadLine();
                y = double.Parse(input);
                motion M = new motion(velocity, x, y, deltatime, weight, angle, subintervals, time);
                Console.WriteLine("Launch velocity: {0}", velocity);
                Console.WriteLine("Launch starting point in Ox: {0}", x);
                Console.WriteLine("Launch staring point in Oy: {0}", y);
                Console.WriteLine("Launch delta-time: {0}", deltatime);
                Console.WriteLine("Launch weight: {0}", weight);
                Console.WriteLine("Launch angle: {0}", angle);
                Console.WriteLine("Launch subintervals: {0}", subintervals);
                Console.WriteLine("Launch time: {0}", time);
                Console.WriteLine("Function influence of wind: sin(10t)");
                Console.WriteLine("Horizontal distance:{0}", M.horizontaldistance[subintervals]);
                Console.WriteLine("Vertical distance:{0}", M.verticaldistance[subintervals]);
                Console.WriteLine("Launch velocity in Ox: {0}", M.velocityOx[subintervals]);
                Console.WriteLine("Launch velocity in Oy: {0}", M.velocityOy[subintervals]);
            }
        }
    }
}
