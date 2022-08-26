using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Excel = Microsoft.Office.Interop.Excel;
//using System.Windows.Forms;
using System.IO;

namespace UFP_program.CastomClass
{
    public class SavingToExcelTable : Excel.Page
    {
        App app = (App)System.Windows.Application.Current;

        public void Save(DataGrid dataGrid, bool[] red_label)
        {
            Excel.Application excel = new Excel.Application();

            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Excel.Range myRange = sheet1.UsedRange;

            myRange.Cells[1, 1].Value2 = "Ф.И.О.";
            myRange.Cells[2, 1].Value2 = app.person.FIO;

            myRange.Cells[2, 2].Value2 = dataGrid.Columns[1].Header;
            myRange.Cells[3, 2].Value2 = dataGrid.Columns[2].Header;

            excel.Visible = true;
            for (int j = 0; j < dataGrid.Items.Count; j++)
            {
                sheet1.Cells[1, j + 1].Font.Bold = true; //Включаем жирный текст
                sheet1.Columns[j + 1].ColumnWidth = 15; //ширина 

            }
            for (int i = 0; i < dataGrid.Columns.Count - 1; i++)    // перебор строк в exel таблице
            {
                for (int j = 0; j < dataGrid.Items.Count + 5; j++)      // перебор столбцов в exel таблице
                {
                    if (j < 3)
                    {
                        if (j < dataGrid.Items.Count)
                        {
                            TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                            myRange.Cells[i + 1, j + 3].Value2 = b.Text;
                            myRange.NumberFormat = "General";
                        }
                        if (j <= 11 && i > 0)
                            if (red_label[j])
                            {
                                myRange.Cells[i + 1, j + 5].Interior.ColorIndex = 3;
                            }
                    }
                    if (j > 3 && j < 15)
                    {
                        if (j < dataGrid.Items.Count)
                        {
                            TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                            myRange.Cells[i + 1, j + 2].Value2 = b.Text;
                            myRange.NumberFormat = "General";
                        }
                        if (j <= 11 && i > 0)
                            if (red_label[j - 1])
                            {
                                myRange.Cells[i + 1, j + 4].Interior.ColorIndex = 3;
                            }
                    }
                    if (j == 15 && i == 2)
                    {
                        if (app.person.Gender == true)
                        {
                            myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.person.Group);
                            myRange.Cells[i + 1, j + 3].Value2 = " (М)";
                        }
                        else
                        {
                            myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.person.Group);
                            myRange.Cells[i + 1, j + 3].Value2 = " (Ж)";
                        }
                    }
                }
            }
        }

        public void SaveIn(System.Windows.Controls.DataGrid dataGrid, bool[] red_label)
        {
            if (app.FilePath == null)
            {
                app.FilePath = GetFilePath();
                if (app.FilePath == "")
                {
                    return;
                }
            }
            Excel.Application excel = new Excel.Application();
            Workbook workbook;

            if (!File.Exists(app.FilePath))
            {
                workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                workbook.SaveAs(app.FilePath);
            }
            workbook = excel.Workbooks.Open(app.FilePath);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Excel.Range myRange;
            myRange = (Excel.Range)sheet1.Cells[1, 1];
            if (myRange.Value2 == null)
            {
                myRange.Cells[1, 1].Value2 = "Ф.И.О.";
                myRange.Cells[2, 1].Value2 = app.person.FIO;
                myRange.Cells[2, 2].Value2 = dataGrid.Columns[1].Header;
                myRange.Cells[3, 2].Value2 = dataGrid.Columns[2].Header;
                for (int j = 0; j < dataGrid.Items.Count; j++)
                {
                    sheet1.Cells[1, j + 1].Font.Bold = true; //Включаем жирный текст
                    sheet1.Columns[j + 1].ColumnWidth = 15; //ширина 
                }
                for (int i = 0; i < dataGrid.Columns.Count - 1; i++)    // перебор строк в exel таблице
                {
                    for (int j = 0; j < dataGrid.Items.Count + 5; j++)      // перебор столбцов в exel таблице
                    {
                        if (j < 3)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange.Cells[i + 1, j + 3].Value2 = b.Text;        //     myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 2];
                                myRange.NumberFormat = "General";
                            }
                            if (j <= 11 && i > 0)
                                if (red_label[j])
                                {
                                    myRange.Cells[i + 1, j + 5].Interior.ColorIndex = 3;
                                }
                        }
                        if (j > 3 && j < 15)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange.Cells[i + 1, j + 2].Value2 = b.Text;
                                myRange.NumberFormat = "General";
                            }
                            if (j <= 11 && i > 0)
                                if (red_label[j - 1])
                                {
                                    myRange.Cells[i + 1, j + 4].Interior.ColorIndex = 3;
                                }
                        }
                        if (j == 15 && i == 1)
                        {
                            if (app.person.Gender == true)
                            {
                                myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.person.Group);
                                myRange.Cells[i + 1, j + 3].Value2 = " (М)";
                            }
                            //if (!app.Gender) 
                            else
                            {
                                myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.person.Group);
                                myRange.Cells[i + 1, j + 3].Value2 = " (Ж)";
                            }
                        }
                    }
                }

                workbook.Save();
                workbook.Close();
                excel.Quit();
            }
            else
            {
                int chek = 0;
                for (int i = 0; chek == 0; i++)
                {
                    if (myRange.Cells[i + 1, 3].Value == null)
                    {
                        if (myRange.Cells[2 + i, 3].Value == null)
                        {
                            chek = i + 1;
                        }
                    }
                }
                myRange.Cells[chek, 1].Value2 = app.person.FIO;
                myRange.Cells[chek, 2].Value2 = dataGrid.Columns[1].Header;
                myRange.Cells[chek + 1, 2].Value2 = dataGrid.Columns[2].Header;

                for (int i = 0; i < dataGrid.Columns.Count - 2; i++)    // перебор строк в exel таблице
                {
                    for (int j = 0; j < dataGrid.Items.Count + 2; j++)      // перебор столбцов в exel таблице
                    {
                        if (j < 3)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i + 1].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange.Cells[i + chek, j + 3].Value2 = b.Text; ;
                                myRange.NumberFormat = "General";
                            }
                            if (j <= 11)
                                if (red_label[j])
                                {
                                    myRange.Cells[i + chek, j + 5].Interior.ColorIndex = 3;
                                }
                        }
                        if (j > 3 && j < 15)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i + 1].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange.Cells[i + chek, j + 2].Value2 = b.Text;
                                myRange.NumberFormat = "General";
                            }
                            if (j <= 11)
                                if (red_label[j - 1])
                                {
                                    myRange.Cells[i + chek, j + 4].Interior.ColorIndex = 3;
                                }
                        }
                        if (j == 15 && i == 1)
                        {
                            if (app.person.Gender == true)
                            {
                                myRange.Cells[i + chek, j + 2].Value2 = Convert.ToString(app.person.Group);
                                myRange.Cells[i + chek, j + 3].Value2 = " (М)";
                            }
                            else
                            {
                                myRange.Cells[i + chek, j + 2].Value2 = Convert.ToString(app.person.Group);
                                myRange.Cells[i + chek, j + 3].Value2 = " (Ж)";
                            }
                        }
                    }
                }
                workbook.Save();
                workbook.Close();
                excel.Quit();
            }
        }

        public string GetFilePath()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".xlsx";
            dialog.Filter = "Excel documents (.xlsx)|*.xlsx";
            Nullable<bool> result = Convert.ToBoolean(dialog.ShowDialog());
            if (result == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        public HeaderFooter LeftHeader => throw new NotImplementedException();
        public HeaderFooter CenterHeader => throw new NotImplementedException();
        public HeaderFooter RightHeader => throw new NotImplementedException();
        public HeaderFooter LeftFooter => throw new NotImplementedException();
        public HeaderFooter CenterFooter => throw new NotImplementedException();
        public HeaderFooter RightFooter => throw new NotImplementedException();
    }
}
