// load a XAML file from disk.
using Microsoft.Win32; // provides two types of classes: those that handle events raised by the operating system and those that manipulate the system registry.
using System;   // contains fundamental classes and base classes that define commonly-used value and reference
using System.IO; // contains types that allow reading and writing to files and data streams, and types that provide basic file and directory support.
using System.Windows; //provides several important WPF base element classes, various classes that support the WPF property system 
using System.Windows.Controls; // provides classes to create elements, known as controls, that enable a user to interact with an application.
using System.Windows.Markup; // provides types to support XAML. 
using System.Xml; // provides standards-based support for processing XML.
namespace Petzold.LoadXamlFile
{
    public class LoadXamlFile : Window
    {
        Frame frame;[STAThread] public static void Main() { Application app = new Application(); app.Run(new LoadXamlFile()); }
        public LoadXamlFile()
        {
            Title = "Load XAML File";
            DockPanel dock = new DockPanel();
            Content = dock;
            // Create button for Open File dialog.            
            Button btn = new Button();
            btn.Content = "Open File...";     // create content of the button.
            btn.Margin = new Thickness(12);  // set spacing between elements.
            btn.HorizontalAlignment =  HorizontalAlignment.Left; // set position of the button (left).
            btn.Click += ButtonOnClick;         // click on button.
            dock.Children.Add(btn);  // add children button to the dock panel.
            DockPanel.SetDock(btn, Dock.Top);  //  dock of elements to the top of the panel.
            // Create Frame for hosting loaded XAML.   
            frame = new Frame();
            dock.Children.Add(frame);         }
        void ButtonOnClick(object sender,  RoutedEventArgs args)         {
            OpenFileDialog dlg = new OpenFileDialog();      // in the ButtonOnClick method, getting a file name from OpenFileDialog
            dlg.Filter = "XAML Files (*.xaml)|* .xaml|All files (*.*)|*.*"; 
            if ((bool)dlg.ShowDialog())
            {
                try
                {
                    // Read file with XmlTextReader.       
                    XmlTextReader xmlreader = new  XmlTextReader(dlg.FileName); // The file name can be passed directly to the XmlTextReader constructor, 
                    // Convert XAML to object.      
                    object obj = XamlReader.Load (xmlreader);                   // and that object is accepted by XamlReader.Load.
                    // If it's a Window, call Show.             
                    if (obj is Window)
                    {
                        Window win = obj as Window;
                        win.Owner = this;  //  It sets the Owner property of the Window object to itself and then calls Show as if the loaded window were a modeless dialog box.
                        win.Show();
                    }
                    // Otherwise, set as Content  of Frame.    
                    else
                        frame.Content = obj;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message,  Title);      // show the message box
                }             }         }     }  } 
