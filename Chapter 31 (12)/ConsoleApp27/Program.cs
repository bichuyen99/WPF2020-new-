using System; // contain fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows; // provide several important WPF base element classes, various classes that support the WPF property system 
using System.Windows.Controls; // provide classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; // provide types to support the WPF input system
using System.Windows.Media; // provide types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
using System.Windows.Media.Imaging; // provide types that are used to encode and decode bitmap images
namespace Petzold.DrawGraphicsOnBitmap { public class DrawGraphicsOnBitmap : Window {
        [STAThread]
        public static void Main() {
            Application app = new Application();      // create a WPF application
            app.Run(new DrawGraphicsOnBitmap()); }    // start that app and open DrawGraphicsOnBitmap window
        public DrawGraphicsOnBitmap() {
            Title = "Draw Graphics on Bitmap";        // content of the title 
            // Set background to demonstrate  transparency of bitmap.         
            Background = Brushes.Khaki;               // set color of background
            // Create the RenderTargetBitmap object.        
            RenderTargetBitmap renderbitmap = new RenderTargetBitmap(100, 100,  96, 96, PixelFormats.Default); // convert a visual object into a bitmap 
            // Create a DrawingVisual object.          
            DrawingVisual drawvis = new  DrawingVisual();  // to render vector graphics on the screen
            DrawingContext dc = drawvis.RenderOpen();      // describe visual content by using command, which open the DrawingVisual object for rendering
            dc.DrawRoundedRectangle(Brushes.Blue,  new Pen(Brushes.Red, 10),new Rect(25,  25, 50, 50), 10, 10); // draw a rounded rectangle with specified brush and pen
            dc.Close();         // close and flush the DrawingContent
            // Render the DrawingVisual on the  RenderTargetBitmap.   
            renderbitmap.Render(drawvis);
            // Create an Image object and set its  Source to the bitmap.        
            Image img = new Image();        // create an Image object  
            img.Source = renderbitmap;      // set Image object's Source to the bitmap
            // Make the Image object the content  of the window.        
            Content = img;         }     } } 