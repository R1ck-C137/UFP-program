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
            app.person.Clear();

            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AgeToCount = Convert.ToInt32(app.person.Age);

            if(app.person.Age < 19)
            {
                AgeToCount = 19;
            }
            if (app.person.Age > 29)
            {
                AgeToCount = 29;
            }
            AgeToCount -= 19;

            GridList.Clear();

            
            addInTable.AddInTableValue(ref GridList, "Рост", app.person.Height);

            app.point.Age = (int)app.person.Age;
            addInTable.AddInTableValue(ref GridList, "Возраст", app.person.Age, point : app.point.Age);

            if (app.person.Gender == true)
            {
                СalculationForMen();
            }
            else
            {
                CalculationForWomen();
            }
            dataGrid.ItemsSource = GridList;

            if (app.GroupMode == true)
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
            if (app.FilePath == null)
            {
                System.Windows.MessageBox.Show("Файл не выбран!");
            }
            app.FilePath = null;
            System.Windows.MessageBox.Show("Готово!");
        }

        public string GetFilePath()
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


        public void SaveIn()
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


            
            Range myRange;
            myRange = (Range)sheet1.Cells[1, 1];
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
                            if (j < dataGrid.Items.Count) {
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
                workbook.Save();
                workbook.Close();
                excel.Quit();
            }
        }

        public void СalculationForMen()
        {
            double NormaVesa_M = (double)(50 + (app.person.Height - 150) * 0.75 + ((app.person.Age - 21) / 4));
            if (NormaVesa_M <= 0)
            {
                NormaVesa_M = 0;
            }
            if (app.person.Weight - NormaVesa_M < 1)
            {
                app.point.Weight = 30;
            }
            else
            {
                if ((app.person.Weight - NormaVesa_M) > 30 || NormaVesa_M == 0)
                {
                    app.point.Weight = 0;
                }
                else
                {
                    app.point.Weight = (int)(30 - (app.person.Weight - NormaVesa_M));
                }
            }

            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "Масса тела", app.person.Weight, NormaVesa_M, app.point.Weight);

            if(app.person.Weight > NormaVesa_M)
            {
                Ves.Visibility = Visibility;
                red_label[0] = true;
            }

            //--------------------------------------

            double NormaSistDavleniya_M = (double)(109 + 0.5 * app.person.Age + 0.1 * app.person.Weight);
            double NormaDiastDavleniya_M = (double)(74 + 0.1 * app.person.Age + 0.15 * app.person.Weight);

            app.point.SystemPressure = 30;
            if (app.person.SystolicPressure - NormaSistDavleniya_M > 0)
            {
                app.point.SystemPressure = (int)(app.point.SystemPressure - Math.Truncate(((double)app.person.SystolicPressure - NormaSistDavleniya_M) / 5));
            }
            if (app.person.DiastolicPressure - NormaSistDavleniya_M > 0)
            {
                app.point.SystemPressure = (int)(app.point.SystemPressure - Math.Truncate(((double)app.person.DiastolicPressure - NormaSistDavleniya_M) / 5));
            }

            addInTable.AddInTableValue(ref GridList, "Системное артериальное давление", point: app.point.SystemPressure);

            //--------------------------------------

            addInTable.AddInTableValue(ref GridList, "     Систолическое давление", app.person.SystolicPressure, NormaSistDavleniya_M);

            if (app.person.SystolicPressure > NormaSistDavleniya_M)
            {
                SD.Visibility = Visibility;
                red_label[1] = true;
            }

            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "     Диастолическое давление", app.person.DiastolicPressure, NormaDiastDavleniya_M);

            if (app.person.DiastolicPressure > NormaDiastDavleniya_M)
            {
                DD.Visibility = Visibility;
                red_label[2] = true;
            }

            //--------------------------------------

            app.point.PulseAtRest = (int)(90 - app.person.PulseAtRest);
            if (app.point.PulseAtRest < 1) { app.point.PulseAtRest = 0; }

            addInTable.AddInTableValue(ref GridList, "Пульс в покое", app.person.PulseAtRest, 60, app.point.PulseAtRest);

            if (app.person.PulseAtRest > 60)
            {
                PulsVPokoe.Visibility = Visibility;
                red_label[3] = true;
            }

            //--------------------------------------

            if (app.person.Sport == true)         //  кросс
            {
                app.point.OverallEndurance = 30;
                app.point.OverallEndurance = (int)(app.point.OverallEndurance - Math.Truncate((TableOfNorms_ForMen[AgeToCount, 5] - (double)app.person.OverallEndurance) / 50) * 5);
                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance, TableOfNorms_ForMen[AgeToCount, 5], app.point.OverallEndurance);

                if (app.person.OverallEndurance < TableOfNorms_ForMen[AgeToCount, 5])
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            else                            //  кол-во тренеровок в неделю
            {
                app.person.OverallEndurance = (int?)Math.Truncate((double)app.person.OverallEndurance);
                if (app.person.OverallEndurance >= 7) { app.point.OverallEndurance = 30; }
                if (app.person.OverallEndurance == 4) { app.point.OverallEndurance = 25; }
                if (app.person.OverallEndurance == 3) { app.point.OverallEndurance = 20; }
                if (app.person.OverallEndurance == 2) { app.point.OverallEndurance = 10; }
                if (app.person.OverallEndurance == 1) { app.point.OverallEndurance = 5; }
                if (app.person.OverallEndurance < 1) { app.point.OverallEndurance = 0; }

                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance, 3, app.point.OverallEndurance);

                if (app.point.OverallEndurance < 3)
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }

            //--------------------------------------

            if (app.person.PulseAfterExercise >= app.person.PulseAtRest + 20)
            {
                app.point.HeartRateRecovery = -10;
            }
            if (app.person.PulseAfterExercise < app.person.PulseAtRest + 20)
            {
                app.point.HeartRateRecovery = 10;
            }
            if (app.person.PulseAfterExercise < app.person.PulseAtRest + 15)
            {
                app.point.HeartRateRecovery = 20;
            }
            if (app.person.PulseAfterExercise <= app.person.PulseAtRest + 10)     //пульс после == пульс до + 10
            {
                app.point.HeartRateRecovery = 30;
            }

            addInTable.AddInTableValue(ref GridList, "Востанавливваемость пульса", app.person.PulseAfterExercise, app.person.PulseAtRest + 10, app.point.HeartRateRecovery);

            if (app.person.PulseAtRest + 10 < app.person.PulseAfterExercise)
            {
                VostPulsa.Visibility = Visibility;
                red_label[5] = true;
            }

            //--------------------------------------

            app.point.Flexibility = (int)(app.person.Flexibility - TableOfNorms_ForMen[AgeToCount, 0]);
            if (app.point.Flexibility < 0) { app.point.Flexibility = 0; }

            addInTable.AddInTableValue(ref GridList, "Гибкость", app.person.Flexibility, TableOfNorms_ForMen[AgeToCount, 0], app.point.Flexibility);

            if (app.person.Flexibility < TableOfNorms_ForMen[AgeToCount, 0])
            {
                Gibcost.Visibility = Visibility;
                red_label[6] = true;
            }

            //--------------------------------------

            app.point.Speed = (int)(TableOfNorms_ForMen[AgeToCount, 1] - Convert.ToDouble(app.person.Speed)) * 2;
            if (app.point.Speed < 0) { app.point.Speed = 0; }

            addInTable.AddInTableValue(ref GridList, "Быстрота", app.person.Speed, TableOfNorms_ForMen[AgeToCount, 1], app.point.Speed);

            if (app.person.Speed > TableOfNorms_ForMen[AgeToCount, 1])
            {
                Bistrota.Visibility = Visibility;
                red_label[7] = true;
            }

            //--------------------------------------

            if ((app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) == 0)
            {
                app.point.DynamicForce = 2;
            }
            if ((app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) > 0)
            {
                app.point.DynamicForce = (int)(2 + (app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) * 2);
            }
            if (app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2] < 0) { app.point.DynamicForce = 0; }

            addInTable.AddInTableValue(ref GridList, "Динамическая сила", app.person.DynamicForce, TableOfNorms_ForMen[AgeToCount, 2], app.point.DynamicForce);

            if (app.person.DynamicForce < TableOfNorms_ForMen[AgeToCount, 2])
            {
                DinamSila.Visibility = Visibility;
                red_label[8] = true;
            }

            //--------------------------------------

            if (app.person.SpeedEndurance - TableOfNorms_ForMen[AgeToCount, 3] >= 0)
            {
                app.point.SpeedEndurance = (int)((app.person.SpeedEndurance - (TableOfNorms_ForMen[AgeToCount, 3] - 1)) * 3);
            }
            if (app.person.SpeedEndurance - TableOfNorms_ForMen[AgeToCount, 3] < 0) { app.point.SpeedEndurance = 0; }

            addInTable.AddInTableValue(ref GridList, "Скоростная выносливость", app.person.SpeedEndurance, TableOfNorms_ForMen[AgeToCount, 3], app.point.SpeedEndurance);

            if (app.person.SpeedEndurance < TableOfNorms_ForMen[AgeToCount, 3])
            {
                SV.Visibility = Visibility;
                red_label[9] = true;
            }

            //--------------------------------------

            if (app.person.SpeedAndStrengthEndurance - TableOfNorms_ForMen[AgeToCount, 4] >= 0)
            {
                app.point.SpeedAndStrengthEndurance = (int)((app.person.SpeedAndStrengthEndurance - (TableOfNorms_ForMen[AgeToCount, 4] - 1)) * 4);
            }
            if (app.person.SpeedAndStrengthEndurance - TableOfNorms_ForMen[AgeToCount, 4] < 0) { app.point.SpeedAndStrengthEndurance = 0; }

            addInTable.AddInTableValue(ref GridList, "Скоростно-силовая выностивость", app.person.SpeedAndStrengthEndurance, TableOfNorms_ForMen[AgeToCount, 4], app.point.SpeedAndStrengthEndurance);

            if (app.person.SpeedAndStrengthEndurance < TableOfNorms_ForMen[AgeToCount, 4])
            {
                SSV.Visibility = Visibility;
                red_label[10] = true;
            }
            CalculationFinalScore();
        }

        public void CalculationForWomen()
        {
            double NormaVesa_W = (double)(50 + (app.person.Height - 150) * 0.32 + (app.person.Age - 21 / 5));
            if (NormaVesa_W <= 0)
            {
                NormaVesa_W = 0;
            }
            if (app.person.Weight - NormaVesa_W < 1)
            {
                app.point.Weight = 30;
            }
            else
            {
                if ((app.person.Weight - NormaVesa_W) > 30 || NormaVesa_W == 0)
                {
                    app.point.Weight = 0;
                }
                else
                {
                    app.point.Weight = (int)(30 - (app.person.Weight - NormaVesa_W));
                }
            }
            
            addInTable.AddInTableValue(ref GridList, "Масса тела", app.person.Weight, NormaVesa_W, app.point.Weight);

            if(app.person.Weight > NormaVesa_W)
            {
                Ves.Visibility = Visibility;
                red_label[0] = true;
            }

            //--------------------------------------

            double NormaSistDavleniya_W = (double)(102 + 0.7 * app.person.Age + 0.15 * app.person.Weight);
            double NormaDiastDavleniya_W = (double)(78 + 0.17 * app.person.Age + 0.1 * app.person.Weight);

            app.point.SystemPressure = 30;
            if (app.person.SystolicPressure - NormaSistDavleniya_W > 0)
            {
                app.point.SystemPressure = (int)(app.point.SystemPressure - Math.Truncate(((double)app.person.SystolicPressure - NormaSistDavleniya_W) / 5));
            }
            if (app.person.DiastolicPressure - NormaSistDavleniya_W > 0)
            {
                app.point.SystemPressure = (int)(app.point.SystemPressure - Math.Truncate(((double)app.person.DiastolicPressure - NormaSistDavleniya_W) / 5));
            }

            addInTable.AddInTableValue(ref GridList, "Системное артериальное давление", point: app.point.SystemPressure);

            //--------------------------------------

            addInTable.AddInTableValue(ref GridList, "     Систолическое давление", app.person.SystolicPressure, NormaSistDavleniya_W);

            if (app.person.SystolicPressure > NormaSistDavleniya_W)
            {
                SD.Visibility = Visibility;
                red_label[1] = true;
            }

            //--------------------------------------
            
            addInTable.AddInTableValue(ref GridList, "     Диастолическое давление", app.person.DiastolicPressure, NormaDiastDavleniya_W);

            if (app.person.DiastolicPressure > NormaDiastDavleniya_W)
            {
                DD.Visibility = Visibility;
                red_label[2] = true;
            }

            //--------------------------------------

            app.point.PulseAtRest = (int)(90 - app.person.PulseAtRest);
            if (app.point.PulseAtRest < 1) { app.point.PulseAtRest = 0; }

            addInTable.AddInTableValue(ref GridList, "Пульс в покое", app.person.PulseAtRest, 60, app.point.PulseAtRest);

            if (app.person.PulseAtRest > 60)
            {
                PulsVPokoe.Visibility = Visibility;
                red_label[3] = true;
            }

            //--------------------------------------

            if (app.person.Sport == true)    //  кросс
            {
                app.point.OverallEndurance = 30;
                app.point.OverallEndurance = (int)(app.point.OverallEndurance - Math.Truncate((TableOfNorms_ForWomen[AgeToCount, 5] - (double)app.person.OverallEndurance) / 50) * 5);
                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance, TableOfNorms_ForWomen[AgeToCount, 5], app.point.OverallEndurance);

                if (app.person.OverallEndurance < TableOfNorms_ForWomen[AgeToCount, 5])
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            else                            //  кол-во тренеровок в неделю
            {

                app.person.OverallEndurance = (int?)Math.Truncate((double)app.person.OverallEndurance);
                if (app.person.OverallEndurance >= 7) { app.point.OverallEndurance = 30; }
                if (app.person.OverallEndurance == 4) { app.point.OverallEndurance = 25; }
                if (app.person.OverallEndurance == 3) { app.point.OverallEndurance = 20; }
                if (app.person.OverallEndurance == 2) { app.point.OverallEndurance = 10; }
                if (app.person.OverallEndurance == 1) { app.point.OverallEndurance = 5; }
                if (app.person.OverallEndurance < 1) { app.point.OverallEndurance = 0; }

                addInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance, 3, app.point.OverallEndurance);

                if (app.point.OverallEndurance < 3)
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }

            //--------------------------------------

            if (app.person.PulseAfterExercise >= app.person.PulseAtRest + 20)
            {
                app.point.HeartRateRecovery = -10;
            }
            if (app.person.PulseAfterExercise < app.person.PulseAtRest + 20)
            {
                app.point.HeartRateRecovery = 10;
            }
            if (app.person.PulseAfterExercise < app.person.PulseAtRest + 15)
            {
                app.point.HeartRateRecovery = 20;
            }
            if (app.person.PulseAfterExercise <= app.person.PulseAtRest + 10)      //пульс после == пульс до + 10
            {
                app.point.HeartRateRecovery = 30;
            }

            addInTable.AddInTableValue(ref GridList, "Востанавливваемость пульса", app.person.PulseAfterExercise, app.person.PulseAtRest + 10, app.point.HeartRateRecovery);

            if (app.person.PulseAtRest + 10 < app.person.PulseAfterExercise)
            {
                VostPulsa.Visibility = Visibility;
                red_label[5] = true;
            }

            //--------------------------------------

            app.point.Flexibility = (int)(app.person.Flexibility - TableOfNorms_ForWomen[AgeToCount, 0]);
            if (app.point.Flexibility < 0) { app.point.Flexibility = 0; }

            addInTable.AddInTableValue(ref GridList, "Гибкость", app.person.Flexibility, TableOfNorms_ForWomen[AgeToCount, 0], app.point.Flexibility);

            if (app.person.Flexibility < TableOfNorms_ForWomen[AgeToCount, 0])
            {
                Gibcost.Visibility = Visibility;
                red_label[6] = true;
            }

            //--------------------------------------

            app.point.Speed = (int)((TableOfNorms_ForWomen[AgeToCount, 1] - app.person.Speed) * 2);
            if (app.point.Speed < 0) { app.point.Speed = 0; }

            addInTable.AddInTableValue(ref GridList, "Быстрота", app.person.Speed, TableOfNorms_ForWomen[AgeToCount, 1], app.point.Speed);

            if (app.person.Speed > TableOfNorms_ForWomen[AgeToCount, 1])
            {
                Bistrota.Visibility = Visibility;
                red_label[7] = true;
            }

            //--------------------------------------

            if ((app.person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2]) == 0)
            {
                app.point.DynamicForce = 2;
            }
            if ((app.person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2]) > 0)
            {
                app.point.DynamicForce = (int)(2 + (app.person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2]) * 2);
            }

            if (app.person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2] < 0) { app.point.DynamicForce = 0; }
            
            addInTable.AddInTableValue(ref GridList, "Динамическая сила", app.person.DynamicForce, TableOfNorms_ForWomen[AgeToCount, 2], app.point.DynamicForce);

            if (app.person.DynamicForce < TableOfNorms_ForWomen[AgeToCount, 2])
            {
                DinamSila.Visibility = Visibility;
                red_label[8] = true;
            }

            //--------------------------------------

            if (app.person.SpeedEndurance - TableOfNorms_ForWomen[AgeToCount, 3] >= 0)
            {
                app.point.SpeedEndurance = (int)((app.person.SpeedEndurance - (TableOfNorms_ForWomen[AgeToCount, 3] - 1)) * 3);
            }

            if (app.person.SpeedEndurance - TableOfNorms_ForWomen[AgeToCount, 3] < 0) { app.point.SpeedEndurance = 0; }
            addInTable.AddInTableValue(ref GridList, "Скоростная выносливость", app.person.SpeedEndurance, TableOfNorms_ForWomen[AgeToCount, 3], app.point.SpeedEndurance);

            if (app.person.SpeedEndurance < TableOfNorms_ForWomen[AgeToCount, 3])
            {
                SV.Visibility = Visibility;
                red_label[9] = true;
            }

            //--------------------------------------

            if (app.person.SpeedAndStrengthEndurance - TableOfNorms_ForWomen[AgeToCount, 4] >= 0)
            {
                app.point.SpeedAndStrengthEndurance = (int)((app.person.SpeedAndStrengthEndurance - (TableOfNorms_ForWomen[AgeToCount, 4] - 1)) * 4);
            }

            if (app.person.SpeedAndStrengthEndurance - TableOfNorms_ForWomen[AgeToCount, 4] < 0) { app.point.SpeedAndStrengthEndurance = 0; }
            addInTable.AddInTableValue(ref GridList, "Скоростно-силовая выностивость", app.person.SpeedAndStrengthEndurance, TableOfNorms_ForWomen[AgeToCount, 4], app.point.SpeedAndStrengthEndurance);

            if (app.person.SpeedAndStrengthEndurance < TableOfNorms_ForWomen[AgeToCount, 4])
            {
                SSV.Visibility = Visibility;
                red_label[10] = true;
            }
            CalculationFinalScore();
        }

        public void CalculationFinalScore()
        {
            string TotalScore = "Ошибка";
            
            if (app.point.Sum() > 250) { TotalScore = "Высокий"; }
            if (app.point.Sum() <= 250) { TotalScore = "Выше среднего"; }
            if (app.point.Sum() <= 160) { TotalScore = "Средний"; }
            if (app.point.Sum() <= 90) { TotalScore = "Ниже среднего"; }
            if (app.point.Sum() < 50) { TotalScore = "Низкий"; }

            addInTable.AddInTableFinalScore(ref GridList, "Ваш уровень физического состояния ", TotalScore, app.point.Sum());
        }

        private void Sled_Click(object sender, RoutedEventArgs e)
        {
            SaveIn();
            NavigationService.Navigate(new Uri("/../Korotkaya_versiya.xaml", UriKind.Relative));
        }

        private void Zakonch_Click(object sender, RoutedEventArgs e)
        {
            SaveIn();
            app.GroupMode = false;
            app.FilePath = null;
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }
    }

    public class AddInTable
    {
        public void AddInTableValue(ref List<Itogi.GridClass> GridList, string lineHeader, double? result = null, double? norm = null, double? point = null)
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

