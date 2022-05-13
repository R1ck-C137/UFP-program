using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Forms;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Приложение_по_физре
{
    public partial class Itogi : Excel.Page
    {
        public Itogi() { InitializeComponent(); }

        App app = (App)System.Windows.Application.Current;
        AddInTable addInTable = new AddInTable();

        public static List<GridClass> GridList = new List<GridClass>();
        public class GridClass
        {
            public string lineHeader { get; set; }
            public string result { get; set; }
            public string norm { get; set; }
            public string point { get; set; }
        }

        public double[,] TableOfNorms_ForMen =
        {                                    //Возраст
            { 9, 13, 57, 18, 23, 3000, 7  },    //19
            { 9, 13, 56, 18, 22, 2900, 7.1},    //20
            { 9, 14, 55, 17, 22, 2800, 7.2},    //21
            { 9, 14, 53, 17, 21, 2750, 7.3},    //22
            { 8, 14, 52, 17, 21, 2700, 7.4},    //23
            { 8, 15, 51, 16, 20, 2650, 7.5},    //24
            { 8, 15, 50, 16, 20, 2600, 8  },    //25
            { 8, 15, 49, 16, 20, 2550, 8.1},    //26
            { 8, 16, 48, 15, 19, 2500, 8.2},    //27
            { 8, 16, 47, 15, 19, 2450, 8.27},   //28
            { 7, 16, 46, 15, 19, 2400, 8.37}    //29
        };

        public double[,] TableOfNorms_ForWomen =
        {                                    //Возраст
            { 10, 15, 41, 15, 21, 2065, 8.43},  //19
            { 10, 15, 40, 15, 20, 2010, 8.55},  //20
            { 10, 16, 39, 14, 20, 1960, 9.1 },  //21
            { 10, 16, 38, 14, 19, 1920, 9.23},  //22
            { 9, 16, 37, 14, 19, 1875, 9.36 },  //23
            { 9, 17, 37, 13, 18, 1840, 9.48 },  //24
            { 9, 17, 36, 13, 18, 1800, 10   },  //25
            { 9, 18, 35, 13, 18, 1765, 10.12},  //26
            { 9, 18, 35, 12, 17, 1730, 10.35},  //27
            { 8, 18, 34, 12, 17, 1700, 10.35},  //28
            { 8, 18, 33, 12, 17, 1670, 10.47}   //29
        };

        public double[] Point = new double[11];
        public bool[] red_label = new bool[11];

        public int AgeToCount;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            app.Indication.Clear();
            app.Person.Clear();

            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AgeToCount = Convert.ToInt32(app.Indication[0]);
            if (app.Indication[0] < 19)
            {
                AgeToCount = 19;
            }
            if (app.Indication[0] > 29)
            {
                AgeToCount = 29;
            }
            AgeToCount -= 19; 

            GridList.Clear();

            addInTable.AddInTableValue(ref GridList, "Рост", app.Indication[2]);

            Point[0] = app.Indication[0];
            //addInTable.AddInTableValue(ref GridList, Point[0], "Возраст", app.Indication[0]);
            addInTable.AddInTableValue(ref GridList, "Возраст", app.Indication[0], point : Point[0]);

            if (app.Gender == true) //мужской пол
            {
                СalculationForMen();
            }
            else
            {
                CalculationForWomen();
            }
            dataGrid.ItemsSource = GridList;

            if (app.Gruppa == true)
            {
                menu.Visibility = Visibility.Hidden;
                nazad.Visibility = Visibility.Hidden;
                Sled.Visibility = Visibility.Visible;
                Zakonch.Visibility = Visibility.Visible;
            }
            else
            {
                menu.Visibility = Visibility.Visible;
                nazad.Visibility = Visibility.Visible;
                Sled.Visibility = Visibility.Hidden;
                Zakonch.Visibility = Visibility.Hidden;
            }

        }

        HeaderFooter Excel.Page.LeftHeader => throw new NotImplementedException();
        HeaderFooter Excel.Page.CenterHeader => throw new NotImplementedException();
        HeaderFooter Excel.Page.RightHeader => throw new NotImplementedException();
        HeaderFooter Excel.Page.LeftFooter => throw new NotImplementedException();
        HeaderFooter Excel.Page.CenterFooter => throw new NotImplementedException();
        HeaderFooter Excel.Page.RightFooter => throw new NotImplementedException();

        public void button_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SaveIn();
            if (app.path == null)
            {
                System.Windows.MessageBox.Show("Файл не выбран!");
            }
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

        public void Save()
        {
            Excel.Application excel = new Excel.Application();

            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            Range myRange = (Range)sheet1.Cells[1, 1];

            myRange.Cells[1, 1].Value2 = "Ф.И.О.";
            myRange.Cells[2, 1].Value2 = app.Person[0];
            

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
                        if (app.Gender) {
                            myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.Person[1]) + " (М)";
                        }
                        if (!app.Gender)
                        {
                            myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.Person[1]) + " (Ж)";
                        }
                    }
                }
            }
        }


        public void SaveIn()
        {

            if (app.path == null)
            {
                app.path = GetPath();
                if (app.path == "")
                {
                    return;
                }
            }

            Excel.Application excel = new Excel.Application();

            Workbook workbook;
            if (!File.Exists(app.path))
            {
                workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                workbook.SaveAs(app.path);
            }

            workbook = excel.Workbooks.Open(app.path);

            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];


            int chek = 0;
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];
            if (myRange.Value2 == null)
            {
                myRange.Cells[1, 1].Value2 = "Ф.И.О.";
                myRange.Cells[2, 1].Value2 = app.Person[0];


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
                                //myRange.Value2 = b.Text;
                            }
                            if (j <= 11 && i > 0)
                                if (red_label[j])
                                {
                                    myRange.Cells[i + 1, j + 5].Interior.ColorIndex = 3;
                                }
                        }
                        if (j > 3 && j < 15)
                        {
                            if (j < dataGrid.Items.Count) {
                                TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange.Cells[i + 1, j + 2].Value2 = b.Text;
                                myRange.NumberFormat = "General";
                                //myRange.Value2 = b.Text;
                            }
                            if (j <= 11 && i > 0)
                                if (red_label[j - 1])
                                {
                                    myRange.Cells[i + 1, j + 4].Interior.ColorIndex = 3;
                                    //myRange.Interior.ColorIndex = 3;
                                }
                        }
                        if (j == 15 && i == 1)
                        {
                            //myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 2];
                            if (app.Gender)
                            {
                                myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.Person[1]) + " (М)";
                            }
                            if (!app.Gender)
                            {
                                myRange.Cells[i + 1, j + 2].Value2 = Convert.ToString(app.Person[1]) + " (Ж)";
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
                for (int i = 0; chek == 0; i++)
                {
                    //myRange = (Range)sheet1.Cells[i + 1, 3];
                    if (myRange.Cells[i + 1, 3].Value == null)
                    {
                        //myRange = (Range)sheet1.Cells[2 + i, 3];
                        if (myRange.Cells[2 + i, 3].Value == null)
                        {
                            chek = i + 1;
                        }
                    }
                }
                myRange.Cells[chek, 1].Value2 = app.Person[0];
                //myRange.Value2 = app.Person[0];
                myRange.Cells[chek, 2].Value2 = dataGrid.Columns[1].Header;
                //myRange.Value2 = dataGrid.Columns[1].Header;
                myRange.Cells[chek + 1, 2].Value2 = dataGrid.Columns[2].Header;
                //myRange.Value2 = dataGrid.Columns[2].Header;

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
                                //myRange.Value2 = b.Text;
                            }
                            if (j <= 11)
                                if (red_label[j])
                                {
                                    myRange.Cells[i + chek, j + 5].Interior.ColorIndex = 3;
                                    //myRange.Interior.ColorIndex = 3;
                                }
                        }
                        if (j > 3 && j < 15)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i + 1].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange.Cells[i + chek, j + 2].Value2 = b.Text;
                                myRange.NumberFormat = "General";
                                //myRange.Value2 = b.Text;
                            }
                            if (j <= 11)
                                if (red_label[j - 1])
                                {
                                    myRange.Cells[i + chek, j + 4].Interior.ColorIndex = 3;
                                    //myRange.Interior.ColorIndex = 3;
                                }
                        }
                        if (j == 15 && i == 1)
                        {
                            //myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + chek, j + 2];
                            if (app.Gender)
                            {
                                myRange.Cells[i + chek, j + 2].Value2 = Convert.ToString(app.Person[1]) + " (М)";
                            }
                            if (!app.Gender)
                            {
                                myRange.Cells[i + chek, j + 2].Value2 = Convert.ToString(app.Person[1]) + " (Ж)";
                            }
                        }
                    }
                }
                workbook.Save();
                workbook.Close();
                excel.Quit();
            }
        }

        public void СalculationForMen()
        {
            double NormaVesa_M = 50 + (app.Indication[2] - 150) * 0.75 + ((app.Indication[0] - 21) / 4);
            if (NormaVesa_M <= 0)
            {
                NormaVesa_M = 0;
            }
            if (app.Indication[1] - NormaVesa_M < 1)
            {
                Point[1] = 30;
            }
            else
            {
                if ((app.Indication[1] - NormaVesa_M) > 30 || NormaVesa_M == 0)
                {
                    Point[1] = 0;
                }
                else
                {
                    Point[1] = 30 - (app.Indication[1] - NormaVesa_M);
                }
            }
            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "Масса тела", app.Indication[1], NormaVesa_M, Point[1]);

            if (app.Indication[1] > NormaVesa_M)
            {
                Ves.Visibility = Visibility;
                red_label[0] = true;
            }
            //--------------------------------------
            double NormaSistDavleniya_M = 109 + 0.5 * app.Indication[0] + 0.1 * app.Indication[1];
            double NormaDiastDavleniya_M = 74 + 0.1 * app.Indication[0] + 0.15 * app.Indication[1];

            Point[2] = 30;
            if (app.Indication[5] - NormaSistDavleniya_M > 0)
            {
                Point[2] = Point[2] - Math.Truncate((app.Indication[5] - NormaSistDavleniya_M) / 5);
            }
            if (app.Indication[6] - NormaDiastDavleniya_M > 0)
            {
                Point[2] = Point[2] - Math.Truncate((app.Indication[6] - NormaDiastDavleniya_M) / 5);
            }

            addInTable.AddInTableValue(ref GridList, "Системное артериальное давление", point: Point[2]);

            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "     Систолическое давление", app.Indication[5], NormaSistDavleniya_M);

            if (app.Indication[5] > NormaSistDavleniya_M)
            {
                SD.Visibility = Visibility;
                red_label[1] = true;
            }
            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "     Диастолическое давление", app.Indication[6], NormaDiastDavleniya_M);

            if (app.Indication[6] > NormaDiastDavleniya_M)
            {
                DD.Visibility = Visibility;
                red_label[2] = true;
            }
            //--------------------------------------
            Point[3] = 90 - app.Indication[3];
            if (Point[3] < 1) { Point[3] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Пульс в покое", app.Indication[3], 60, Point[3]);

            if (app.Indication[3] > 60)
            {
                PulsVPokoe.Visibility = Visibility;
                red_label[3] = true;
            }
            //--------------------------------------
            if (app.Sport == true)          //  кросс
            {

                Point[4] = 30;
                Point[4] = Point[4] - Math.Truncate((TableOfNorms_ForMen[AgeToCount, 5] - app.Indication[10]) / 50) * 5;

                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.Indication[10], TableOfNorms_ForMen[AgeToCount, 5], Point[4]);

                if (app.Indication[10] < TableOfNorms_ForMen[AgeToCount, 5])
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            else                            //  кол-во тренеровок в неделю
            {
                app.Indication[10] = Math.Truncate(app.Indication[10]);
                if (app.Indication[10] >= 7) { Point[4] = 30; }
                if (app.Indication[10] == 4) { Point[4] = 25; }
                if (app.Indication[10] == 3) { Point[4] = 20; }
                if (app.Indication[10] == 2) { Point[4] = 10; }
                if (app.Indication[10] == 1) { Point[4] = 5; }
                if (app.Indication[10] < 1) { Point[4] = 0; }
                
                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.Indication[10], 3, Point[4]);

                if (app.Indication[10] < 3)
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            //--------------------------------------
            if (app.Indication[4] >= app.Indication[3] + 20)
            {
                Point[5] = -10;
            }
            if (app.Indication[4] < app.Indication[3] + 20)
            {
                Point[5] = 10;
            }
            if (app.Indication[4] < app.Indication[3] + 15)
            {
                Point[5] = 20;
            }
            if (app.Indication[4] <= app.Indication[3] + 10)      //пульс после == пульс до + 10
            {
                Point[5] = 30;
            }
            
            addInTable.AddInTableValue(ref GridList, "Востанавливваемость пульса", app.Indication[4], app.Indication[3] + 10, Point[5]);

            if (app.Indication[3] + 10 < app.Indication[4])
            {
                VostPulsa.Visibility = Visibility;
                red_label[5] = true;
            }
            //--------------------------------------
            Point[6] = app.Indication[7] - TableOfNorms_ForMen[AgeToCount, 0];
            if (Point[6] < 0) { Point[6] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Гибкость", app.Indication[7], TableOfNorms_ForMen[AgeToCount, 0], Point[6]);

            if (app.Indication[7] < TableOfNorms_ForMen[AgeToCount, 0])
            {
                Gibcost.Visibility = Visibility;
                red_label[6] = true;
            }
            //--------------------------------------
            Point[7] = (TableOfNorms_ForMen[AgeToCount, 1] - app.Indication[8]) * 2;
            if (Point[7] < 0) { Point[7] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Быстрота", app.Indication[8], TableOfNorms_ForMen[AgeToCount, 1], Point[7]);

            if (app.Indication[8] > TableOfNorms_ForMen[AgeToCount, 1])
            {
                Bistrota.Visibility = Visibility;
                red_label[7] = true;
            }
            //--------------------------------------
            if ((app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2]) == 0)
            {
                Point[8] = 2;
            }
            if ((app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2]) > 0)
            {
                Point[8] = 2 + (app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2]) * 2;
            }
            if (app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2] < 0) { Point[8] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Динамическая сила", app.Indication[9], TableOfNorms_ForMen[AgeToCount, 2], Point[8]);

            if (app.Indication[9] < TableOfNorms_ForMen[AgeToCount, 2])
            {
                DinamSila.Visibility = Visibility;
                red_label[8] = true;
            }
            //--------------------------------------
            if (app.Indication[11] - TableOfNorms_ForMen[AgeToCount, 3] >= 0)
            {
                Point[9] = (app.Indication[11] - (TableOfNorms_ForMen[AgeToCount, 3] - 1)) * 3;
            }
            if (app.Indication[11] - TableOfNorms_ForMen[AgeToCount, 3] < 0) { Point[9] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Скоростная выносливость", app.Indication[11], TableOfNorms_ForMen[AgeToCount, 3], Point[9]);

            if (app.Indication[9] < TableOfNorms_ForMen[AgeToCount, 3])
            {
                SV.Visibility = Visibility;
                red_label[9] = true;
            }
            //--------------------------------------
            if (app.Indication[12] - TableOfNorms_ForMen[AgeToCount, 4] >= 0)
            {
                Point[10] = (app.Indication[12] - (TableOfNorms_ForMen[AgeToCount, 4] - 1)) * 4;
            }
            if (app.Indication[12] - TableOfNorms_ForMen[AgeToCount, 4] < 0) { Point[10] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Скоростно-силовая выностивость", app.Indication[12], TableOfNorms_ForMen[AgeToCount, 4], Point[10]);

            if (app.Indication[12] < TableOfNorms_ForMen[AgeToCount, 4])
            {
                SSV.Visibility = Visibility;
                red_label[10] = true;
            }
            CalculationFinalScore();
        }

        public void CalculationForWomen()
        {
            double NormaVesa_W = 50 + (app.Indication[2] - 150) * 0.32 + (app.Indication[0] - 21 / 5);
            if (NormaVesa_W <= 0)
            {
                NormaVesa_W = 0;
            }
            if (app.Indication[1] - NormaVesa_W < 1)
            {
                Point[1] = 30;
            }
            else
            {
                if ((app.Indication[1] - NormaVesa_W) > 30 || NormaVesa_W == 0)
                {
                    Point[1] = 0;
                }
                else
                {
                    Point[1] = 30 - (app.Indication[1] - NormaVesa_W);
                }
            }
            
            addInTable.AddInTableValue(ref GridList, "Масса тела", app.Indication[1], NormaVesa_W, Point[1]);

            if (app.Indication[1] > NormaVesa_W)
            {
                Ves.Visibility = Visibility;
                red_label[0] = true;
            }
            //--------------------------------------
            double NormaSistDavleniya_W = 102 + 0.7 * app.Indication[0] + 0.15 * app.Indication[1];
            double NormaDiastDavleniya_W = 78 + 0.17 * app.Indication[0] + 0.1 * app.Indication[1];

            Point[2] = 30;
            if (app.Indication[5] - NormaSistDavleniya_W > 0)
            {
                Point[2] = Point[2] - Math.Truncate((app.Indication[5] - NormaSistDavleniya_W) / 5);
            }
            if (app.Indication[6] - NormaDiastDavleniya_W > 0)
            {
                Point[2] = Point[2] - Math.Truncate((app.Indication[6] - NormaDiastDavleniya_W) / 5);
            }
            
            addInTable.AddInTableValue(ref GridList, "Системное артериальное давление", point: Point[2]);

            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "     Систолическое давление", app.Indication[5], NormaSistDavleniya_W);

            if (app.Indication[5] > NormaSistDavleniya_W)
            {
                SD.Visibility = Visibility;
                red_label[1] = true;
            }
            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "     Диастолическое давление", app.Indication[6], NormaDiastDavleniya_W);

            if (app.Indication[6] > NormaDiastDavleniya_W)
            {
                DD.Visibility = Visibility;
                red_label[2] = true;
            }
            //--------------------------------------
            Point[3] = 90 - app.Indication[3];
            if (Point[3] < 1) { Point[3] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Пульс в покое", app.Indication[3], 60, Point[3]);

            if (app.Indication[3] > 60)
            {
                PulsVPokoe.Visibility = Visibility;
                red_label[3] = true;
            }
            //--------------------------------------
            if (app.Sport == true)          //  кросс
            {
                Point[4] = 30;
                Point[4] = Point[4] - Math.Truncate((TableOfNorms_ForWomen[AgeToCount, 5] - app.Indication[10]) / 50) * 5;
                
                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.Indication[10], TableOfNorms_ForWomen[AgeToCount, 5], Point[4]);

                if (app.Indication[10] < TableOfNorms_ForWomen[AgeToCount, 5])
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            else                            //  кол-во тренеровок в неделю
            {
                app.Indication[10] = Math.Truncate(app.Indication[10]);
                if (app.Indication[10] >= 7) { Point[4] = 30; }
                if (app.Indication[10] == 4) { Point[4] = 25; }
                if (app.Indication[10] == 3) { Point[4] = 20; }
                if (app.Indication[10] == 2) { Point[4] = 10; }
                if (app.Indication[10] == 1) { Point[4] = 5; }
                if (app.Indication[10] < 1) { Point[4] = 0; }
                
                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.Indication[10], 3, Point[4]);

                if (app.Indication[10] < 3)
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            //--------------------------------------
            if (app.Indication[4] >= app.Indication[3] + 20)
            {
                Point[5] = -10;
            }
            if (app.Indication[4] < app.Indication[3] + 20)
            {
                Point[5] = 10;
            }
            if (app.Indication[4] < app.Indication[3] + 15)
            {
                Point[5] = 20;
            }
            if (app.Indication[4] <= app.Indication[3] + 10)      //пульс после == пульс до + 10
            {
                Point[5] = 30;
            }
            
            addInTable.AddInTableValue(ref GridList, "Востанавливваемость пульса", app.Indication[4], app.Indication[3] + 10, Point[5]);

            if (app.Indication[3] + 10 < app.Indication[4])
            {
                VostPulsa.Visibility = Visibility;
                red_label[5] = true;
            }
            //--------------------------------------
            Point[6] = app.Indication[7] - TableOfNorms_ForWomen[AgeToCount, 0];
            if (Point[6] < 0) { Point[6] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Гибкость", app.Indication[7], TableOfNorms_ForWomen[AgeToCount, 0], Point[6]);

            if (app.Indication[7] < TableOfNorms_ForWomen[AgeToCount, 0])
            {
                Gibcost.Visibility = Visibility;
                red_label[6] = true;
            }
            //--------------------------------------
            Point[7] = (TableOfNorms_ForWomen[AgeToCount, 1] - app.Indication[8]) * 2;
            if (Point[7] < 0) { Point[7] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Быстрота", app.Indication[8], TableOfNorms_ForWomen[AgeToCount, 1], Point[7]);

            if (app.Indication[8] > TableOfNorms_ForWomen[AgeToCount, 1])
            {
                Bistrota.Visibility = Visibility;
                red_label[7] = true;
            }
            //--------------------------------------
            if ((app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2]) == 0)
            {
                Point[8] = 2;
            }
            if ((app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2]) > 0)
            {
                Point[8] = 2 + (app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2]) * 2;
            }
            if (app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2] < 0) { Point[8] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Динамическая сила", app.Indication[9], TableOfNorms_ForWomen[AgeToCount, 2], Point[8]);

            if (app.Indication[9] < TableOfNorms_ForWomen[AgeToCount, 2])
            {
                DinamSila.Visibility = Visibility;
                red_label[8] = true;
            }
            //--------------------------------------
            if (app.Indication[11] - TableOfNorms_ForWomen[AgeToCount, 3] >= 0)
            {
                Point[9] = (app.Indication[11] - (TableOfNorms_ForWomen[AgeToCount, 3] - 1)) * 3;
            }
            if (app.Indication[11] - TableOfNorms_ForWomen[AgeToCount, 3] < 0) { Point[9] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Скоростная выносливость", app.Indication[11], TableOfNorms_ForWomen[AgeToCount, 3], Point[9]);

            if (app.Indication[11] < TableOfNorms_ForWomen[AgeToCount, 3])
            {
                SV.Visibility = Visibility;
                red_label[9] = true;
            }
            //--------------------------------------
            if (app.Indication[12] - TableOfNorms_ForWomen[AgeToCount, 4] >= 0)
            {
                Point[10] = (app.Indication[12] - (TableOfNorms_ForWomen[AgeToCount, 4] - 1)) * 4;
            }
            if (app.Indication[12] - TableOfNorms_ForWomen[AgeToCount, 4] < 0) { Point[10] = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Скоростно-силовая выностивость", app.Indication[12], TableOfNorms_ForWomen[AgeToCount, 4], Point[10]);

            if (app.Indication[12] < TableOfNorms_ForWomen[AgeToCount, 4])
            {
                SSV.Visibility = Visibility;
                red_label[10] = true;
            }
            CalculationFinalScore();
        }

        public void CalculationFinalScore()
        {
            string TotalScore = "Ошибка";
            if (Point[0] + Point[1] + Point[2] + Point[3] + Point[4] + Point[5] + Point[6] + Point[7] + Point[8] + Point[9] + Point[10] > 250) { TotalScore = "Высокий"; }
            if (Point[0] + Point[1] + Point[2] + Point[3] + Point[4] + Point[5] + Point[6] + Point[7] + Point[8] + Point[9] + Point[10] <= 250) { TotalScore = "Выше среднего"; }
            if (Point[0] + Point[1] + Point[2] + Point[3] + Point[4] + Point[5] + Point[6] + Point[7] + Point[8] + Point[9] + Point[10] <= 160) { TotalScore = "Средний"; }
            if (Point[0] + Point[1] + Point[2] + Point[3] + Point[4] + Point[5] + Point[6] + Point[7] + Point[8] + Point[9] + Point[10] <= 90) { TotalScore = "Ниже среднего"; }
            if (Point[0] + Point[1] + Point[2] + Point[3] + Point[4] + Point[5] + Point[6] + Point[7] + Point[8] + Point[9] + Point[10] < 50) { TotalScore = "Низкий"; }

            addInTable.AddInTableFinalScore(ref GridList, "Ваш уровень физического состояния ", TotalScore, Point[0] + Point[1] + Point[2] + Point[3] + Point[4] + Point[5] + Point[6] + Point[7] + Point[8] + Point[9] + Point[10]);
        }

        private void Sled_Click(object sender, RoutedEventArgs e)
        {
            SaveIn();
            NavigationService.Navigate(new Uri("/../Korotkaya_versiya.xaml", UriKind.Relative));
        }

        private void Zakonch_Click(object sender, RoutedEventArgs e)
        {
            SaveIn();
            app.Gruppa = false;
            app.path = null;
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }
    }

    public class AddInTable
    {
        public void AddInTableValue(ref List<Itogi.GridClass> GridList, string lineHeader, double ? result = null, double ? norm = null, double ? point = null)
        {
            if (result == null && norm == null && point != null)
                GridList.Add(new Itogi.GridClass()
                {
                    lineHeader = lineHeader,
                    point = Convert.ToString(point)
                });
            if (result != null && norm == null && point == null)
                GridList.Add(new Itogi.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result)
                });
            if (result == null && norm != null && point == null)
                GridList.Add(new Itogi.GridClass()
                {
                    lineHeader = lineHeader,
                    norm = Convert.ToString(norm)
                });


            if (result != null && norm != null && point == null)
                GridList.Add(new Itogi.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result),
                    norm = Convert.ToString(norm)
                });
            if (result != null && norm == null && point != null)
                GridList.Add(new Itogi.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result),
                    point = Convert.ToString(point)
                });
            if (result == null && norm != null && point != null)
                GridList.Add(new Itogi.GridClass()
                {
                    lineHeader = lineHeader,
                    norm = Convert.ToString(norm),
                    point = Convert.ToString(point)
                });


            if ( result != null && norm != null && point != null)
                GridList.Add(new Itogi.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result),
                    norm = Convert.ToString(norm),
                    point = Convert.ToString(point)
                });
        }

        public void AddInTableFinalScore(ref List<Itogi.GridClass> GridList, string lineHeader, string norm, double point )
        {
            GridList.Add(new Itogi.GridClass()
            {
                lineHeader = lineHeader,
                norm = Convert.ToString(norm),
                point = Convert.ToString(point)
            });
        }
    }
}

