using Play;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp4;

namespace Set
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
             InitializeComponent();
         }
            // background
            private void btnbgr(object sender, RoutedEventArgs e)
            {
                Image morning = new Image();
                Image afternoon = new Image();
                Image defu = new Image();
                morning.Width = 150;
                morning.Height = 150;
                afternoon.Width = 150;
                afternoon.Height = 150;
                defu.Width = 150;
                defu.Height = 150;
                morning.Source = new BitmapImage(new Uri(@"landscape.jpg"));
                afternoon.Source = new BitmapImage(new Uri(@"landscape2.jpg"));
                defu.Source = new BitmapImage(new Uri(@"dong co 3.jpg"));

                DockPanel bckgrd = new DockPanel();
                bckgrd.Margin = new Thickness(25,100,0,0);
                bckgrd.Children.Add(morning);

                DockPanel bckgrd1 = new DockPanel();
                bckgrd1.Margin = new Thickness(25, 100, 0, 0);
                bckgrd1.Children.Add(afternoon);

                DockPanel bckgrd2 = new DockPanel();
                bckgrd2.Margin = new Thickness(25, 100, 0, 0);
                bckgrd2.Children.Add(defu);

                Button btn = new Button();
                btn.Content = bckgrd;
                MyCanvas.Children.Add(btn);

                Button btn1 = new Button();
                btn1.Content = bckgrd1;
                MyCanvas.Children.Add(btn1);

                Button btn2 = new Button();
                btn2.Content = bckgrd;
                MyCanvas.Children.Add(btn2);

            }
            private void btn(object sender, RoutedEventArgs e)
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource = new BitmapImage(new Uri(@"landscape.jpg"));
                MyCanvas.Background = myBrush;
                Window1 background1 = new Window1();
                background1.MyCanvas.Background = MyCanvas.Background;
                MainWindow mainbckgrnd = new MainWindow();
                mainbckgrnd.MyCanvas.Background = MyCanvas.Background;
        }
            private void btn1(object sender, RoutedEventArgs e)
            {
                ImageBrush myBrush1 = new ImageBrush();
                myBrush1.ImageSource = new BitmapImage(new Uri(@"landscape2.jpg"));
                MyCanvas.Background = myBrush1;
                Window1 background1 = new Window1();
                background1.MyCanvas.Background = MyCanvas.Background;
                MainWindow mainbckgrnd = new MainWindow();
                mainbckgrnd.MyCanvas.Background = MyCanvas.Background;
        }
            private void btn2(object sender, RoutedEventArgs e)
            {
                ImageBrush myBrush2 = new ImageBrush();
                myBrush2.ImageSource = new BitmapImage(new Uri(@"dong co 3.jpg"));
                MyCanvas.Background = myBrush2;
                Window1 background1 = new Window1();
                background1.MyCanvas.Background = MyCanvas.Background;
                MainWindow mainbckgrnd = new MainWindow();
                mainbckgrnd.MyCanvas.Background = MyCanvas.Background;
        }
            // Character
            private void btnchr(object sender, RoutedEventArgs e)
            {
                Image redg = new Image();
                Image redb = new Image();
                Image yellow = new Image();
                redg.Width = 100;
                redg.Height = 100;
                redb.Width = 100;
                redb.Height = 100;
                yellow.Width = 100;
                yellow.Height = 100;
                redg.Source = new BitmapImage(new Uri(@"birdg.png"));
                redb.Source = new BitmapImage(new Uri(@"birdb.png"));
                yellow.Source = new BitmapImage(new Uri(@"birdy.png"));

                DockPanel birdgirl = new DockPanel();
                birdgirl.Margin = new Thickness(25, 100, 0, 0);
                birdgirl.Children.Add(redg);

                DockPanel birdboy = new DockPanel();
                birdboy.Margin = new Thickness(25, 100, 0, 0);
                birdboy.Children.Add(redb);

                DockPanel birdyell = new DockPanel();
                birdyell.Margin = new Thickness(25, 100, 0, 0);
                birdyell.Children.Add(yellow);

                Button btng = new Button();
                btng.Content = birdgirl;
                MyCanvas.Children.Add(btng);

                Button btnb = new Button();
                btnb.Content = birdboy;
                MyCanvas.Children.Add(btnb);

                Button btny = new Button();
                btny.Content = birdyell;
                MyCanvas.Children.Add(btny);

            }

            private void btng(object sender, RoutedEventArgs e)
            {
                Window1 newwindow1 = new Window1();
                newwindow1.Bird.Source = new BitmapImage(new Uri(@"birdg.png"));
            }
            private void btnb(object sender, RoutedEventArgs e)
            {
                Window1 newwindow1 = new Window1();
                newwindow1.Bird.Source = new BitmapImage(new Uri(@"birdb.png"));
            }
            private void btny(object sender, RoutedEventArgs e)
            {
                 Window1 newwindow1 = new Window1();
                 newwindow1.Bird.Source = new BitmapImage(new Uri(@"birdy.png"));
             }
        // Sound
        private void btnsnd (object sender, RoutedEventArgs e)
        {
            Image on = new Image();
            on.Source = new BitmapImage(new Uri(@"volume.png"));
            Image off = new Image();
            off.Source = new BitmapImage(new Uri(@"mute.png"));

            DockPanel vol = new DockPanel();
            vol.Margin = new Thickness(200, 150, 0, 0);
            vol.Children.Add(on);

            DockPanel mut = new DockPanel();
            mut.Margin = new Thickness(25, 150, 0, 0);
            mut.Children.Add(off);

            Button btnvol = new Button();
            btnvol.Content = on;
            MyCanvas.Children.Add(btnvol);

            Button btnmut = new Button();
            btnmut.Content = off;
            MyCanvas.Children.Add(btnmut);
            }
        private void btnOn (object sender, RoutedEventArgs e)
        {
            System.Media.SoundPlayer sdon = new System.Media.SoundPlayer();
            sdon.Play();
        }
        private void btnOff(object sender, RoutedEventArgs e)
        {
            System.Media.SoundPlayer sdoff = new System.Media.SoundPlayer();
            sdoff.Stop();
        }
    }
    }

