using System;       // contain fundamental classes and base classes that define commonly-used value and reference data types
using System.Globalization;     // contain classes that define culture-related information
using System.Windows;           // provide several important WPF base element classes, various classes that support the WPF property system
using System.Windows.Data;      // contains classes used for binding properties to data sources
namespace Petzold.DecimalScrollBar {
    [ValueConversion(typeof(double), typeof(decimal))] // allow a value converter to specify data types
    public class DoubleToDecimalConverter : IValueConverter {  // provide a way to apply custom logic to a binding
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture) {
            decimal num = new Decimal((double)value);       // initialize a new instance of decimal to the value of the specified double number
            if (param != null) num = Decimal.Round(num, Int32.Parse(param as string)); // if parameter isn't null, round a decimal value 
            return num; }                                   // return to num
        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture) { 
            return Decimal.ToDouble((decimal)value); } } }  // convert the value of the specified decimal to the equivalent double number
