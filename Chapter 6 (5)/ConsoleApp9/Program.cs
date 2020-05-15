using System;       /*The System namespace contains fundamental classes and base classes that define commonly-used value and reference data types,
                        events and event handlers, interfaces, attributes, and processing exceptions.*/
using System.Windows;   //Provides several important WPF base element classes
using System.Windows.Controls;  //Provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input;     //Provides types to support the WPF input system.
using System.Windows.Media;     //Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications.
namespace Petzold.CalculateYourLife
{
    class CalculateYourLife : Window
    {
        TextBox txtboxBegin, txtboxEnd;
        Label lblLifeYears;
        [STAThread]
        public static void Main() {
            Application app = new Application();
            app.Run(new CalculateYourLife()); }
        public CalculateYourLife()
        {
            Title = "Calculate Your Life";
            SizeToContent = SizeToContent.WidthAndHeight;       // auto width and height, that fit the content 
            ResizeMode = ResizeMode.CanMinimize;                
            // Create the grid.             
            Grid grid = new Grid();
            Content = grid;            
            // Row and column definitions.             
            for (int i = 0; i < 3; i++)             {
                RowDefinition rowdef = new  RowDefinition();
                rowdef.Height = GridLength.Auto;                // the static property returns an object of type GridLength.
                grid.RowDefinitions.Add(rowdef);            }  //  add RowDefinition objects to the collections for every row.
            for (int i = 0; i < 2; i++)             {
                ColumnDefinition coldef = new  ColumnDefinition();
                coldef.Width = GridLength.Auto;                 // the static property returns an object of type GridLength.
                grid.ColumnDefinitions.Add(coldef);         }  // add ColumnDefinition objects to the collections for every column     
            // First label.             
            Label lbl = new Label();
            lbl.Content = "Begin Date: ";
            grid.Children.Add(lbl);                             // enter dates
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);             
            // First TextBox.             
            txtboxBegin = new TextBox();
            txtboxBegin.Text = new DateTime(1980,  1, 1).ToShortDateString();         // converts the text strings to DateTime objects
            txtboxBegin.TextChanged +=  TextBoxOnTextChanged;
            grid.Children.Add(txtboxBegin);
            Grid.SetRow(txtboxBegin, 0);
            Grid.SetColumn(txtboxBegin, 1);             
            // Second label.             
            lbl = new Label();
            lbl.Content = "End Date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 1);
            Grid.SetColumn(lbl, 0);             
            // Second TextBox.            
            txtboxEnd = new TextBox();
            txtboxEnd.TextChanged +=  TextBoxOnTextChanged;
            grid.Children.Add(txtboxEnd);
            Grid.SetRow(txtboxEnd, 1);
            Grid.SetColumn(txtboxEnd, 1);
            // Third label.             
            lbl = new Label();
            lbl.Content = "Life Years: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 2);
            Grid.SetColumn(lbl, 0);
            // Label for calculated result.             
            lblLifeYears = new Label();
            grid.Children.Add(lblLifeYears);
            Grid.SetRow(lblLifeYears, 2);
            Grid.SetColumn(lblLifeYears, 1);
            // Set margin for everybody.            
            Thickness thick = new Thickness(5);   //  ~1/20 inch.             
            grid.Margin = thick;
            foreach (Control ctrl in grid.Children)
                ctrl.Margin = thick;
            // Set focus and trigger the event  handler.             
            txtboxBegin.Focus();
            txtboxEnd.Text = DateTime.Now .ToShortDateString();         }
        void TextBoxOnTextChanged(object sender,  TextChangedEventArgs args)         {
            DateTime dtBeg, dtEnd;
            if (DateTime.TryParse(txtboxBegin.Text , out dtBeg) &&                 
                DateTime.TryParse(txtboxEnd.Text,  out dtEnd))             {
                int iYears = dtEnd.Year - dtBeg.Year;                       //calculates a difference between the two years.
                int iMonths = dtEnd.Month - dtBeg .Month;                   // calculates a difference between the two months.
                int iDays = dtEnd.Day - dtBeg.Day;                          // calculates a difference between the two dates.
                if (iDays < 0)                 {
                    iDays += DateTime.DaysInMonth (dtEnd.Year,                                             
                        1 +  (dtEnd.Month + 10) % 12);
                    iMonths -= 1;                 }
                if (iMonths < 0)                 {
                    iMonths += 12;
                    iYears -= 1;                 }
                lblLifeYears.Content =                   
                    String.Format("{0} year{1}, {2 } month{3}, {4} day{5}",            
                    iYears, iYears  == 1 ? "" : "s",                              
                    iMonths, iMonths  == 1 ? "" : "s",                             
                    iDays, iDays ==  1 ? "" : "s");             }
            else
            {
                lblLifeYears.Content = "";
            }           }     } } 
