using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Forms;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Приложение_по_физре.CastomClass;

namespace Приложение_по_физре
{
    public partial class Results
    {
        public Results() { InitializeComponent(); }

        App app = (App)System.Windows.Application.Current;
        Calculation_ForMen Calculation_ForMen = new Calculation_ForMen();
        Calculation_ForWomen Calculation_ForWomen = new Calculation_ForWomen();

        Person Person = new Person();

        public static List<GridClass> GridList = new List<GridClass>();
        public class GridClass
        {
            public string lineHeader { get; set; }
            public string result { get; set; }
            public string norm { get; set; }
            public string point { get; set; }
        }

        public bool[] red_label = new bool[11];

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            app.person.Clear();

            NavigationService.Navigate(new Uri("/../InitialPage.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GridList.Clear();

            tb1.Text = "Ф.И.О.: " + Convert.ToString(app.person.FIO);
            tb2.Text = "Группа: " + Convert.ToString(app.person.Group);

            AddInTable.AddInTableValue(ref GridList, "Рост", app.person.Height);

            app.point.Age = (int)app.person.Age;
            AddInTable.AddInTableValue(ref GridList, "Возраст", app.person.Age, point : app.point.Age);

            if (app.person.Gender == true)
            {
                ProcessingOfAvailableData_ForMen();
            }
            else
            {
                ProcessingOfAvailableData_ForWomen();
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

        public void button_Click(object sender, RoutedEventArgs e)
        {
            SavingToExcelTable savingToExcelTable = new SavingToExcelTable();
            savingToExcelTable.Save(dataGrid, red_label);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SavingToExcelTable savingToExcelTable = new SavingToExcelTable();
            savingToExcelTable.SaveIn(dataGrid, red_label);
            if (app.FilePath == null)
            {
                System.Windows.MessageBox.Show("Файл не выбран!");
            }
            app.FilePath = null;
            System.Windows.MessageBox.Show("Готово!");
        }

        public void ProcessingOfAvailableData_ForMen()
        {
            AddInTable AddInTable = new AddInTable();
            Calculation_ForMen.Сalculation();
            AddInTable.Men(ref GridList);
            MarkingOfUnfulfilledStandards_ForMen();
        }

        public void ProcessingOfAvailableData_ForWomen()
        {
            AddInTable AddInTable = new AddInTable();
            Calculation_ForWomen.Сalculation();
            AddInTable.Women(ref GridList);
            MarkingOfUnfulfilledStandards_ForWomen();
        }

        private void Sled_Click(object sender, RoutedEventArgs e)
        {
            SavingToExcelTable savingToExcelTable = new SavingToExcelTable();
            savingToExcelTable.SaveIn(dataGrid, red_label);
            NavigationService.Navigate(new Uri("/../Short_version.xaml", UriKind.Relative));
        }

        private void Zakonch_Click(object sender, RoutedEventArgs e)
        {
            SavingToExcelTable savingToExcelTable = new SavingToExcelTable();
            savingToExcelTable.SaveIn(dataGrid, red_label);
            app.GroupMode = false;
            app.FilePath = null;
            NavigationService.Navigate(new Uri("/../InitialPage.xaml", UriKind.Relative));
        }

        public void MarkingOfUnfulfilledStandards_ForMen()
        {
            if (app.person.Weight > Calculation_ForMen.WeightNorm((int)app.person.Height, (int)app.person.Age))
            {
                Ves.Visibility = Visibility;
                red_label[0] = true;
            }
            //--------------------------------------
            if (app.person.SystolicPressure > Calculation_ForMen.NormaSistDavleniya((int)app.person.Age, (int)app.person.Weight))
            {
                SD.Visibility = Visibility;
                red_label[1] = true;
            }
            //--------------------------------------
            if (app.person.DiastolicPressure > Calculation_ForMen.NormaSistDavleniya((int)app.person.Age, (int)app.person.Weight))
            {
                DD.Visibility = Visibility;
                red_label[2] = true;
            }
            //--------------------------------------
            if (app.person.PulseAtRest > 60)
            {
                PulsVPokoe.Visibility = Visibility;
                red_label[3] = true;
            }
            //--------------------------------------
            if (app.person.Sport == true)         //  кросс
            {
                if (app.person.OverallEndurance < Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 5])
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            else                            //  кол-во тренеровок в неделю
            {
                if (app.point.OverallEndurance < 3)
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            //--------------------------------------
            if (app.person.PulseAtRest + 10 < app.person.PulseAfterExercise)
            {
                VostPulsa.Visibility = Visibility;
                red_label[5] = true;
            }
            //--------------------------------------
            if (app.person.Flexibility < Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 0])
            {
                Gibcost.Visibility = Visibility;
                red_label[6] = true;
            }
            //--------------------------------------
            if (app.person.Speed > Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 1])
            {
                Bistrota.Visibility = Visibility;
                red_label[7] = true;
            }
            //--------------------------------------
            if (app.person.DynamicForce < Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 2])
            {
                DinamSila.Visibility = Visibility;
                red_label[8] = true;
            }
            //--------------------------------------
            if (app.person.SpeedEndurance < Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 3])
            {
                SV.Visibility = Visibility;
                red_label[9] = true;
            }
            //--------------------------------------
            if (app.person.SpeedAndStrengthEndurance < Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 4])
            {
                SSV.Visibility = Visibility;
                red_label[10] = true;
            }
        }

        public void MarkingOfUnfulfilledStandards_ForWomen()
        {
            if (app.person.Weight > Calculation_ForWomen.WeightNorm((int)app.person.Height, (int)app.person.Age))
            {
                Ves.Visibility = Visibility;
                red_label[0] = true;
            }
            //--------------------------------------
            if (app.person.SystolicPressure > Calculation_ForWomen.NormaSistDavleniya((int)app.person.Age, (int)app.person.Weight))
            {
                SD.Visibility = Visibility;
                red_label[1] = true;
            }
            //--------------------------------------
            if (app.person.DiastolicPressure > Calculation_ForWomen.NormaDiastDavleniya((int)app.person.Age, (int)app.person.Weight))
            {
                DD.Visibility = Visibility;
                red_label[2] = true;
            }
            //--------------------------------------
            if (app.person.PulseAtRest > 60)
            {
                PulsVPokoe.Visibility = Visibility;
                red_label[3] = true;
            }
            //--------------------------------------
            if (app.person.Sport == true)    //  кросс
            {
                if (app.person.OverallEndurance < Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 5])
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            else                            //  кол-во тренеровок в неделю
            {
                if (app.point.OverallEndurance < 3)
                {
                    ObshVinos.Visibility = Visibility;
                    red_label[4] = true;
                }
            }
            //--------------------------------------
            if (app.person.PulseAtRest + 10 < app.person.PulseAfterExercise)
            {
                VostPulsa.Visibility = Visibility;
                red_label[5] = true;
            }
            //--------------------------------------
            if (app.person.Flexibility < Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 0])
            {
                Gibcost.Visibility = Visibility;
                red_label[6] = true;
            }
            //--------------------------------------
            if (app.person.Speed > Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 1])
            {
                Bistrota.Visibility = Visibility;
                red_label[7] = true;
            }
            //--------------------------------------
            if (app.person.DynamicForce < Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 2])
            {
                DinamSila.Visibility = Visibility;
                red_label[8] = true;
            }
            //--------------------------------------
            if (app.person.SpeedEndurance < Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 3])
            {
                SV.Visibility = Visibility;
                red_label[9] = true;
            }
            //--------------------------------------
            if (app.person.SpeedAndStrengthEndurance < Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 4])
            {
                SSV.Visibility = Visibility;
                red_label[10] = true;
            }
        }
    }
}

