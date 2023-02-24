using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Security.Policy;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace UFP_program.CastomClass
{
    public class TableSort
    {
        App app = (App)System.Windows.Application.Current;

        public void Sorting_By_Column(int column)
        {
            app.FilePath = GetExcelFilePath();
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
            app.FilePath = GetExcelFilePath();
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
            Workbook workbook = excel.Workbooks.Open(app.FilePath);
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


        public void ListOfUnfulfilledStandards()
        {
            app.FilePath = GetExcelFilePath();

            Application excel = new Application();
            Workbook workbook;
            workbook = excel.Workbooks.Open(app.FilePath);
            if(workbook.Sheets.Count < 2)
                workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Worksheet sheet2 = (Worksheet)workbook.Sheets[2];
            Range myRange1 = sheet1.UsedRange;
            Range myRange2 = sheet2.UsedRange;

            int lastLine = new();

            for (int i = 2; Convert.ToString(myRange1.Cells[i, 5].Value2) != null; i++)
            {
                lastLine = i;
            }
            
            for(int i = 5; i <= 15; i++)
            {
                myRange2.Cells[1, i - 4].Value2 = myRange1.Cells[1, i].Value2;
                sheet2.Cells[1, i - 4].Font.Bold = true; //Включаем жирный текст
                //sheet2.Columns[1, i - 4].ColumnWidth = 15;
                sheet2.Columns[i - 4].ColumnWidth = 15;
                int lastLineInNewTable = 2;
                for (int j = 2; j + 1 <= lastLine; j += 2)
                {
                    if(myRange1.Cells[j, i].Interior.ColorIndex == 3)
                    {
                        myRange2.Cells[lastLineInNewTable, i - 4].Value2 = myRange1.Cells[j, 1].Value2;
                        lastLineInNewTable++;
                    }
                }
            }
            workbook.Save();
            workbook.Close();
        }

        static void Workbook_NewSheet(object sheet)
        {
            Worksheet worksheet = sheet as Worksheet;

            if (worksheet != null)
            {
                Console.WriteLine(String.Format(
                "Workbook.NewSheet({0})", worksheet.Name));
            }

            Chart chart = sheet as Chart;

            if (chart != null)
            {
                Console.WriteLine(String.Format(
                "Workbook.NewSheet({0})", chart.Name));
            }
        }

        protected void SwapRows(int first, int second, int column, Worksheet sheet1)
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

        protected void From_Table_To_Array(string[,] masValue, int lineNumber, ref bool[] redFlag, int column, Worksheet sheet1)
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

        protected void From_Array_To_Table(string[,] masValue, int lineNumber, ref bool[] redFlag, int column, Worksheet sheet1)
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
        public string GetExcelFilePath()
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
