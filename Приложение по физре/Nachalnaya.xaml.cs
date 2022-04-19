using System;
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

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
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
            myRange = (Range)sheet1.Cells[3, 2];
            double Percent;
            if (myRange.Value2 == "Результат")
            {
                int i;
                for (int j = 5; j <= 15; j++)
                {
                    double Total = 0;//всего
                    double Passed = 0;//выполненный норматив
                    for (i = 3; Convert.ToString(myRange.Value2) != null; i = i + 2)
                    {
                        myRange = (Range)sheet1.Cells[i, j];
                        if (myRange.Interior.ColorIndex != 3)
                        {
                            Passed++;
                        }
                        Total++;
                    }
                    Total--;
                    Passed--;
                    Percent = 100 / (Total / Passed);
                    Percent = Math.Round(Percent, 0);
                    myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i - 2, j];
                    myRange.Value2 = Percent + "%";
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
            excel.Visible = true;

            Range myRange;
            myRange = (Range)sheet1.Cells[1,1];
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
                        for (i = 3; Convert.ToString(myRange.Value2) != null; i = i + 2)
                        {
                            myRange = (Range)sheet1.Cells[i, j];
                            if (myRange.Interior.ColorIndex != 3)
                            {
                                Passed++;
                            }
                            Total++;
                        }
                        Total--;
                        Passed--;
                        Percent = 100 / (Total / Passed);
                        Percent = Math.Round(Percent, 0);
                        myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i - 2, j];
                        myRange.Value2 = Percent + "%";
                        //myRange.Cells[i, j].Value2 = "ок";
                    }
                    break;
                }
            }

            ChartObjects xlCharts = (ChartObjects)sheet1.ChartObjects(Type.Missing);
            ChartObject myChart = (ChartObject)xlCharts.Add(1380, 20, 450, 270);
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
            //System.Windows.MessageBox.Show("Готово!");
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            CreateCharts();
        }

        private void MenuItem_Gender_Click(object sender, RoutedEventArgs e)
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

            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];
            int i = 1;
            int j = 1;

            for (i = 2; Convert.ToString(myRange.Cells[i, 5].Value2) != null; i++)
            {
            }
            i--; // на последней сторке таблицы 
            int lastLine = i;

        }

        private void MenuItem_Group_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Swap_Positions(int first, int second)
        {
            Excel.Application excel = new Excel.Application();
            Workbook workbook;
            workbook = excel.Workbooks.Open(app.path);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];

            int criteriaСolumn = 17;
            double[,] firstMasValue = new double[2, 17];
            double[,] secondMasValue = new double[2, 17];
            From_Table_To_Array(firstMasValue, first);
            From_Table_To_Array(secondMasValue, second);
            From_Array_To_Table(firstMasValue, second);
            From_Array_To_Table(secondMasValue, first);
        }

        public void From_Table_To_Array(double[,] masValue, int lineNumber)
        {
            Excel.Application excel = new Excel.Application();
            Workbook workbook;
            workbook = excel.Workbooks.Open(app.path);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];

            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; i < 17; j++)
                {
                    masValue[i, j] = myRange.Cells[i + lineNumber + 1, j + 1].Value2;
                }
            }
        }

        public void From_Array_To_Table(double[,] masValue, int lineNumber)
        {
            Excel.Application excel = new Excel.Application();
            Workbook workbook;
            workbook = excel.Workbooks.Open(app.path);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];

            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; i < 17; j++)
                {
                    myRange.Cells[i + lineNumber + 1, j + 1].Value2 = masValue[i, j];
                }
            }
        }

    }
}
