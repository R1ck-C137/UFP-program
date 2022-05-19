﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;


namespace Приложение_по_физре
{
    /// <summary>
    /// Логика взаимодействия для Nachalnaya.xaml
    /// </summary>
    public partial class Nachalnaya : Excel.Page
    {

        public Nachalnaya()
        {
            InitializeComponent();
            ShowsNavigationUI = false;
        }
        App app = (App)System.Windows.Application.Current;

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page1.xaml", UriKind.Relative));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Korotkaya_versiya.xaml", UriKind.Relative));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "УФС Группы"; // Default file name
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel documents (.xlsx)|*.xlsx";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                app.path = dlg.FileName;
            }
            
            if (app.path != null)
            {
                //workbook.SaveAs(app.path);
                app.Gruppa = true;
                
                //workbook.Close();
                //excel.Quit();
                
                NavigationService.Navigate(new Uri("/../Korotkaya_versiya.xaml", UriKind.Relative));
                
            }
        }

        public HeaderFooter LeftHeader => throw new NotImplementedException();

        public HeaderFooter CenterHeader => throw new NotImplementedException();

        public HeaderFooter RightHeader => throw new NotImplementedException();

        public HeaderFooter LeftFooter => throw new NotImplementedException();

        public HeaderFooter CenterFooter => throw new NotImplementedException();

        public HeaderFooter RightFooter => throw new NotImplementedException();

        private void CalculationPercent_Click(object sender, RoutedEventArgs e)
        {
            app.path = GetPath();
            if (app.path == "")
            {
                app.path = null;
                return;
            }
            Excel.Application excel = new Excel.Application();

            Workbook workbook;
            if (!File.Exists(app.path))
            {
                System.Windows.MessageBox.Show("Файла не существует!");
                return;
            }

            workbook = excel.Workbooks.Open(app.path);

            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];
            
            if (myRange.Cells[3, 2].Value2 == "Результат")
            {
                double Percent;
                int i;
                for (int j = 5; j <= 15; j++)
                {
                    double Total = 0;//всего
                    double Passed = 0;//выполненный норматив
                    for (i = 2; Convert.ToString(myRange.Cells[i, j].Value2) != null; i = i + 2)
                    {
                        //myRange = (Range)sheet1.Cells[i, j];
                        if (myRange.Cells[i, j].Interior.ColorIndex != 3)
                        {
                            Passed++;
                        }
                        Total++;
                    }
                    //Total--;
                    //Passed--;
                    Percent = 100 / (Total / Passed);
                    Percent = Math.Round(Percent, 0);
                    myRange.Cells[i + 1, j].Value2 = Percent + "%";
                    //myRange.NumberFormat = "Общий";
                }
            }

            workbook.Save();
            workbook.Close();
            excel.Quit();
            app.path = null;
            System.Windows.MessageBox.Show("Готово!");
        }


        public string GetPath()
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".xlsx";
            dialog.Filter = "Excel documents (.xlsx)|*.xlsx";
            Nullable<bool> result = Convert.ToBoolean(dialog.ShowDialog());
            if (result == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        public void CreateCharts()
        {
            if (app.path == null)
            {
                app.path = GetPath();
                if (app.path == "")
                {
                    app.path = null;
                    return;
                }
            }

            Excel.Application excel = new Excel.Application();

            Workbook workbook;
            if (!File.Exists(app.path))
            {
                System.Windows.MessageBox.Show("Файла не существует!");
                return;
            }

            workbook = excel.Workbooks.Open(app.path);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange = (Range)sheet1.Cells[1, 1];
            excel.Visible = true;

            int i = 1;
            int j = 1;

            for (i = 2; Convert.ToString(myRange.Cells[i, 5].Value2) != null; i++)
            {
            }
            int cell_position_for_Chart = i + 1;
            
            for (j = 5; j <= 15; j++)
            {
                if (Convert.ToString(myRange.Cells[i + 1, j].Value2) == null || Convert.ToString(myRange.Cells[i + 1, j].Value2).Contains("%"))
                {
                    double Percent;
                    for (j = 5; j <= 15; j++)
                    {
                        double Total = 0;//всего
                        double Passed = 0;//выполненный норматив
                        for (i = 2; Convert.ToString(myRange.Cells[i, j].Value2) != null; i = i + 2)
                        {
                            if (myRange.Cells[i, j].Interior.ColorIndex != 3)
                            {
                                Passed++;
                            }
                            Total++;
                        }
                        //Total--;
                        //Passed--;
                        Percent = 100 / (Total / Passed);
                        Percent = Math.Round(Percent, 0);
                        myRange.Cells[i + 1, j].Value2 = Percent + "%";
                    }
                    break;
                }
            }

            ChartObjects xlCharts = (ChartObjects)sheet1.ChartObjects(Type.Missing);
            ChartObject myChart = (ChartObject)xlCharts.Add(1420, 20, 450, 270);
            Chart chart = myChart.Chart;
            SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Type.Missing);
            Series series = seriesCollection.NewSeries();
            series.Values = sheet1.get_Range((Range)sheet1.Cells[cell_position_for_Chart, 5], (Range)sheet1.Cells[cell_position_for_Chart, 15]);
            //chart.ChartType = XlChartType.xlBarClustered; // horizontal histogram
            chart.ChartType = XlChartType.xlColumnClustered;
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Физическое состояние";
            chart.HasLegend = false;
            series.XValues = sheet1.get_Range("E1", "O1");
            chart.Axes(XlAxisType.xlValue).MaximumScale = 1.0;


            //excel.Visible = true;
            workbook.Save();
            //workbook.Close();
            //excel.Quit();
            app.path = null;
            System.Windows.MessageBox.Show("Готово!");
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            CreateCharts();
        }

        private void MenuItem_Gender_Click(object sender, RoutedEventArgs e)
        {
            Sorting_By_Column(18);
        }

        private void MenuItem_Group_Click(object sender, RoutedEventArgs e)
        {
            Sorting_By_Column(17);
        }

        public void Swap_Positions(int first, int second, int column, Worksheet sheet1)
        {
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];

            //int criteriaСolumn = 17;
            bool[] firstRedFlag = new bool[11];      //false - белая ячейка, true - красная ячейка
            bool[] secondRedFlag = new bool[11];
            string[,] firstMasValue = new string[2, column];
            string[,] secondMasValue = new string[2, column];
            From_Table_To_Array(firstMasValue, first, ref firstRedFlag, column, sheet1);
            From_Table_To_Array(secondMasValue, second, ref secondRedFlag, column, sheet1);
            From_Array_To_Table(firstMasValue, second, ref firstRedFlag, column, sheet1);
            From_Array_To_Table(secondMasValue, first, ref secondRedFlag, column, sheet1);
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

        public void Sorting_By_Column(int column, bool DelitePath = true)
        {
            if (app.path == null)
            {
                app.path = GetPath();
                if (app.path == "")
                {
                    app.path = null;
                    return;
                }
            }

            Excel.Application excel = new Excel.Application();

            Workbook workbook;
            if (!File.Exists(app.path))
            {
                System.Windows.MessageBox.Show("Файла не существует!");
                return;
            }

            workbook = excel.Workbooks.Open(app.path);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange = (Range)sheet1.Cells[1, 1];

            
            int lastLine = new int();

            for (int i = 2; Convert.ToString(myRange.Cells[i, 5].Value2) != null; i++)
            {
                lastLine = i;
            }
            lastLine--; // на последней сторке таблицы
            
            bool repitSort = true;
            while (repitSort)
            {
                repitSort = false;
                for (int i = 5; i <= lastLine; i += 2)
                {
                    for (int comparisonValue = i - 2; comparisonValue >= 3; comparisonValue -= 2)
                    {
                        if (myRange.Cells[i, column].Value2 == myRange.Cells[comparisonValue, column].Value2 && i != comparisonValue + 2 && myRange.Cells[comparisonValue + 2, column].Value2 != myRange.Cells[i, column].Value2)
                        {
                            Swap_Positions(i, comparisonValue + 2, column, sheet1);
                            repitSort = true;
                            break;
                        }
                    }
                }
            }
            workbook.Save();
            workbook.Close();
            excel.Quit();
            if(DelitePath == true)
                app.path = null;
            //System.Windows.MessageBox.Show("Готово!");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            Excel.Application excel = new Excel.Application();

            Sorting_By_Column(18, false);
            Workbook workbook = excel.Workbooks.Open(app.path);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            SeparationOfGroups(18, sheet1);
            CalculatingPercentForGroups(sheet1);

            excel.Visible = true;
        }
        void SeparationOfGroups(int column, Worksheet sheet1)
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
                        Swap_Positions(j, j - 2, column, sheet1);
                    }
                    i += 2;
                }
                if (myRange.Cells[i, column].Value == null)
                    break;
            }
        }

        void CalculatingPercentForGroups(Worksheet sheet1)
        {
            Range myRange = (Range)sheet1.Cells[1, 1];
            int column = 5;
            int row = 2;
            for (int i = 3; true; i += 2)
            {
                if (myRange.Cells[i, column].Value == null)
                {
                    CalculatingPercentagesUpToTheNullRow(row, sheet1);
                    row = i + 1;
                    if(myRange.Cells[row, column].Value == null)
                    {
                        break;
                    }
                }
            }
        }
        void CalculatingPercentagesUpToTheNullRow(int Row, Worksheet sheet1)
        {
            Range myRange = (Range)sheet1.Cells[1, 1];
            double Percent;
            int i;
            for (int j = 5; j <= 15; j++)
            {
                double Total = 0;//всего
                double Passed = 0;//выполненный норматив
                for (i = Row; Convert.ToString(myRange.Cells[i, j].Value2) != null; i = i + 2)
                {
                    if (myRange.Cells[i, j].Interior.ColorIndex != 3)
                    {
                        Passed++;
                    }
                    Total++;
                }
                Percent = 100 / (Total / Passed);
                Percent = Math.Round(Percent, 0);
                myRange.Cells[i + 1, j].Value2 = Percent + "%";
            }
        }
    }
}
