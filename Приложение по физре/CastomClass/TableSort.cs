using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Controls;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace UFP_program.CastomClass
{
    public class TableSort
    {
        App app = (App)System.Windows.Application.Current;

        public void Sorting_By_Column(int column)
        {
            app.FilePath = GetFilePath();
            if (app.FilePath == null || app.FilePath == "")
            {
                return;
            }
            if (!File.Exists(app.FilePath))
            {
                System.Windows.MessageBox.Show("Файла не существует!");
                return;
            }

            Application excel = new Application();
            Workbook workbook;
            workbook = excel.Workbooks.Open(app.FilePath);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange = (Range)sheet1.Cells[1, 1];

            int lastLine = new();

            for (int i = 2; Convert.ToString(myRange.Cells[i, 5].Value2) != null; i++)
            {
                lastLine = i;
            }
            lastLine--; // на последней сторке таблицы

            bool repeatSort = true;
            while (repeatSort)
            {
                repeatSort = false;
                for (int i = 5; i <= lastLine; i += 2)
                {
                    for (int comparisonValue = i - 2; comparisonValue >= 3; comparisonValue -= 2)
                    {
                        if (myRange.Cells[i, column].Value2 == myRange.Cells[comparisonValue, column].Value2 && i != comparisonValue + 2 && myRange.Cells[comparisonValue + 2, column].Value2 != myRange.Cells[i, column].Value2)
                        {
                            SwapRows(i, comparisonValue + 2, column, sheet1);
                            repeatSort = true;
                            break;
                        }
                    }
                }
            }
            workbook.Save();
            workbook.Close();
            excel.Quit();

            //System.Windows.MessageBox.Show("Готово!");
        }

        public void BoobleSort()
        {
            app.FilePath = GetFilePath();
            if (app.FilePath == null || app.FilePath == "")
            {
                return;
            }
            if (!File.Exists(app.FilePath))
            {
                System.Windows.MessageBox.Show("Файла не существует!");
                return;
            }

            Application excel = new Application();
            Workbook workbook;
            workbook = excel.Workbooks.Open(app.FilePath);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange = (Range)sheet1.Cells[1, 1];

            int lastLine = new();

            for (int i = 2; Convert.ToString(myRange.Cells[i, 5].Value2) != null; i++)
            {
                lastLine = i;
            }
            //lastLine--; // на последней сторке таблицы

            bool repeatSort = true;
            int booble = lastLine;
            int sortedPart = 1;
            while (sortedPart < lastLine)
            {
                for (booble = lastLine; booble > sortedPart + 2 && booble > 3; booble -= 2)
                {
                    if(Convert.ToInt32(myRange.Cells[booble, 16].Value2) > Convert.ToInt32(myRange.Cells[booble - 2, 16].Value2))
                        SwapRows(booble, booble - 2, 16, sheet1);
                }
                sortedPart += 2;
            }

            workbook.Save();
            workbook.Close();
            excel.Quit();
        } 

        public void SwapRows(int first, int second, int column, Worksheet sheet1)
        {
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];

            bool[] firstRedFlag = new bool[11];      //false - белая ячейка, true - красная ячейка
            bool[] secondRedFlag = new bool[11];
            string[,] firstMasValue = new string[2, 18];
            string[,] secondMasValue = new string[2, 18];
            From_Table_To_Array(firstMasValue, first, ref firstRedFlag, column, sheet1);
            From_Table_To_Array(secondMasValue, second, ref secondRedFlag, column, sheet1);
            From_Array_To_Table(firstMasValue, second, ref firstRedFlag, column, sheet1);
            From_Array_To_Table(secondMasValue, first, ref secondRedFlag, column, sheet1);
        }

        public void SeparationOfGroups(int column, Worksheet sheet1)
        {
            Range myRange = (Range)sheet1.Cells[1, 1];

            string firstGroup = myRange.Cells[3, column].Value;
            for (int i = 3; true; i += 2)
            {
                if (myRange.Cells[i, column].Value != firstGroup && myRange.Cells[i, column].Value != null)
                {
                    int nullCell;
                    firstGroup = myRange.Cells[i, column].Value;
                    for (nullCell = i; myRange.Cells[nullCell, 7].Value != null; nullCell += 2)
                    {
                    }
                    for (int j = nullCell; j > i; j -= 2)
                    {
                        SwapRows(j, j - 2, column, sheet1);
                    }
                    i += 2;
                }
                if (myRange.Cells[i, column].Value == null)
                    break;
            }
        }

        public void From_Table_To_Array(string[,] masValue, int lineNumber, ref bool[] redFlag, int column, Worksheet sheet1)
        {
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];

            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    masValue[i, j] = Convert.ToString(myRange.Cells[i + lineNumber - 1, j + 1].Value2);
                    if (j >= 5 && myRange.Cells[i + lineNumber - 1, j].Interior.ColorIndex == 3 && i == 0)
                    {
                        redFlag[j - 5] = true;
                    }
                }
            }
        }

        public void From_Array_To_Table(string[,] masValue, int lineNumber, ref bool[] redFlag, int column, Worksheet sheet1)
        {
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];

            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    myRange.Cells[i + lineNumber - 1, j + 1].Value2 = masValue[i, j];
                    if (j <= 10)
                    {
                        if (redFlag[j])
                        {
                            myRange.Cells[i + lineNumber - 1, j + 5].Interior.ColorIndex = 3;
                        }
                        else
                            myRange.Cells[i + lineNumber - 1, j + 5].Interior.ColorIndex = 0;
                    }
                }
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
    }
}
