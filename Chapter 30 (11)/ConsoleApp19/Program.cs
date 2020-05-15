using System;           //contains fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows;   //Provides several important WPF base element classes, various classes that support the WPF property system 
using System.Windows.Controls; // Provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input;    // Provides types to support the WPF input system
using System.Windows.Media; // Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
using System.Windows.Media.Animation;  // Provides types that support property animation functionality, including timelines, storyboards, and key frames
namespace Petzold.EnlargeButtonWithAnimation {
    public class EnlargeButtonWithAnimation : Window {
        const double initFontSize = 12;     // the button's FontSize is initially 12
        const double maxFontSize = 48;      // final value of the button is 48
        Button btn;
        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new EnlargeButtonWithAnimation()); }        // creates application, which enlarge button with animation
        public EnlargeButtonWithAnimation() {
            Title = "Enlarge Button with Animation";
            btn = new Button();                                 // creates button
            btn.Content = "Expanding Button";                   // content inside button
            btn.FontSize = initFontSize;                        // size of content's font fit with button's
            btn.HorizontalAlignment = HorizontalAlignment.Center; // sets horizontal alignment of button in center 
            btn.VerticalAlignment = VerticalAlignment.Center;     // sets vertical alignment of button in center
            btn.Click += ButtonOnClick; Content = btn; }        // clicks on the button to start
        void ButtonOnClick(object sender, RoutedEventArgs args) { 
            DoubleAnimation anima = new DoubleAnimation();         // creates animation
            anima.Duration = new Duration(TimeSpan.FromSeconds(2)); // sets time of the animation (2 seconds)
            anima.From = initFontSize;                              // set the From property isn't required in this example because the button's FontSize is initially 12 anyway. 
            anima.To = maxFontSize;                             // reaches its value of maximum (=48)
            anima.FillBehavior = FillBehavior.Stop;             // after reaching its maximum value, the animation ends.
            btn.BeginAnimation(Button.FontSizeProperty, anima); } } } /* The first argument is the dependency property being animated, 
                                                        and the second argument is the DoubleAnimation object to animate that property. */
