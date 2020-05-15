using System;       // contain fundamental classes and base classes that define commonly-used value and reference data types
using System.Globalization;   // contain classes that define culture-related information
using System.Windows;   // provide several important WPF base element classes, various classes that support the WPF property system
using System.Windows.Controls; // provide classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Media;  // provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
namespace Petzold.XamlCruncher
{
    class XamlOrientationMenuItem : MenuItem
    {
        MenuItem itemChecked;   // create item check
        Grid grid;              // create grid
        TextBox txtbox;         // create box of text
        Frame frame;            // create frame
        // Orientation public property of type Dock.   
        public Dock Orientation         {
            set             {
                foreach (MenuItem item in Items)                        // with a foreach loop, pass the array of item values directly to the Items, property of the MenuItem
                    if (item.IsChecked = (value ==  (Dock)item.Tag))   
                        itemChecked = item;             }               // if value of IsChecked equal to Tag's, then assign item to Checked item
            get             {
                return (Dock)itemChecked.Tag;               // return to Tag of item Checked
            }         }
        // Constructor requires three arguments.    
        public XamlOrientationMenuItem(Grid grid,  TextBox txtbox, Frame frame)         {
            this.grid = grid;                              // refer to grid
            this.txtbox = txtbox;                          // refer to textbox
            this.frame = frame;                            // refer to frame
            Header = "_Orientation";                       // content of header 
            for (int i = 0; i < 4; i++)
                Items.Add(CreateItem((Dock)i));             // add method of the item property
            (itemChecked = (MenuItem) Items[0]) .IsChecked = true;         }
        // Create each menu item based on Dock  setting.         
        MenuItem CreateItem(Dock dock)         {
            MenuItem item = new MenuItem();                 // create menu item
            item.Tag = dock;                                // assign Tag item to dock
            item.Click += ItemOnClick;                      // occur when the item is clicked
            item.Checked += ItemOnCheck;                    // enable overflow checking for operations 
            // Two text strings that appear in  menu item.  
            FormattedText formtxt1 =  CreateFormattedText("Edit");  // create format text with content "edit"
            FormattedText formtxt2 =  CreateFormattedText("Display"); // create format text with content "display"
            double widthMax = Math.Max(formtxt1 .Width, formtxt2.Width);  // assign maximal width by compare width between format text 1 and format text 2
            // Create a DrawingVisual and a  DrawingContext.            
            DrawingVisual vis = new DrawingVisual();         // create a DrawingVisual 
            DrawingContext dc = vis.RenderOpen();            // create a DrawingContext
            // Draw boxed text on the visual.            
            switch (dock)             {
                case Dock.Left:
                    // Edit on  left, display on right.           
                    BoxText(dc, formtxt1, formtxt1 .Width, new Point(0, 0));
                    BoxText(dc, formtxt2, formtxt2 .Width, new Point(formtxt1 .Width + 4, 0));
                    break;
                case Dock.Top:
                    // Edit on  top, display on bottom.        
                    BoxText(dc, formtxt1, widthMax , new Point(0, 0));
                    BoxText(dc, formtxt2, widthMax, new Point(0, formtxt1 .Height + 4));
                    break;
                case Dock.Right:
                    // Edit on  right, display on left.
                    BoxText(dc, formtxt2, formtxt2 .Width, new Point(0, 0));
                    BoxText(dc, formtxt1, formtxt1 .Width, new Point(formtxt2 .Width + 4, 0));
                    break;
                case Dock.Bottom:
                    // Edit on  bottom, display on top. 
                    BoxText(dc, formtxt2, widthMax , new Point(0, 0));
                    BoxText(dc, formtxt1, widthMax, new Point(0, formtxt2 .Height + 4));
                    break;             }
            dc.Close();
            // Create Image object based on  Drawing from visual.      
            DrawingImage drawimg = new  DrawingImage(vis.Drawing);
            Image img = new Image();
            img.Source = drawimg;               // take source of image
            // Set the Header of the menu item to  the Image object.         
            item.Header = img;
            return item;         }
        // Handles the hairy FormattedText arguments.      
        FormattedText CreateFormattedText(string str)         {
            return new FormattedText(str,  CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(SystemFonts .MenuFontFamily,
                SystemFonts.MenuFontStyle,SystemFonts .MenuFontWeight, FontStretches.Normal),  SystemFonts.MenuFontSize,  SystemColors.MenuTextBrush);         }
        // Draws text surrounded by a rectangle.       
        void BoxText(DrawingContext dc,  FormattedText formtxt, double  width, Point pt)         {
            Pen pen = new Pen(SystemColors .MenuTextBrush, 1);              // create pen with the menu text brush and thickness equal to 1
            dc.DrawRectangle(null, pen, new Rect(pt.X, pt.Y, width + 4,  formtxt.Height + 4)); // draw a rectangle with pen
            double X = pt.X + (width - formtxt .Width) / 2;                 // represent a double number
            dc.DrawText(formtxt, new Point(X + 2,  pt.Y + 2));         }    // draw format text at the specified location
        // Check and uncheck items when clicked.        
        void ItemOnClick(object sender,  RoutedEventArgs args)         {    
            itemChecked.IsChecked = false;                                  // uncheck items when clicked
            itemChecked = args.Source as MenuItem;                          // get parameter of Source as MenuItem
            itemChecked.IsChecked = true;         }                         // check items when clicked
        // Change the orientation based on the  checked item. 
        void ItemOnCheck(object sender,  RoutedEventArgs args)         {
            MenuItem itemChecked = args.Source as  MenuItem;                
            // Initialize the 2nd and 3rd rows and  columns to zero.       
            for (int i = 1; i < 3; i++)             {
                grid.RowDefinitions[i].Height =  new GridLength(0);           // initialize the 2nd and 3rd rows to zero
                grid.ColumnDefinitions[i].Width =  new GridLength(0);   }     // initialize the 2nd and 3rd columns to zero
            // Initialize the cell of the TextBox  and Frame to zero.          
            Grid.SetRow(txtbox, 0);             // initialize row of the TextBox to zero 
            Grid.SetColumn(txtbox, 0);          // initialize column of the TextBox to zero 
            Grid.SetRow(frame, 0);              // initialize row of the Frame to zero
            Grid.SetColumn(frame, 0);           // initialize column of the Frame to zero 
            // Set row and columns based on the  orientation setting.  
            switch ((Dock)itemChecked.Tag)             {
                case Dock.Left:
                    //  Edit on left, display on right.     
                    grid.ColumnDefinitions[1] .Width = GridLength.Auto;
                    grid.ColumnDefinitions[2].Width = new GridLength(100 , GridUnitType.Star);
                    Grid.SetColumn(frame, 2);
                    break;               
                        case Dock.Top:
                    //  Edit on top, display on bottom.      
                    grid.RowDefinitions[1].Height  = GridLength.Auto;
                    grid.RowDefinitions[2].Height = new GridLength(100 , GridUnitType.Star);
                    Grid.SetRow(frame, 2);
                    break;
                case Dock.Right:
                    //  Edit on right, display on left.       
                    grid.ColumnDefinitions[1] .Width = GridLength.Auto;
                    grid.ColumnDefinitions[2].Width = new GridLength(100 , GridUnitType.Star);
                    Grid.SetColumn(txtbox, 2);
                    break;
                case Dock.Bottom:
                    //  Edit on bottom, display on top.   
                    grid.RowDefinitions[1].Height  = GridLength.Auto;
                    grid.RowDefinitions[2].Height =  new GridLength(100 , GridUnitType.Star);
                    Grid.SetRow(txtbox, 2);
                    break;             }         }     } } 
