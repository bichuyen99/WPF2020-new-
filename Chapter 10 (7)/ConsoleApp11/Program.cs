using System;        /*The System namespace contains fundamental classes and base classes that define commonly-used value 
                and reference data types, events and event handlers, interfaces, attributes, and processing exceptions.*/
using System.Globalization; // Contains classes that define culture-related information and sort order for strings. 
using System.Windows;   // Provides several important WPF base element classes, various classes that support the WPF property system
using System.Windows.Controls; // Provides classes to create elements, known as controls, that enable a user to interact with an application. 
using System.Windows.Input;    // Provides types to support the Windows Presentation Foundation (WPF) input system.
using System.Windows.Media;    // Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications. 
namespace Petzold.GetMedieval
{
    public class MedievalButton : Control
    {         // Just two private fields.   
        FormattedText formtxt;
        bool isMouseReallyOver;
        // Static readonly fields.     
        public static readonly DependencyProperty  TextProperty;
        public static readonly RoutedEvent KnockEvent;
        public static readonly RoutedEvent  PreviewKnockEvent;
        // Static constructor.     
        static MedievalButton()         {
            // Register dependency property.    
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof (MedievalButton), new FrameworkPropertyMetadata(" ", FrameworkPropertyMetadataOptions.AffectsMeasure));
            // Register routed events.        
            KnockEvent = EventManager.RegisterRoutedEvent ("Knock", RoutingStrategy.Bubble, typeof(RoutedEventHandler),  typeof(MedievalButton));
            PreviewKnockEvent = EventManager.RegisterRoutedEvent ("PreviewKnock", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(MedievalButton));     }
        // Public interface to dependency property.  
        public string Text     {
            set { SetValue(TextProperty, value == null  ? " " : value); }
            get { return (string)GetValue(TextProperty); }     }
        // Public interface to routed events.   
        public event RoutedEventHandler Knock     {
            add { AddHandler(KnockEvent, value); }
            remove { RemoveHandler(KnockEvent, value); }     }
        public event RoutedEventHandler PreviewKnock     {
            add { AddHandler(PreviewKnockEvent, value); }
            remove { RemoveHandler(PreviewKnockEvent,  value); }     }
        // MeasureOverride called whenever the size of  the button might change. 
        protected override Size MeasureOverride(Size  sizeAvailable)        {  
            formtxt = new FormattedText( Text, CultureInfo.CurrentCulture,  FlowDirection, new Typeface(FontFamily, FontStyle , FontWeight, FontStretch),FontSize, Foreground);
            // Take account of Padding when  calculating the size.       
            Size sizeDesired = new Size(Math.Max(48,  formtxt.Width) + 4, formtxt.Height + 4); // calculates a desired size of the button to prevent a short text string from resulting in a tiny button
            sizeDesired.Width += Padding.Left +  Padding.Right;
            sizeDesired.Height += Padding.Top +  Padding.Bottom; return sizeDesired;     }
        // OnRender called to redraw the button.  
        protected override void OnRender (DrawingContext dc)     {
            // Determine background color. 
            Brush brushBackground = SystemColors .ControlBrush;
            if (isMouseReallyOver && IsMouseCaptured)
                brushBackground = SystemColors .ControlDarkBrush;       // allow OnRender to correctly color the button background
            // Determine pen width.  
            Pen pen = new Pen(Foreground,  IsMouseOver ? 2 : 1);
            // Draw filled rounded rectangle.        
            dc.DrawRoundedRectangle (brushBackground, pen,new Rect(new  Point(0, 0), RenderSize), 4, 4); // draw the button border and background
            // Determine foreground color.       
            formtxt.SetForegroundBrush( IsEnabled ? Foreground :  SystemColors.ControlDarkBrush);  // The text normally has a color based on the Foreground property defined by Control
            // Determine start point of text.       
            Point ptText = new Point(2, 2);                 // initialized to the point (2, 2) to allow room for the border drawn by the Rectangle call.
            switch (HorizontalContentAlignment)            {            // calculate a starting position of the text based on HorizontalContentAlignment. 
                case HorizontalAlignment.Left: ptText.X += Padding.Left;
                    break;
                case HorizontalAlignment.Right: ptText.X += RenderSize.Width -  formtxt.Width - Padding.Right;
                    break;
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch: ptText.X += (RenderSize.Width  - formtxt.Width - Padding.Left - Padding .Right) / 2;
                    break;             }
            switch (VerticalContentAlignment)   // calculate a starting position of the text based on VerticalContentAlignment
            {
                case VerticalAlignment.Top: ptText.Y += Padding.Top;
                    break;
                case VerticalAlignment.Bottom: ptText.Y += RenderSize.Height -  formtxt.Height - Padding.Bottom;
                    break;
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch: ptText.Y += (RenderSize.Height  - formtxt.Height - Padding.Top - Padding .Bottom) / 2;
                    break;             }
            // Draw the text.           
            dc.DrawText(formtxt, ptText);         }             //display the text inside the button
        // Mouse events that affect the visual  look of the button.   
        protected override void OnMouseEnter (MouseEventArgs args)         {
            base.OnMouseEnter(args);                
            InvalidateVisual();        }           // call to InvalidateVisual so that the button is redrawn.
        protected override void OnMouseLeave (MouseEventArgs args)         {
            base.OnMouseLeave(args);
            InvalidateVisual();        }          // call to InvalidateVisual so that the button is redrawn.
        protected override void OnMouseMove (MouseEventArgs args)         {
            base.OnMouseMove(args);
            // Determine if mouse has really moved  inside or out.     
            Point pt = args.GetPosition(this);
            bool isReallyOverNow = (pt.X >= 0 &&  pt.X < ActualWidth && pt.Y >= 0 &&  pt.Y < ActualHeight);
            if (isReallyOverNow != isMouseReallyOver)             { // determine if the mouse was positioned over the button.
                isMouseReallyOver = isReallyOverNow;
                InvalidateVisual();             }         }      // if the mouse is captured, IsMouseOver returns true regardless of the position of the mouse.
        // This is the start of how 'Knock' events  are triggered.      
        protected override void  OnMouseLeftButtonDown(MouseButtonEventArgs args)         {
            base.OnMouseLeftButtonDown(args);
            CaptureMouse();
            InvalidateVisual();     //the class captures the mouse, invalidates the appearance of the button, and sets the Handled property of MouseButtonEventArgs to true.
            args.Handled = true;         }
        // This event actually triggers the  'Knock' event.    
        protected override void  OnMouseLeftButtonUp(MouseButtonEventArgs args)         {
            base.OnMouseLeftButtonUp(args);
            if (IsMouseCaptured)             {      
                if (isMouseReallyOver)                     
                {
                    OnPreviewKnock();      // call effectively fires the PreviewKnock and Knock events in that order by calling OnPreviewKnock and OnKnock.                  
                    OnKnock();                 }
                args.Handled = true;                       
                Mouse.Capture(null);             }         }
        // If lose mouse capture (either  internally or externally), redraw.  
        protected override void OnLostMouseCapture (MouseEventArgs args)
        {
            base.OnLostMouseCapture(args);
            InvalidateVisual();         }
        // The keyboard Space key or Enter also  triggers the button.        
        protected override void OnKeyDown (KeyEventArgs args)       
        {
            base.OnKeyDown(args);
            if (args.Key == Key.Space || args.Key  == Key.Enter)
                args.Handled = true;         }
        protected override void OnKeyUp (KeyEventArgs args)
        {
            base.OnKeyUp(args);
            if (args.Key == Key.Space || args.Key  == Key.Enter)
            {
                OnPreviewKnock();
                OnKnock();
                args.Handled = true;             }         }
        // OnKnock method raises the 'Knock' event.    
        protected virtual void OnKnock()         {
            RoutedEventArgs argsEvent = new  RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton .PreviewKnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);         }
        // OnPreviewKnock method raises the  'PreviewKnock' event.  
        protected virtual void OnPreviewKnock()         {
            RoutedEventArgs argsEvent = new  RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton .KnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }     }
}

