using System;           // Contain fundamental classes and base classes that define commonly-used value and reference data types
using System.ComponentModel;    // Provide classes that are used to implement the run-time and design-time behavior of components and controls
using System.Xml.Serialization; // Contais classes that are used to serialize objects into XML format documents or streams.
namespace Petzold.SingleRecordDataEntry
{
    public class Person : INotifyPropertyChanged
    {         // PropertyChanged event definition.     
        public event PropertyChangedEventHandler  PropertyChanged;
        // Private fields.      
        string strFirstName = "<first name>";                       // Create a string, which is first name
        string strMiddleName = "";                                  // Create a string, which is middle name
        string strLastName = "<last name>";                         // Create a string, which is last name
        DateTime? dtBirthDate = new DateTime(1800,  1, 1);          // Initialize date of birth
        DateTime? dtDeathDate = new DateTime(1900,  12, 31);        // Initialize date of death
        // Public properties.     
        public string FirstName         {
            set             {
                strFirstName = value;                              // Set value of first name string
                OnPropertyChanged("FirstName");            } 
            get { return strFirstName; }        }                  // Get value of first name string
        public string MiddleName         {
            set             {
                strMiddleName = value;                             // Set value of middle name string
                OnPropertyChanged("MiddleName");             }
            get { return strMiddleName; }        }                 // Get value of middle name string
        public string LastName         {
            set             {
                strLastName = value;                               // Set value of last name string
                OnPropertyChanged("LastName");             }
            get { return strLastName; }            }               // Get value of last name string
        [XmlElement(DataType="date")]                              // Indicate a public field
        public DateTime? BirthDate         {                       // Represent BirthDate as a date
            set             {
                dtBirthDate = value;                               // Set value of BirthDate
                OnPropertyChanged("BirthDate");             }
            get { return dtBirthDate; }        }                   // Get value of BirthDate
        [XmlElement(DataType = "date")]                            // Indicate a public field
        public DateTime? DeathDate         {                       // Represent DeathDate as a date
            set             {
                dtDeathDate = value;                               // Set value of DeathDate
                OnPropertyChanged("DeathDate");             }
            get { return dtDeathDate; }        }                   // Get value of DeathDate
        // Fire the PropertyChanged event.     
        // OnPropertyChanged method provides a convenient place to fire the PropertyChanged event
        protected virtual void OnPropertyChanged (string strPropertyName)         { 
            if (PropertyChanged != null)                           // Delegate invocation can be simplified
                PropertyChanged(this, new PropertyChangedEventArgs (strPropertyName));         }     } } 
