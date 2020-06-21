using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.IO;
using System.Threading;

namespace WpfApp9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public double t0 = 1;
        public double tf = 11;
        public double gamin, gamax;
        public double pmin, pmax;                       // value of Ф(gamma,tau)
        public double gmin, gmax;                           // value of gamma 
        public double Tmin,Tmax;                            //  value of tau
        public List<double> data = new List<double>();      // value of function fi(t)
        
        string fileExcel;

        public MainWindow()
        {
            InitializeComponent();
        }
               // function f = g - fi(t) - fi(t+ tau)
        public double function(double i, double T, double g)
        {
            
            double f = 0;
            double fi = data[Convert.ToInt32(i * 100)];
            double fi1 = data[Convert.ToInt32((i + T) * 100)];

            return f = (g - fi - fi1);
        }
        // function gamma
        public void gamma()
        {
            double min = 10000000;
            double max = -1000000;
            for (double T = 0; T <= tf - t0; T += 0.01)
            {
                for (double t = t0; t <= tf; t += 0.01)
                {
                    // gamma = fi + fi1
                    if (data[Convert.ToInt32(t * 100) + 1] + data[Convert.ToInt32((t + T) * 100) + 1] > max)
                        max = data[Convert.ToInt32(t * 100) + 1] + data[Convert.ToInt32((t + T) * 100) + 1];
                    if (data[Convert.ToInt32(t * 100) + 1] + data[Convert.ToInt32((t + T) * 100) + 1] < min)
                        min = data[Convert.ToInt32(t * 100) + 1] + data[Convert.ToInt32((t + T) * 100) + 1];
                }
            }
            gamin = min;            // minumum value of gamma when t in (t0,tf); tau in (0,tf-t0)
            gamax = max;            // maximum value of gamma when t in (t0,tf); tau in (0,tf-t0)

        }

               // value of integral
        public void inte(double g, double T, double t)
        {
                    
            for (g = gamin; g < gamax; g += 1000)
            {
                for (T = 0; T <= tf - t0; T += 0.01)
                {
                    double Integral = 0;
                    for (t = t0; t <= tf; t += 0.01)
                    {
                        Integral = Integral + Math.Pow(function(t, T, g), 2);
                    }
                    if ((pmin == 0) || (Integral < pmin))
                    {
                        pmin = Integral;
                        gmin = g;
                        Tmin = T;
                    }
                }
                if (g > gmin)
                    break;
            }
            // deep searching
            double r, x;
            for (g = gmin, r = 1; (g >= 0) && (g <= gmin + 2500); Math.Abs(g += (Math.Pow(-1, r) * r * 500)), r += 1) 
            {
                for (T = Tmin, x = 1; (T >= 0) && (T <= tf - t0) && (x < 101); Math.Abs(T += (Math.Pow(-1, x) * x / 100)), x += 1)
                { 
                    double Integral = 0;
                    for (int i = Convert.ToInt32(t0 * 100); i <= Convert.ToInt32(tf - t0 * 100); i++)
                    {
                        Integral = Integral + Math.Pow(function(t, T, g), 2);
                    }
                    if (Integral < pmin)
                    {
                        pmin = Integral;
                        gmin = g;
                        Tmin = T;

                    }

                }
            }

        }
        // value of min(gamma, tau){max(t)}
        public void minmax(double g, double T, double t)
        {
            double max, min = 0;        
            double tmax = 0;
            for (g = gamin; g < gamax; g += 1000)
            {
                for (T = 0; T <= tf - t0; T += 0.01)
                {
                    for (t = t0; t < tf; t += 0.01)
                    {
                        max = Math.Abs(function(t, T, g));
                        if (max > min)
                        {
                            min = max;
                            gmax = g;
                            Tmax = T;
                            tmax = t;
                            pmax = max;
                        }
                    }
                }
            }
            // deep searching
            double r, x;
            for (g = gmax, r = 1; (g >= 0) && (g <= gmax + 2500); Math.Abs(g += (Math.Pow(-1, r) * r * 500)), r += 1) 
            { 
                for (T = Tmax, x = 1; (T >= 0) && (T <= tf - t0) && (x < 251); Math.Abs(T += (Math.Pow(-1, x) * x / 100)), x += 1)
                { 
                    max = Math.Abs(function(t, T, g));
                    if (max > min)
                    {
                        min = max;
                        gmax = g;
                        Tmax = T;
                        tmax = t;
                        pmax = max;
                    }
                }
            }
        }
        private void btn1_Click(object sender, System.EventArgs e)
        {
            // function Ф(gamma,tau), Вт
            for (double g = pmin; g < gamax; g += 1000)
            {
                for (double T = 0; T <= tf - t0; T += 0.01)
                {
                    for (double t = t0; t <= tf; t += 0.01)
                    {
                        function(t, T, g);
                        gamma();
                        inte(g, T, t);
                    }
                }
            }
            String sMsg;
            sMsg = "Minimum of integral Ф(gamma,tau): ";
            sMsg = String.Concat(sMsg, pmin);
            sMsg = String.Concat(sMsg, " Вт, when minimum of gamma: ");
            sMsg = String.Concat(sMsg, gmin);
            sMsg = String.Concat(sMsg, ", minimum of tau: ");
            sMsg = String.Concat(sMsg, Tmin);

            MessageBoxResult mes = MessageBox.Show(sMsg, "Caculate and draw graphic?", MessageBoxButton.YesNo);
            if (mes == MessageBoxResult.No)
            {
                Close();
            }
            else
            {
                Microsoft.Office.Interop.Excel.Range Rng;
                fileExcel = @"C:\Users\hp\source\repos\WpfApp9\WpfApp9\obj\Debug\SK_Moschnost.xlsx";
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                // open workbook
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(fileExcel, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Sheets[1];
                Microsoft.Office.Interop.Excel.Chart xlChart;
                Microsoft.Office.Interop.Excel.Series xlSeries;
                xlApp.Visible = true;
                xlApp.UserControl = true;
                Microsoft.Office.Interop.Excel.Range usedColumn = xlSheet.UsedRange.Columns[2];
                System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
                string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();

                for (int i = 1; i < strArray.Length; i++)
                    data.Add(Convert.ToDouble(strArray[i]));


                // Add table headers going cell by cell.
                xlSheet.Cells[1, 4] = "Время [t0,tf], сек.";
                xlSheet.Cells[1, 5] = "gamma";

                //AutoFit columns A:B.
                Rng = xlSheet.get_Range("A1:G1");
                Rng.EntireColumn.AutoFit();

                // interval [t0, tf]
                Rng = xlApp.get_Range("D2", "D1002");
                Rng.Formula = "=A101";


                StreamWriter txt = new StreamWriter("testinte.txt");
                for (double t = t0; t < tf; t += 0.01)
                {
                    txt.WriteLine(data[Convert.ToInt32(t * 100) + 1] + data[Convert.ToInt32((t + Tmin) * 100) + 1]);
                }
                txt.Close();
                object misvalue = System.Reflection.Missing.Value;
                string[] txtname = System.IO.File.ReadAllLines(@"C:\Users\hp\source\repos\WpfApp9\WpfApp9\bin\Debug\testinte.txt");
                try
                {
                    for (int i = 0; i <= txtname.Length; i++)
                    {
                        xlSheet.Cells[5][i + 2] = txtname[i];
                    }
                    Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception" + ex);
                }
                // add a chart for the selected data
                xlWorkBook = (Microsoft.Office.Interop.Excel.Workbook)xlSheet.Parent;
                xlChart = (Microsoft.Office.Interop.Excel.Chart)xlWorkBook.Charts.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                // use the ChartWizard to create a new chart from the select data
                xlSeries = (Microsoft.Office.Interop.Excel.Series)xlChart.SeriesCollection(1);
                xlSeries.XValues = xlSheet.get_Range("E2:E1002");
            }
        }
        private void btn2_Click(object sender, System.EventArgs e)
        {
            for (double g = pmin; g < gamax; g += 1000)
            {
                for (double T = 0; T <= tf - t0; T += 0.01)
                {
                    for (double t = t0; t <= tf; t += 0.01)
                    {
                        function(t, T, g);
                        gamma();
                        minmax(g, T, t);
                    }
                }
            }
            String sMsg;
            sMsg = "Minimum of maximum |f(gamma,tau)|: ";
            sMsg = String.Concat(sMsg, pmax);
            sMsg = String.Concat(sMsg, " Вт, when minimum of gamma: ");
            sMsg = String.Concat(sMsg, gmax);
            sMsg = String.Concat(sMsg, ", minimum of tau: ");
            sMsg = String.Concat(sMsg, Tmax);

            MessageBoxResult mes = MessageBox.Show(sMsg, "Caculate and draw graphic?", MessageBoxButton.YesNo);
            if (mes == MessageBoxResult.No)
            {
                Close();
            }
            else
            {
                Microsoft.Office.Interop.Excel.Range Rng;
                fileExcel = @"C:\Users\hp\source\repos\WpfApp9\WpfApp9\obj\Debug\SK_Moschnost.xlsx";
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                // open workbook
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(fileExcel, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Sheets[1];
                Microsoft.Office.Interop.Excel.Chart xlChart;
                Microsoft.Office.Interop.Excel.Series xlSeries;
                xlApp.Visible = true;
                xlApp.UserControl = true;
                Microsoft.Office.Interop.Excel.Range usedColumn = xlSheet.UsedRange.Columns[2];
                System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
                string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();

                for (int i = 1; i < strArray.Length; i++)
                    data.Add(Convert.ToDouble(strArray[i]));


                // Add table headers going cell by cell.
                xlSheet.Cells[1, 4] = "Время [t0,tf], сек.";
                xlSheet.Cells[1, 5] = "|f(gamma,tau)|";

                //AutoFit columns A:B.
                Rng = xlSheet.get_Range("A1:G1");
                Rng.EntireColumn.AutoFit();

                // interval [t0, tf]
                Rng = xlApp.get_Range("D2", "D1002");
                Rng.Formula = "=A101";


                StreamWriter txt = new StreamWriter("testinte.txt");
                for (double t = t0; t < tf; t += 0.01)
                {
                    txt.WriteLine(Math.Abs(gmax - data[Convert.ToInt32(t * 100) + 1] - data[Convert.ToInt32((t + Tmax) * 100) + 1]));
                }
                txt.Close();
                object misvalue = System.Reflection.Missing.Value;
                string[] txtname = System.IO.File.ReadAllLines(@"C:\Users\hp\source\repos\WpfApp9\WpfApp9\bin\Debug\testinte.txt");
                try
                {
                    for (int i = 0; i <= txtname.Length; i++)
                    {
                        xlSheet.Cells[5][i + 2] = txtname[i];
                    }
                    Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception" + ex);
                }
                // add a chart for the selected data
                xlWorkBook = (Microsoft.Office.Interop.Excel.Workbook)xlSheet.Parent;
                xlChart = (Microsoft.Office.Interop.Excel.Chart)xlWorkBook.Charts.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                // use the ChartWizard to create a new chart from the select data
                xlSeries = (Microsoft.Office.Interop.Excel.Series)xlChart.SeriesCollection(1);
                xlSeries.XValues = xlSheet.get_Range("E2:E1002");
            }
        }
        
    }
}
