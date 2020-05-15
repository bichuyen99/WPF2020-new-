using System;   // contain fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows; // provide several important WPF base element classes, various classes that support the WPF property system
using System.Windows.Controls; // provide classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; // provide types to support the WPF input system
using System.Windows.Media; // provide types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
using System.Windows.Shapes;   // provide access to a library of shapes that can be used in Extensible Application Markup Language (XAML) or code
namespace SineWave {
    public class SineWave : Window {
        [STAThread]
        public static void Main() {
            Application app = new Application();  // escapsulate a WPF application
            app.Run(new SineWave()); }          // start the application and open window SineWave
        public SineWave() {
            Title = "Sine Wave";                    // content of title
            // Make Polyline content of window.  
            Polyline poly = new Polyline();         // create a variable of Polyline class, that can draw a series of connected straight lines
            poly.VerticalAlignment =  VerticalAlignment.Center; // be aligned to the center of vertical
            poly.Stroke = SystemColors .WindowTextBrush; // set colors of the poly's outside 
            poly.StrokeThickness = 2;               // set the width of the poly's outline
            Content = poly;                         // Polyline is set as the Content property of the Window 
            // Define the points.     
            for (int i = 0; i < 2000; i++)          
                poly.Points.Add(new Point(i, 96 * (1 - Math .Sin(i * Math.PI / 192))));         }     } }
/* vertical coordinates of the sine curve are scaled to range from 0 to 192 
  If the vertical coordinates ranged from 96 to 96 (which can be accomplished by removing the 1 and the minus sign in front of Math.Sin), 
         the curve is treated as if it had a vertical dimension of 96 rather than 192*/
