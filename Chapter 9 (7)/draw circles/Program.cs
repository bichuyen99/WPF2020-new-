using System;   /*The System namespace contains fundamental classes and base classes that define commonly-used value 
                and reference data types, events and event handlers, interfaces, attributes, and processing exceptions.*/
using System.Windows; // Provides several important WPF base element classes, various classes that support the WPF property system
using System.Windows.Controls; // Provides classes to create elements, known as controls, that enable a user to interact with an application. 
using System.Windows.Input; // Provides types to support the Windows Presentation Foundation (WPF) input system.
using System.Windows.Media; // Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications.
using System.Windows.Shapes; // Provides access to a library of shapes that can be used in XAML or code.
namespace Petzold.DrawCircles
{
    public class DrawCircles : Window
    {
        Canvas canv;      
        // Drawing-Related fields.    
        bool isDrawing;
        Ellipse elips;
        Point ptCenter;       
        // Dragging-Related fields.       
        bool isDragging;
        FrameworkElement elDragging;
        Point ptMouseStart, ptElementStart;
        [STAThread]
        public static void Main()         {
            Application app = new Application();
            app.Run(new DrawCircles());         }
        public DrawCircles()         {
            Title = "Draw Circles";
            Content = canv = new Canvas();      }            // The window covers its client area with a Canvas panel
        protected override void  OnMouseLeftButtonDown(MouseButtonEventArgs args)         {
            base.OnMouseLeftButtonDown(args);
            if (isDragging)
                return;
            // Create a new Ellipse object and add  it to canvas.         
            ptCenter = args.GetPosition(canv);
            elips = new Ellipse();                          // creates an Ellipse object 
            elips.Stroke = SystemColors .WindowTextBrush;
            elips.StrokeThickness = 1;
            elips.Width = 0;
            elips.Height = 0;
            canv.Children.Add(elips);                       // adds it to the Canvas child collection
            Canvas.SetLeft(elips, ptCenter.X);
            Canvas.SetTop(elips, ptCenter.Y);
            // Capture the mouse and prepare for  future events.         
            CaptureMouse();
            isDrawing = true;         }
        protected override void  OnMouseRightButtonDown(MouseButtonEventArgs args)         {
            base.OnMouseRightButtonDown(args);              // draws based on mouse input
            if (isDrawing)
                return;
            // Get the clicked element and prepare  for future events.       
            ptMouseStart = args.GetPosition(canv);          // saves the mouse position and that element's position in fields.
            elDragging = canv.InputHitTest (ptMouseStart) as FrameworkElement;      // determine which element is underneath the mouse cursor. 
            if (elDragging != null)             {
                ptElementStart = new Point(Canvas .GetLeft(elDragging), Canvas .GetTop(elDragging));
                isDragging = true;          // sets the isDrawing field to true so that all future event handlers know exactly what's going on.
            }
        }              
        protected override void OnMouseDown (MouseButtonEventArgs args)         {
            base.OnMouseDown(args);
            if (args.ChangedButton == MouseButton .Middle)             {
                Shape shape = canv.InputHitTest (args.GetPosition(canv)) as Shape;
                if (shape != null)
                    shape.Fill = (shape.Fill ==  Brushes.Red ? Brushes .Transparent : Brushes.Red); 
            }         }
        protected override void OnMouseMove (MouseEventArgs args) // original mouse click to indicate the center of the circle
        {
            base.OnMouseMove(args);
            Point ptMouse = args.GetPosition(canv);
            // Move and resize the Ellipse.           
            if (isDrawing)             {
                double dRadius = Math.Sqrt(Math .Pow(ptCenter.X - ptMouse.X, 2) + Math .Pow(ptCenter.Y - ptMouse.Y, 2));
                Canvas.SetLeft(elips, ptCenter.X -  dRadius);
                Canvas.SetTop(elips, ptCenter.Y -  dRadius);
                elips.Width = 2 * dRadius;
                elips.Height = 2 * dRadius;             }
            // Move the Ellipse.         
            else if (isDragging)             {
                Canvas.SetLeft(elDragging, ptElementStart.X + ptMouse.X -  ptMouseStart.X);
                Canvas.SetTop(elDragging, ptElementStart.Y + ptMouse.Y -  ptMouseStart.Y);             }         }
        protected override void OnMouseUp (MouseButtonEventArgs args)         {
            base.OnMouseUp(args);
            // End the drawing operation.        
            if (isDrawing && args.ChangedButton ==  MouseButton.Left)             {
                elips.Stroke = Brushes.Blue;        // When you release the left mouse button, the circle is drawn with a blue perimeter.
                elips.StrokeThickness = Math.Min (24, elips.Width / 2);
                elips.Fill = Brushes.Red;           // When you release the left mouse button, the circle is drawn with a red interior.
                isDrawing = false;
                ReleaseMouseCapture();             }
            // End the capture operation.          
            else if (isDragging && args .ChangedButton == MouseButton.Right)       // If you press the right mouse button over one of the circles, you can drag it to a new location. 
            {
                isDragging = false;             }         }
        protected override void OnTextInput (TextCompositionEventArgs args)         {
            base.OnTextInput(args);
            // End drawing or dragging with press of Escape key.   
            if (args.Text.IndexOf('\x1B') != -1)             {
                if (isDrawing)
                    ReleaseMouseCapture();
                else if (isDragging)                 {
                    Canvas.SetLeft(elDragging,  ptElementStart.X);
                    Canvas.SetTop(elDragging,  ptElementStart.Y);
                    isDragging = false;                 }             }         }
        protected override void OnLostMouseCapture (MouseEventArgs args)         {
            base.OnLostMouseCapture(args);
            // Abnormal end of drawing: Remove  child Ellipse.     
            if (isDrawing)             {
                canv.Children.Remove(elips);
                isDrawing = false;             }         }     } }


