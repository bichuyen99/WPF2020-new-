using System;               // Contains fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows;       // Provides several important WPF base element classes, various classes that support the WPF property system
using System.Windows.Controls; // Provides classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; // Provides types to support the WPF input system. 
using System.Windows.Media; // Provides types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications.
using System.Windows.Media.Imaging;  // Provides types that are used to encode and decode bitmap images.
namespace Petzold.CutCopyAndPaste {
    public class CutCopyAndPaste : Window {
        TextBlock text;                 // Creates a text block
        protected MenuItem itemCut, itemCopy, itemPaste, itemDelete;  // Initialize variables of menu item
        [STAThread]
        public static void Main() {
            Application app = new Application();        // Creates application, that lets you Cut,Copy ,Paste and Delete the text in a TextBlock element
            app.Run(new CutCopyAndPaste()); }
        public CutCopyAndPaste() {
            Title = "Cut, Copy, and Paste";            // Content of title
            // Create DockPanel.            
            DockPanel dock = new DockPanel();
            Content = dock;
            // Create Menu docked at top.     
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);
            // Create TextBlock filling the rest.   
            text = new TextBlock();
            text.Text = "Sample clipboard text";        // Adds content of text
            text.HorizontalAlignment =  HorizontalAlignment.Center;  // Sets textblock in center of horizontal
            text.VerticalAlignment =  VerticalAlignment.Center;      // Sets textblock in center of vertical 
            text.FontSize = 32;                                      // Sets size of font 
            text.TextWrapping = TextWrapping.Wrap;                   // The block wraps all text
            dock.Children.Add(text);                                // Adds text into textblock
            // Create Edit menu.        
            MenuItem itemEdit = new MenuItem();                // Creates item edit on menu item
            itemEdit.Header = "_Edit";                         // Adds header of item edit's content 
            itemEdit.SubmenuOpened += EditOnOpened;            // Disable menu items when the submenu is being displayed.
            menu.Items.Add(itemEdit);                          // Adds item edit to menu
            // Create items on Edit menu.    
            itemCut = new MenuItem();                          
            itemCut.Header = "Cu_t";                           // Adds header of item edit's content
            itemCut.Click += CutOnClick;                       // CLicks to cut 
            Image img = new Image();                           // Creates image     
            img.Source = new BitmapImage(new Uri("pack:/ /application:,,/Images/CutHS.png")); // Adds image's source
            itemCut.Icon = img;                                // Lets you put a little picture in the cut item 
            itemEdit.Items.Add(itemCut);                       // Adds item cut into item edit
            itemCopy = new MenuItem();                         // Creates copy item on menu item
            itemCopy.Header = "_Copy";                         // Adds header of item copy's content 
            itemCopy.Click += CopyOnClick;                     // Clicks to copy
            img = new Image();                                 // Creates image 
            img.Source = new BitmapImage(new Uri("pack:/ /application:,,/Images/CopyHS.png"));  // Adds image's source
            itemCopy.Icon = img;                               // Lets you put a little picture in the copy item 
            itemEdit.Items.Add(itemCopy);                      // Adds item copy into item edit
            itemPaste = new MenuItem();                        // Creates item paste on menu item            
            itemPaste.Click += PasteOnClick;                   // Clicks to copy
            img = new Image();                                 // Creates image 
            img.Source = new BitmapImage(new Uri("pack:/ /application:,,/Images/PasteHS.png"));   // Adds image's source
            itemPaste.Icon = img;                              // Lets you put a little picture in the paste item 
            itemEdit.Items.Add(itemPaste);                     // Adds item paste into item edit
            itemDelete = new MenuItem();                       // Creates item delete on menu item 
            itemDelete.Header = "_Delete";                     // Adds header of item edit's content
            itemDelete.Click += DeleteOnClick;                 // Clicks to delete
            img = new Image();                                 // Creates image 
            img.Source = new BitmapImage( new Uri("pack:/ /application:,,/Images/DeleteHS.png"));  // Adds image's source
            itemDelete.Icon = img;                             // Lets you put a little picture in the delete item  
            itemEdit.Items.Add(itemDelete);         }          // Adds item delete into item edit
        void EditOnOpened(object sender,  RoutedEventArgs args)         { 
            itemCut.IsEnabled = itemCopy.IsEnabled = itemDelete.IsEnabled = text.Text !=  null && text.Text.Length > 0; /* The Cut, Copy, and Delete options should be enabled only 
                                                                                                 if the program is able to copy something to the clipboard.*/
            itemPaste.IsEnabled = Clipboard.ContainsText(); }
        protected void CutOnClick(object sender,  RoutedEventArgs args)        { // Cut is a Copy followed by a Delete by calling CopyOnClick and DeleteOnClick
            CopyOnClick(sender, args);
            DeleteOnClick(sender, args);         }          
        protected void CopyOnClick(object sender,  RoutedEventArgs args)        { // The CopyOnClick method uses the static Clipboard.GetText method to copy text from the clipboard.
            if (text.Text != null && text.Text. Length > 0)
                Clipboard.SetText(text.Text);         }
        protected void PasteOnClick(object sender,  RoutedEventArgs args)        { // The PasteOnClick method uses the static Clipboard.GetText method to copy text from the clipboard
            if (Clipboard.ContainsText()) text.Text = Clipboard.GetText();         }
        protected void DeleteOnClick(object sender , RoutedEventArgs args)       { // Sets the Text property of the TextBlock element to null      
            text.Text = null;         }     } } 