using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double g = 9.8;
        double ymin;
        double angle=45;
        double v0=80;
        double x0;
        double y0;
        double a;
        double Hmax;
        double sumt;
        double xmax;
        double time;
        double x1;
        double y1;
        double thight;
        Random r = new Random();

        public DispatcherTimer t = new DispatcherTimer();
        

        private void Data(out double v0, out double angle)
        {
            v0 = Convert.ToDouble(v0textbox.Text);
            angle = Convert.ToDouble(angletextbox.Text);
        }
         
        public MainWindow()
        {
            check();            
            t.Tick += new EventHandler(OnTimer);
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
        }

        private void play(object sender, RoutedEventArgs e)
        {
            v0 = Convert.ToDouble(v0textbox.Text);
            angle = Convert.ToDouble(angletextbox.Text);

            // Canvas dimensions
            Canvas cnv = (Canvas)this.FindName("cnv");
            double cvh = cnv.ActualHeight;
            double cvw = cnv.ActualWidth;

            //Position of bird
            Image Bird = (Image)this.FindName("bird");
            Image Nest = (Image)this.FindName("nest");

            x0 = 120+35;
            y0 = 480+35;
            a = angle * Math.PI / 180;
            // the hightest place
            Hmax = y0 - Math.Pow(v0 * Math.Sin(a), 2) / (2 * g);
            // time of all process
            sumt = v0 * Math.Sin(a) / g + Math.Sqrt(2 * (y0 + Hmax) / g);
            thight = 2 * v0 * Math.Sin(a) / g;
            // the farthest place
            xmax = x0 + Math.Pow(v0 * Math.Sin(2 * a), 2) / g;
            ymin = y0;
            x1 = r.Next(650, 750);
            y1 = r.Next(500, 600);
         
            t.Start();
        }

        void OnTimer(object sender, EventArgs e)
        {

            //Position of bird
            Image Bird = (Image)this.FindName("bird");
            Polyline pl = (Polyline)this.FindName("pln");


            time += 0.1;
            if (time <= thight)
            {
                pl.Points.Add(new Point(x0 + v0 * Math.Cos(a) * time, y0 - v0*Math.Sin(a)*time + 0.5*g*time*time));

                Canvas.SetLeft(Bird, pl.Points.Last().X - 35);
                Canvas.SetTop(Bird, pl.Points.Last().Y - 35);
            }
           /* else if (thight < time & time <= sumt)
            {
                pl.Points.Add(new Point(x0 + v0 * Math.Cos(a) * time, y0 - 0.5* g * time*time));
            }*/
            else
            {
                t.Stop();
                check();
                pl.Points.Clear();
            }

         }
        private void check()
        {
            if (Math.Abs(xmax - x1)<=70 && Math.Abs(ymin - y1)<=70)
            {
                TextBlock win = new TextBlock();
                win.Text = "YOU WIN";
            }
            else
            {
                TextBlock lose = new TextBlock();
                lose.Text = "YOU LOSE";
            }
        }
        private void repl (object sender, RoutedEventArgs e)
        {
            Button replay = new Button();
            replay.Content = "REPLAY";
            t.Tick += new EventHandler(OnTimer);
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
            t.Start();
            t.Start();
        }
       private void exit (object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}


