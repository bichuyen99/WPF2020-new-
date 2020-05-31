using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double g = 9.8;
        double a;       //  angle (rad)
        double Hmax;    // the highest place 
        double t;       // time of all process
        double xmax;    // the farthest place
        double angle;   // angle 
        double v0;      // start velocity
        double x0;      // start point in Ox
        double y0;      // start point in Oy

        public MainWindow()
        {
            InitializeComponent();
        }
        public void Data(out double x0, out double y0, out double v0, out double angle)
        {
            x0 = Convert.ToDouble(Oxtextbox.Text);
            y0 = Convert.ToDouble(Oytextbox.Text);
            v0 = Convert.ToDouble(v0textbox.Text);
            angle = Convert.ToDouble(angletextbox.Text);
        }
        public void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Data(out x0, out y0, out v0, out angle);
            a = angle * Math.PI / 180;
            Hmax = Math.Pow(v0 * Math.Sin(a), 2) / (2 * g) + y0;
            t = v0 * Math.Sin(a) / g + Math.Sqrt(2 * (y0 + Hmax) / g);
            xmax = x0 + Math.Pow(v0, 2) * Math.Sin(2 * a) / (2 * g) + v0 * Math.Cos(a) * Math.Sqrt(2 * (y0 + Hmax) / g);
            calculate.Items.Add("Time of all process:" + String.Format("{0:0.00}", t) + " s");
            calculate.Items.Add("The highest point:" + String.Format("{0:0.00}", Hmax) + " m");
            calculate.Items.Add("The farthest point:" + String.Format("{0:0.00}", xmax) + " m");
        }
        public void ButtonOnClick1(object sender, RoutedEventArgs e)
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x0 + 40, 300 - y0);
            myPathFigure.Segments.Add( new BezierSegment(
                    new Point(x0 + 40, 300 - y0),
                    new Point(Math.Sqrt(v0) * Math.Sin(2 * a) / (2 * g) + 40, - Hmax + 300), 
                    new Point(xmax + 40, 300),
                    true ));
           
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(myPathFigure);
                      
            myPath.Data = myPathGeometry;

        }
    }
}

