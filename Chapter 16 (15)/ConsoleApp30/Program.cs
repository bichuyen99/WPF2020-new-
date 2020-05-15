using System;               // Contain fundamental classes and base classes that define commonly-used value and reference data types
using System.Windows;       // Provide several important WPF base element classes, various classes that support the WPF property system 
using System.Windows.Controls; // Provide classes to create elements, known as controls, that enable a user to interact with an application
using System.Windows.Input; // Provide types to support the WPF input system
using System.Windows.Media; // Provide types that enable integration of rich media, including drawings, text, and audio/video content in WPF applications
namespace Petzold.ManuallyPopulateTreeView {
    public class ManuallyPopulateTreeView : Window {
        [STAThread]
        public static void Main() {
            Application app = new Application();            // Create a WPF application
            app.Run(new ManuallyPopulateTreeView()); }      // Start that application and open ManuallyPopulateTreeView window
        public ManuallyPopulateTreeView() {
            Title = "Manually Populate TreeView";
            TreeView tree = new TreeView();                 // Create a TreeView object 
            Content = tree;                                 // Set content of TreeView object
            TreeViewItem itemAnimal = new TreeViewItem();   // Create a TreeViewItem object 
            itemAnimal.Header = "Animal";                   // Set label of the Animal item
            tree.Items.Add(itemAnimal);                     // Add content of the Animal item to the tree
            TreeViewItem itemDog = new TreeViewItem();      // Create a TreeViewItem object 
            itemDog.Header = "Dog";                         // Set label of the Dog item
            // Add content to the Dog item
            itemDog.Items.Add("Poodle");                    
            itemDog.Items.Add("Irish Setter");              
            itemDog.Items.Add("German Shepherd");           
            itemAnimal.Items.Add(itemDog);                  // Add content of the Dog item to the Animal item
            TreeViewItem itemCat = new TreeViewItem();      // Create a TreeViewItem object
            itemCat.Header = "Cat";                         // Set label of the Cat item
            itemCat.Items.Add("Calico");                    // Add content to the Cat item
            TreeViewItem item = new TreeViewItem();         // Create a TreeViewItem object
            item.Header = "Alley Cat";                      // Set content of item's header
            itemCat.Items.Add(item);                        // Add an item to the Cat item
            Button btn = new Button();                      // Create a button
            btn.Content = "Noodles";                        // Set content of button
            itemCat.Items.Add(btn);                         // Add button's content to the Cat item
            itemCat.Items.Add("Siamese");                   // Add content to the Cat item
            itemAnimal.Items.Add(itemCat);                  // Add content of the Cat item to the Animal item
            TreeViewItem itemPrimate = new TreeViewItem();  // Create a TreeViewItem object
            itemPrimate.Header = "Primate";                 // Set label of the Primate item
            // Add content to the Primate item
            itemPrimate.Items.Add("Chimpanzee");
            itemPrimate.Items.Add("Bonobo");
            itemPrimate.Items.Add("Human");
            itemAnimal.Items.Add(itemPrimate);              // Add content of the Primate item to the Animal item
            TreeViewItem itemMineral = new TreeViewItem();  // Create a TreeViewItem object
            itemMineral.Header = "Mineral";                 // Set label of the Mineral item
            // Add content to the Mineral item
            itemMineral.Items.Add("Calcium");
            itemMineral.Items.Add("Zinc");
            itemMineral.Items.Add("Iron");
            tree.Items.Add(itemMineral);                    // Add content of the Mineral item to the tree
            TreeViewItem itemVegetable = new TreeViewItem(); // Create a TreeViewItem object
            itemVegetable.Header = "Vegetable";             // Set label of the Vegetable item
            // Add content to the Vegetable item
            itemVegetable.Items.Add("Carrot");
            itemVegetable.Items.Add("Asparagus");
            itemVegetable.Items.Add("Broccoli");
            tree.Items.Add(itemVegetable); } } }            // Add content of the Vegetable item to the tree