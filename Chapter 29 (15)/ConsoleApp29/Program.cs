using System;               // Contain fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows;       // Provide several important WPF base element classes, various classes that support the WPF property system 
using System.Windows.Controls; // Provide classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; // Provide types to support the WPF input system
using System.Windows.Media; // Provide types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
namespace Petzold.TransformedButtons
{
    public class TransformedButtons : Window
    {
        [STAThread]
        public static void Main()
        {   Application app = new Application();           // Create a WPF application
            app.Run(new TransformedButtons());    }        // Start that application and open TransformedButtons window
        public TransformedButtons()
        {
            Title = "Transformed Buttons";                 // Set title of window
            // Create Canvas as content of window.       
            Canvas canv = new Canvas();                    // Create a canvas object    
            Content = canv;                                // Set content of canvas object
            // Untransformed button.   
            Button btn = new Button();                     // Create a button
            btn.Content = "Untransformed";                 // Set content of button object
            canv.Children.Add(btn);                        // Add button's content to Canvas
            Canvas.SetLeft(btn, 50);                       // Set distance between the left side of button and the left side of its parent Canvas
            Canvas.SetTop(btn, 100);                       // Set distance between the top of button and the top of its parent Canvas
            // Translated button.            
            btn = new Button();                            // Create a button
            btn.Content = "Translated";                    // Set content of button object
            btn.RenderTransform = new  TranslateTransform(-100, 150); // Set transform information that affects the rending position of button 
            canv.Children.Add(btn);                        // Add button's content to Canvas
            Canvas.SetLeft(btn, 200);                      // Set distance between the left side of translated button and the left side of its parent Canvas
            Canvas.SetTop(btn, 100);                       // Set distance between the top of translated button and the top of its parent Canvas
            // Scaled button.            
            btn = new Button();                            // Create a button
            btn.Content = "Scaled";                        // Set content of button object
            btn.RenderTransform = new  ScaleTransform(1.5, 4); // Set transform information that affects the rending position of button 
            canv.Children.Add(btn);                        // Add button's content to Canvas
            Canvas.SetLeft(btn, 350);                      // Set distance between the left side of scaled button and the left side of its parent Canvas
            Canvas.SetTop(btn, 100);                       // Set distance between the top of scaled button and the top of its parent Canvas
            // Skewed button.            
            btn = new Button();                            // Create a button
            btn.Content = "Skewed";                        // Set content of button object
            btn.RenderTransform = new  SkewTransform(0, 20); // Set transform information that affects the rending position of button 
            canv.Children.Add(btn);                        // Add button's content to Canvas
            Canvas.SetLeft(btn, 500);                      // Set distance between the left side of skewed button and the left side of its parent Canvas
            Canvas.SetTop(btn, 100);                       // Set distance between the top of skewed button and the top of its parent Canvas
            // Rotated button.          
            btn = new Button();                            // Create a button
            btn.Content = "Rotated";                       // Set content of button object
            btn.RenderTransform = new  RotateTransform(-30); // Set transform information that affects the rending position of button 
            canv.Children.Add(btn);                        // Add button's content to Canvas
            Canvas.SetLeft(btn, 650);                      // Set distance between the left side of rotated button and the left side of its parent Canvas
            Canvas.SetTop(btn, 100);         }     }    }  // Set distance between the top of rotated button and the top of its parent Canvas

