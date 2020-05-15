//PaintTheButton.cs
//display a blue 1.5-inch-square background with rounded corners and a yellow star one inch in diameter centered within the square.

using System;                  /*The System namespace contains fundamental classes and base classes that define commonly-used value 
                            and reference data types, events and event handlers, interfaces, attributes, and processing exceptions.*/
using System.Windows;            // Provides several important WPF base element classes
using System.Windows.Controls;  // Provides classes to create elements, known as controls, that enable a user to interact with an application.
using System.Windows.Input;     // Provides types to support the WPF input system.
using System.Windows.Media;     // Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF.
using System.Windows.Shapes;    // Provides access to a library of shapes that can be used in Extensible Application Markup Language (XAML) or code.
namespace Petzold.PaintTheButton {
    public class PaintTheButton : Window     {
        [STAThread]
        public static void Main()         {
            Application app = new Application();
            app.Run(new PaintTheButton());         }
        public PaintTheButton()         {
            Title = "Paint the Button";             
            // Create the Button as content of the  window.             
            Button btn = new Button();
            btn.HorizontalAlignment =  HorizontalAlignment.Center;      // position of element within the cell
            btn.VerticalAlignment =  VerticalAlignment.Center;
            Content = btn;                          
            // Create the Canvas as content of the  button.             
            Canvas canv = new Canvas();
            canv.Width = 144;               //creates a Canvas 1.5 inches square
            canv.Height = 144;
            btn.Content = canv;             
            // Create Rectangle as child of canvas.             
            Rectangle rect = new Rectangle();
            rect.Width = canv.Width;
            rect.Height = canv.Height;
            rect.RadiusX = 24;
            rect.RadiusY = 24;
            rect.Fill = Brushes.Blue;           // display a blue background
            canv.Children.Add(rect);            // The Rectangle is added to the Canvas children collection
            Canvas.SetLeft(rect, 0);            // positioned at the top-left corner of the Canvas
            Canvas.SetRight(rect, 0);
            // Create Polygon as child of canvas.             
            Polygon poly = new Polygon();
            poly.Fill = Brushes.Yellow;                   // display a yellow star in diameter centered within the square
            poly.Points = new PointCollection();         //store the points of the polygon
            for (int i = 0; i < 5; i++)             {   // The code in the for loop calculates the points of the star.
                double angle = i * 4 * Math.PI / 5;    // newly created Polygon object is null
                Point pt = new Point(48 * Math.Sin (angle),                                    
                    -48 * Math.Cos (angle));
                poly.Points.Add(pt);             }
            canv.Children.Add(poly);                    // The Polygon is added to the Canvas children collection
            Canvas.SetLeft(poly, canv.Width / 2);             // To center the ellipse (0,0) within the Canvas, offsets all points in the polygon.
            Canvas.SetTop(poly, canv.Height / 2);         }     } } 