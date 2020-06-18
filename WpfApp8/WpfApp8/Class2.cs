using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp8
{
    class Class2
    {
        double t0 = 1;   
        double tf = 11;             
        double gamin, gamax;
        double pmin= 0;            // final function Ф(gamma,tau)
        double gmin = 0;            // minimum value of gamma
        double Tmin = 0;              // minimum value of tau
        public List<List<double>> data = new List<List<double>>();
        public List<List<double>> functp = new List<List<double>>();
        public double time { get; set; }
        public double funct_p { get; set; }

        public void Transfer()
        {
            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            string pathToFile = @"C:\Users\hp\source\repos\WpfApp8\WpfApp8\SK_Moschnost.xlsx";
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(pathToFile, 0, false, 5, "", "", false,
                Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets["Sheet1"];
           
            Microsoft.Office.Interop.Excel.Range usedColumn = ObjWorkSheet.UsedRange.Columns[3];
            System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
            string[] strArray = myvalues.OfType<object>().Select(m => m.ToString()).ToArray();
                        
        }
        public double function(double i, double T, double g)
        {
            double f = 0;
            double fi = data[2][Convert.ToInt32(i * 100)];
            double fi1 = data[2][Convert.ToInt32((i + T) * 100)];

            return f = (g - fi - fi1);
        }
        public double gamma()
        {
            double min = 10000000;
            double max = -1000000;
            for (double T = 0; T <= tf - t0; T += 0.01)
            {
                for (double t = t0; t <= tf; t += 0.01)
                {
                    // gamma = fi + fi1
                    if (data[2][Convert.ToInt32(t * 100)] + data[2][Convert.ToInt32((t + T) * 100)] > max)
                        max = data[1][Convert.ToInt32(t * 100)] + data[1][Convert.ToInt32((t + T) * 100)];
                    if (data[2][Convert.ToInt32(t * 100)] + data[2][Convert.ToInt32((t + T) * 100)] < min)
                        min = data[1][Convert.ToInt32(t * 100)] + data[1][Convert.ToInt32((t + T) * 100)];
                }
            }
            gamin = min;            // minumum value of gamma when t in (t0,tf); tau in (0,tf-t0)
            gamax = max;            // maximum value of gamma when t in (t0,tf); tau in (0,tf-t0)
            return gamin;
        }
        public void inte(double g, double T, double i)
        {
            // function Ф(gamma,tau)
            var workbook = new Workbook();
            var worksheet = workbook.Worksheets.Add("functP");
            functp[3][Convert.ToInt32(t0 * 100)] = 0;

            for (g = pmin; g < gamax; g += 1000)
            {
                for (T = 0; T <= tf - t0; T += 0.01)
                {
                    for (i = t0; i <= tf; i += 0.01)
                    {
                        functp[3][Convert.ToInt32(i * 100)+1] = functp[3][Convert.ToInt32(i * 100)] + Math.Pow(function(i, T, g), 2);
                        worksheet.Row(Convert.ToInt32(i * 100) + 1).Cell(2).Value = functp[2][Convert.ToInt32(i * 100) + 1];
                        worksheet.Row(Convert.ToInt32(i * 100) + 1).Cell(3).Value = i;
                    }
                    if ((pmin == 0) || (functp[2][Convert.ToInt32(i * 100)] < pmin))
                    {
                        pmin = functp[3][Convert.ToInt32(i * 100)];
                        gmin = g;
                        Tmin = T;
                    }
                 }
            }
         }
        

        
    }
}


