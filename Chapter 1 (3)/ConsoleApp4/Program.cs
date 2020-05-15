using System;           // contains fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows;   // provides several important WPF base element classes
using System.Windows.Input; // provides several important WPF base element classes
namespace Petzold.ThrowWindowParty {
    class ThrowWindowParty : Application {
        [STAThread]
        public static void Main() {
            ThrowWindowParty app = new ThrowWindowParty();          // creates a WPF application
            app.Run(); }                                            // starts that application
        protected override void OnStartup(StartupEventArgs args) {
            Window winMain = new Window();                          // creates a window
            winMain.Title = "Main Window";                          // sets window's title
            winMain.MouseDown += WindowOnMouseDown;                 // occurs when any mouse button is pressed while the pointer is over this element 
            winMain.Show();                                         // opens a window and returns without waiting for the newly opened window to close
            for (int i = 0; i < 2; i++) {                           // a loop from 0 to 2
                Window win = new Window();                          // creates new windows
                win.Title = "Extra Window No. " + (i + 1); win.Show(); } } // sets windows's titles
        void WindowOnMouseDown(object sender, MouseButtonEventArgs args) {
            Window win = new Window();                              // creates new window
            win.Title = "Modal Dialog Box";                         // sets window's title
            win.ShowDialog(); } } }                                 // opens a window and returns without waiting for the newly opened window to close

