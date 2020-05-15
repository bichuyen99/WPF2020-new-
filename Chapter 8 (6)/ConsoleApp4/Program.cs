using System;                   // contains fundamental classes and base classes that define commonly-used value and reference data types
using System.Text;              // contains classes that represent ASCII and Unicode character encodings
using System.Windows;           // provides several important WPF base element classes
using System.Windows.Controls;  // provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input;     // provides types to support the WPF input system
using System.Windows.Media;     // provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
namespace Petzold.SetSpaceProperty
{
    public class SpaceButton : Button
    {         // A traditional .NET private field and  public property.  
        string txt;                         // creates string text
        public string Text         {
            set             {
                txt = value;                // sets value of text
                Content = SpaceOutText(txt); // calls the SpaceOutText method to insert spaces in the text and then sets the Content property from that
            } 
            get             {
                return txt;             }         } // return to text
        // A DependencyProperty and public property.  
        public static readonly DependencyProperty  SpaceProperty;
        public int Space         {
            set             {
                SetValue(SpaceProperty, value);             } // sets value of SpaceProperty
            get             {
                return (int)GetValue(SpaceProperty);          // returns the current effective value of SpaceProperty
            }         }
        // Static constructor.   
        static SpaceButton()         {
            // Define the metadata.        
            FrameworkPropertyMetadata metadata =  new FrameworkPropertyMetadata();  // creates metadata object
            metadata.DefaultValue = 1;                        // sets default value 
            metadata.AffectsMeasure = true;                   // sets Metadata property potentially affects measure pass during layout engine operation 
            metadata.Inherits = true;                         // sets value of Metadata property is inheritable
            metadata.PropertyChangedCallback +=  OnSpacePropertyChanged; // sets a reference to a PropertyChangedCallback implementation specified in this metadata
            // Register the DependencyProperty.           
            SpaceProperty =  DependencyProperty.Register ("Space", typeof(int), typeof (SpaceButton), metadata, ValidateSpaceValue);
        }         // Callback method for value validation.    
        static bool ValidateSpaceValue(object obj)         {
            int i = (int)obj;                       // The ValidateSpaceValue property returns true if the value is acceptable and returns false for negative values.
            return i >= 0;         }
        // Callback method for property changed.    
        static void OnSpacePropertyChanged (DependencyObject obj, DependencyPropertyChangedEventArgs args) // The OnSpacePropertyChanged method will be called whenever the property changes
        {
            SpaceButton btn = obj as SpaceButton;           // cast the first argument to a SpaceButton and use that to reference everything in the object it needs
            btn.Content = btn.SpaceOutText(btn.txt);  }     // sets the button Content property from the return value of SpaceOutText
        // Method to insert spaces in the text.        
        string SpaceOutText(string str)         {
            if (str == null)                               // if value of string is null, then return to null
                return null;
            StringBuilder build = new StringBuilder();     // creates build of StringBuilder class
            foreach (char ch in str)
                build.Append(ch + new string(' ',  Space));  // refers to the Space property to obtain the number of desired spaces
            return build.ToString();         }     } }
