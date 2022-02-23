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

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
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
            //myRange = (Range)sheet1.Cells[2, 1];
            //myRange.Value2 = app.Lichnost[0];
            myRange = (Range)sheet1.Cells[3, 2];
            double Percent;
            if (myRange.Value2 == "Результат")
            {
                
                int i;
                for (int j = 5; j <= 15; j++)
                {
                    double Total = 0;
                    double Passed = 0;
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
                    Percent = Math.Round(Percent, 2);
                    myRange = (Range)sheet1.Cells[i - 2, j];
                    myRange.Value2 = Percent + "%";
                }
            }

            workbook.Save();
            workbook.Close();
            excel.Quit();
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
    }
}
