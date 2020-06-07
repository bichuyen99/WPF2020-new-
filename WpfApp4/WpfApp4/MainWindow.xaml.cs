using Play;
using Set;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public void btninstru(object sender, RoutedEventArgs e)
        {
            TextBlock instruc = new TextBlock();
            instruc.Text = ("Enter velocity and angle to help the bird fly to the bird nest");
            instruc.Width = 300;
            instruc.Height = 200;
            instruc.FontSize = 20;
            instruc.Margin = new Thickness(100);
            instruc.TextWrapping = TextWrapping.Wrap;
       }
       
        private void btnset(object sender, EventArgs e)
        {
            Window2 newwindow2 = new Window2();
            this.Visibility = Visibility.Hidden;
            newwindow2.Show();
            newwindow2.Bckgr.IsEnabled = false;
            newwindow2.Chr.IsEnabled = false;
            newwindow2.Snd.IsEnabled = false;
        }
        
        private void btnplay(object sender, EventArgs e)
        {
            Window1 newwindow1 = new Window1();
            this.Visibility = Visibility.Hidden;
            newwindow1.Show();
        }
        private void gamedefault()
        {
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"dong co 3.jpg"));
            MyCanvas.Background = myBrush;
            Window1 bg1 = new Window1();
            bg1.MyCanvas.Background = MyCanvas.Background;
            Window2 bg2 = new Window2();
            bg2.MyCanvas.Background = MyCanvas.Background;
            Window1 bi1 = new Window1();
            bi1.Bird.Source = new BitmapImage(new Uri(@"birdg.png"));
            System.Media.SoundPlayer on = new System.Media.SoundPlayer();
            on.Play();
        }
        public MainWindow()
        {
            InitializeComponent();
            SoundPlayerAction sndintro = new SoundPlayerAction();
            sndintro.Source = new Uri(@"Birds-chirping-sound-effect.mp3");
            gamedefault();
        }
       
    }
}

