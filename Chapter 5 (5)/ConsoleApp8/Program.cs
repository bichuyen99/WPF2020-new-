//sets the Content property of its window to a StackPanel and then creates 10 buttons that become children of the panel
using System;   /*The System namespace contains fundamental classes and base classes that define commonly-used value and reference data types,
                        events and event handlers, interfaces, attributes, and processing exceptions.*/
using System.Windows; //Provides several important WPF base element classes
using System.Windows.Controls; //Provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; //Provides types to support the WPF input system.
using System.Windows.Media; //Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications.
namespace Petzold.StackTenButtons
{
    class StackTenButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new StackTenButtons());
        }
        public StackTenButtons()
        {
            Title = "Stack Ten Buttons";
            StackPanel stack = new StackPanel();
            Content = stack;                                // sets the Content property of its window to a StackPanel
            Random rand = new Random();                     // creates an object of type Random
            for (int i = 0; i < 10; i++)                    // creates 10 buttons that become children of the panel
            {
                Button btn = new Button();
                btn.Name = ((char)('A' + i)).ToString();    
                btn.FontSize += rand.Next(10);              // increases the FontSize property of each button with a small random number
                btn.Content = "Button " + btn.Name + " says 'Click me'";        // creates content
                btn.Click += ButtonOnClick;                 
                stack.Children.Add(btn);                    // Each button is added to the Children collection of the StackPanel
            }
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;     /* The source of the Click event obtained from the Source property of RoutedEventArgs is the object 
                                                            to which the event handler is attached and which is obtained from the sender argument.*/
            MessageBox.Show("Button " + btn.Name + " has been clicked", "Button Click");  // show message box
        }
    }
}

