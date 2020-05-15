using System;       /*The System namespace contains fundamental classes and base classes that define commonly-used value 
and reference data types, events and event handlers, interfaces, attributes, and processing exceptions.*/
using System.Windows;   /*Provides several important Windows Presentation Foundation (WPF) base element classes, 
various classes that support the WPF property system and event logic, and other types that are more broadly consumed by the WPF core and framework.*/
using System.Windows.Input; /*Provides types to support the Windows Presentation Foundation (WPF) input system. This includes device abstraction classes for mouse, keyboard, 
and stylus devices, a common input manager class, support for commanding and custom commands, and various utility classes.*/
using System.Windows.Media;  /*Provides types that enable integration of rich media, including drawings, text, and audio/video content 
in Windows Presentation Foundation (WPF) applications*/
namespace Petzold.VaryTheBackground {
    public class VaryTheBackground : Window     {
        SolidColorBrush brush = new  SolidColorBrush(Colors.Black);
        [STAThread]
        public static void Main()         {
            Application app = new Application();
            app.Run(new VaryTheBackground());         }
        public VaryTheBackground()         {
            Title = "Vary the Background";
            Width = 384;            //size of the background
            Height = 384;
            Background = brush;         }
        protected override void OnMouseMove (MouseEventArgs args)         {
            double width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;           //calculate the size of the client area
            double height = ActualHeight - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight - SystemParameters.CaptionHeight;
            Point ptMouse = args.GetPosition(this); //obtains the mouse pointer's location and saves that Point object in ptMouse
            Point ptCenter = new Point(width/2, height/2);  //distance from the center of the client area
            Vector vectMouse = ptMouse - ptCenter;  //The magnitude of vectMouse is the distance between ptCenter and ptMouse, and it's provided by the Length property of the Vector structure. 
            double angle = Math.Atan2(vectMouse.Y,  vectMouse.X); //The direction of a Vector object is represented as an angle.
            Vector vectEllipse = new Vector(width/2 * Math.Cos(angle),height/2 * Math.Sin(angle)); /*uses that angle to calculate another Vector object that represents the distance from the center
            of the client area to a point on an ellipse that fills the client area.*/
            Byte byLevel = (byte) (255 * (1 - Math .Min(1, vectMouse.Length /vectEllipse.Length))); //The level of gray is simply proportional to the ratio of the two vectors.
            Color clr = brush.Color;            /*the Color object associated with the SolidColorBrush originally created as a field of the class, sets the three primaries to the gray level, 
            and then sets the Color property of the brush to this new value.*/
            clr.R = clr.G = clr.B = byLevel;
            brush.Color = clr;         }     } } 
