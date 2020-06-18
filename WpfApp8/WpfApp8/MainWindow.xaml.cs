using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ExcelDataService _objExcelSer;
        Class1 _num = new Class1();
        Class2 _cal = new Class2();
        public MainWindow()
        {
            InitializeComponent();
        }
        // Getting Data From Excel Sheet 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            _objExcelSer = new ExcelDataService();
            try
            {
                dataGridnumber.ItemsSource = _objExcelSer.ReadRecordFromEXCELAsync().Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRefreshRecord_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }
        // Getting Data of each cell  
        private void dataGridnumber_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                FrameworkElement t = dataGridnumber.Columns[0].GetCellContent(e.Row);
                if (t.GetType() == typeof(TextBox))
                {
                    _num.time = Convert.ToDouble(((TextBox)t).Text);
                }

                FrameworkElement function = dataGridnumber.Columns[1].GetCellContent(e.Row);
                if (function.GetType() == typeof(TextBox))
                {
                    _num.funct = Convert.ToDouble(((TextBox)function).Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Get entire Row  
        public void dataGridnumber_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                bool IsSave = _objExcelSer.ManageExcelRecordsAsync(_num).Result;
                if (IsSave)
                {
                    MessageBox.Show("Saved Successfully.");
                }
                else
                {
                    MessageBox.Show("Some Problem Occured.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        // Get Record info to update  
        private void dataGridnumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _num = dataGridnumber.SelectedItem as Class1;
        }

        private void btncal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand MyCommand = new System.Data.OleDb.OleDbCommand();
                MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=; Extended Properties=Excel 12.0;Persist Security Info=True");
                MyConnection.Open();
                MyCommand.Connection = MyConnection;
                MyCommand.Parameters.AddWithValue("@time", _cal.time);
                MyCommand.Parameters.AddWithValue("@functP", _cal.funct_p);
                MyCommand.ExecuteNonQuery();
                MyConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}