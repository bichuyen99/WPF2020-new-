using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp4;

namespace Play
{

    public partial class Window1 : Window
    {
        private DispatcherTimer t = new DispatcherTimer();
        private double time = 0;
        double g = 9.8;
        double angle;
        double v0;
        // position of bird
        double x0;
        double y0;
        double a;
        double Hmax;
        double sumt;
        double xmax;
        // position of birdnest
        double x1;          
        double y1;
        double thight;
        Random r = new Random();

        private void Data(out double v0, out double angle)
        {
            v0 = Convert.ToDouble(v0textbox.Text);
            angle = Convert.ToDouble(angletextbox.Text);
        }

        public Window1()
        {
            InitializeComponent();
            Play();
            check();
            result();
         }
        private void Play ()
        {
            Button play = new Button();
        }
        private void play (object sender, RoutedEventArgs e)
        {
            SoundPlayerAction sndprcs = new SoundPlayerAction();
            sndprcs.Source = new Uri(@"slow-down-sound-effect.mp3");
            t.Tick += new EventHandler(OnTimer);
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
            t.Start();
        }

        private void menu (object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Visibility = Visibility.Hidden;
            main.playbtn.IsEnabled = false;
            main.instrubtn.IsEnabled = false;
            main.setbtn.IsEnabled = false;
            SoundPlayerAction sndintro = new SoundPlayerAction();
            sndintro.Source = new Uri(@"Birds-chirping-sound-effect.mp3");
        }
        void OnTimer(object sender, EventArgs e)
        {
              //Position of bird
            x0 = Canvas.GetLeft(Bird) + Bird.Width / 2;
            y0 = Canvas.GetTop(Bird) + Bird.Height / 2;
            a = angle * Math.PI / 180;
            // the hightest place
            Hmax = Math.Pow(v0 * Math.Sin(a), 2) / (2 * g) + y0;
            // time of all process
            sumt = v0 * Math.Sin(a) / g + Math.Sqrt(2 * (y0 + Hmax) / g);
            thight = v0 * Math.Sin(a) / g;
            // the farthest place
            xmax = x0 + Math.Pow(v0, 2) * Math.Sin(2 * a) / (2 * g) + v0 * Math.Cos(a) * Math.Sqrt(2 * (y0 + Hmax) / g);
            x1 = r.Next(650, 750);
            y1 = y0;

            Polyline pl = (Polyline)this.FindName("pln");
            Image bird = (Image)this.FindName("Bird");
            Image nest = (Image)this.FindName("Nest");

            time += 0.1;
            if (time <= thight)
            {
                pl.Points.Add(new Point(x0 + v0 * Math.Cos(a) * time, y0 - v0 * Math.Sin(a) * time + 0.5 * g * Math.Sqrt(time)));
            }
            else if (thight < time & time <= sumt)
            {
                pl.Points.Add(new Point(x0 + v0 * Math.Cos(a) * time, y0 - 0.5 * g * Math.Sqrt(time)));
            }
            else
            {
                t.Stop();
            }

            Canvas.SetLeft(Bird, pl.Points.Last().X);
            Canvas.SetTop(Bird, pl.Points.Last().Y);
        }
        private void check()
        {
            if (xmax == x1)
            {
                TextBlock win = new TextBlock();
                win.Text = "YOU WIN";
                win.MaxHeight = 150;
                win.MaxWidth = 150;
                win.Margin = new Thickness(280);
                SoundPlayerAction sndwin = new SoundPlayerAction();
                sndwin.Source = new Uri(@"Tada-sound.mp3");
            }
            else
            {
                TextBlock lose = new TextBlock();
                lose.Text = "YOU LOSE";
                lose.MaxHeight = 150;
                lose.MaxWidth = 150;
                lose.Margin = new Thickness(280);
                SoundPlayerAction sndlos = new SoundPlayerAction();
                sndlos.Source = new Uri(@"game-over-piano-sound-effect.mp3");
            }
        }
        private void result ()
        {
            Button replay = new Button();
            replay.Content = "REPLAY";
            replay.MaxHeight = 100;
            replay.MaxWidth = 100;
            replay.Margin = new Thickness(280);
        }
        private void replay(object sender, RoutedEventArgs e)
        {
            time = 0;
            t.Tick += new EventHandler(OnTimer);
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
            t.Start();
            SoundPlayerAction sndprcs = new SoundPlayerAction();
            sndprcs.Source = new Uri(@"slow-down-sound-effect.mp3");
        }
    }
        
    }


//
//
//