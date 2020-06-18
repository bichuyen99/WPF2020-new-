using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8
{
    public class Class1
    {
        public double time { get; set; }
        public double funct { get; set; }
    }
    public class ExcelDataService
    {
        OleDbConnection Conn;
        OleDbCommand Cmd;

        public ExcelDataService()
        {
            string ExcelFilePath = @"C:\Users\hp\source\repos\WpfApp8\WpfApp8\SK_Moschnost.xlsx";
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 12.0;Persist Security Info=True";
            Conn = new OleDbConnection(excelConnectionString);
        }
        // Method to Get All the Records from Excel  
        public async Task<ObservableCollection<Class1>> ReadRecordFromEXCELAsync()
        {
            ObservableCollection<Class1> number = new ObservableCollection<Class1>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select * from [Sheet1$]";
            var Reader = await Cmd.ExecuteReaderAsync();
            while (Reader.Read())
            {
                number.Add(new Class1()
                {
                    time = Convert.ToDouble(Reader["time"]),
                    funct = Convert.ToDouble(Reader["func"]),
                });
            }
            Reader.Close();
            Conn.Close();
            return number;
        }
        // Method to Insert Record in the Excel 
        public async Task<bool> ManageExcelRecordsAsync(Class1 num)
        {
            bool IsSave = false;
            if (num.time != 0)
            {
                await Conn.OpenAsync();
                Cmd = new OleDbCommand();
                Cmd.Connection = Conn;
                Cmd.Parameters.AddWithValue("@time", num.time);
                Cmd.Parameters.AddWithValue("@func", num.funct);
               
                if (!IsRecordExistAsync(num).Result)
                {
                    Cmd.CommandText = "Insert into [Sheet1$] values (@time,@func)";
                }
                else
                {
                    Cmd.CommandText = "Update [Sheet1$] set time=@time,funct=@func";

                }
                int result = await Cmd.ExecuteNonQueryAsync();
                if (result > 0)
                {
                    IsSave = true;
                }
                Conn.Close();
            }
            return IsSave;
        }
        // The method to check if the record is already available in the workgroup  
        private async Task<bool> IsRecordExistAsync(Class1 num)
        {
            bool IsRecordExist = false;
            Cmd.CommandText = "Select * from [Sheet1$] where time=@time";
            var Reader = await Cmd.ExecuteReaderAsync();
            if (Reader.HasRows)
            {
                IsRecordExist = true;
            }

            Reader.Close();
            return IsRecordExist;
        }
    }

}

