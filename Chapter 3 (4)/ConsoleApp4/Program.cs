using System;                   // contains fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows;           // provides several important WPF base element classes
using System.Windows.Controls;  // provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input;     // provides types to support the WPF input system
using System.Windows.Media;     // provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
using System.Windows.Documents; // contains types that support FixedDocument, FlowDocument, and XML Paper Specification (XPS) document creation
namespace Petzold.FormatTheText
{
    class FormatTheText : Window
    {
        [STAThread]
        public static void Main() {
            Application app = new Application();                                // creates a WPF application
            app.Run(new FormatTheText()); }                                     // starts that application and opens FormatTheText window
        public FormatTheText()
        {
            Title = "Format the Text";                                          // sets title of window
            TextBlock txt = new TextBlock();                                    // creates a textblock
            txt.FontSize = 32;                                                  // 24  points            
            txt.Inlines.Add("This is some ");                                   // adds content of text        
            txt.Inlines.Add(new Italic(new Run ("italic")));                    // sets italic word 
            txt.Inlines.Add(" text, and this is  some ");                       // adds content of text  
            txt.Inlines.Add(new Bold(new Run ("bold")));                        // sets bold word
            txt.Inlines.Add(" text, and let's cap  it off with some ");         // adds content of text 
            txt.Inlines.Add(new Bold(new Italic (new Run("bold italic"))));     // sets italic and bold word
            txt.Inlines.Add(" text.");                                          // adds content of text
            txt.TextWrapping = TextWrapping.Wrap;                               // sets textblock wrap the text 
            Content = txt;         }     } }                                    // gets content of text
