//ImageTheButton.cs
using System;   /*The System namespace contains fundamental classes and base classes that define commonly-used value and reference data types,
                        events and event handlers, interfaces, attributes, and processing exceptions.*/
using System.Windows; //Provides several important WPF base element classes
using System.Windows.Controls; //Provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; //Provides types to support the WPF input system.
using System.Windows.Media; //Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications.
using System.Windows.Media.Imaging; //Provides types that are used to encode and decode bitmap images.
namespace Petzold.ImageTheButton {
    public class ImageTheButton : Window {
        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new ImageTheButton()); }
        public ImageTheButton() {
            Title = "Image the Button";
            Uri uri = new Uri("pack://application: ,,/munch.png");      // gain access to that file using the Uri constructor                                                                    
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmap;                                        // the image has actually been embedded into the executable file as a resource
            img.Stretch = Stretch.None;
            Button btn = new Button();                                  // create the Button and set as window content 
            btn.Content = img;
            btn.HorizontalAlignment = HorizontalAlignment.Center;       // defined the positioning of the button's content within the button
            btn.VerticalAlignment = VerticalAlignment.Center; Content = btn; } } }
