using System;       // Contains fundamental classes and base classes that define commonly-used value and reference data types
using System.Reflection; //Contains types that retrieve information about assemblies, modules, members, parameters, and other entities in managed code by examining their metadata.
using System.Windows; // Provides several important WPF base element classes, various classes that support the WPF property system
using System.Windows.Controls; // Provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; // Provides types to support the WPF input system. 
using System.Windows.Media; // Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications.
using System.Windows.Shapes;  // Provides access to a library of shapes that can be used in XAML or code.
namespace Petzold.ListColorShapes {
    class ListColorShapes : Window {
        [STAThread]
        public static void Main() {
            Application app = new Application();            // creates new application
            app.Run(new ListColorShapes()); }
        public ListColorShapes() { Title = "List Color Shapes";
            // Create ListBox as content of window.           
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;                             // Width of box is 150
            lstbox.Height = 150;                            // Height of box is 150
            lstbox.SelectionChanged +=  ListBoxOnSelectionChanged; // Changes selection
            Content = lstbox;                               // Adds content into listbox
            // Fill ListBox with Ellipse objects.      
            PropertyInfo[] props = typeof(Brushes).GetProperties();  // Assigns props by properties of brushes  
            foreach (PropertyInfo prop in props)            // With a foreach loop, pass the array of prop values directly to the props, property of the PropertyInfo
            {
                Ellipse ellip = new Ellipse();              // Creates ellipse with width 100, height 25, margin 10 5 0 5
                ellip.Width = 100;
                ellip.Height = 25;
                ellip.Margin = new Thickness(10, 5 , 0, 5);
                ellip.Fill = prop.GetValue(null,  null) as Brush;   // Fills ellipse with brush
                lstbox.Items.Add(ellip);             }         }    // Adds ellipse into listbox
        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args) // Fills the ListBox with ellipses, that are painted by color and sets a handler for the SelectionChanged event
        {
            ListBox lstbox = sender as ListBox;                 
            if (lstbox.SelectedIndex != -1)                         // If index aren't equal -1, then fills background with color, that is selected
                Background = (lstbox.SelectedItem  as Shape).Fill;         }     } } 