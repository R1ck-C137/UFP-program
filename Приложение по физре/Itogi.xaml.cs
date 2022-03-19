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

        public double[] Baly = new double[11];
        public bool[] red_label = new bool[11];

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            app.Indication.Clear();
            app.Person.Clear();

            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            int AgeToCount;
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

            GridList.Add(new GridClass()
            {
                lineHeader = "Рост",
                result = Convert.ToString(app.Indication[2]),
            });

            Baly[0] = app.Indication[0];
            GridList.Add(new GridClass()
            {
                lineHeader = "Возраст",
                result = Convert.ToString(app.Indication[0]),
                point = Convert.ToString(Baly[0])
            });

            if (app.Gender == true) //мужской пол
            {

                double NormaVesa_M = 50 + (app.Indication[2] - 150) * 0.75 + ((app.Indication[0] - 21) / 4);
                if (NormaVesa_M <= 0)
                {
                    NormaVesa_M = 0;
                }
                if (app.Indication[1] - NormaVesa_M < 1)
                {
                    Baly[1] = 30;
                }
                else
                {
                    if ((app.Indication[1] - NormaVesa_M) > 30 || NormaVesa_M == 0)
                    {
                        Baly[1] = 0;
                    }
                    else
                    {
                        Baly[1] = 30 - (app.Indication[1] - NormaVesa_M);
                    }
                }
                //--------------------------------------
                GridList.Add(new GridClass()
                {
                    lineHeader = "Масса тела",
                    result = Convert.ToString(app.Indication[1]),
                    norm = Convert.ToString(NormaVesa_M),
                    point = Convert.ToString(Baly[1])
                });
                if (app.Indication[1] > NormaVesa_M)
                {
                    Ves.Visibility = Visibility;
                    red_label[0] = true;
                }
                //--------------------------------------
                double NormaSistDavleniya_M = 109 + 0.5 * app.Indication[0] + 0.1 * app.Indication[1];
                double NormaDiastDavleniya_M = 74 + 0.1 * app.Indication[0] + 0.15 * app.Indication[1];

                Baly[2] = 30;
                if (app.Indication[5] - NormaSistDavleniya_M > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.Indication[5] - NormaSistDavleniya_M) / 5);
                }
                if (app.Indication[6] - NormaDiastDavleniya_M > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.Indication[6] - NormaDiastDavleniya_M) / 5);
                }

                GridList.Add(new GridClass()
                {
                    lineHeader = "Системное артериальное давление",
                    result = "",
                    point = Convert.ToString(Baly[2])
                });
                //--------------------------------------
                GridList.Add(new GridClass()
                {
                    lineHeader = "     Систолическое давление",
                    result = Convert.ToString(app.Indication[5]),
                    norm = Convert.ToString(NormaSistDavleniya_M),
                });
                if (app.Indication[5] > NormaSistDavleniya_M)
                {
                    SD.Visibility = Visibility;
                    red_label[1] = true;
                }
                //--------------------------------------
                GridList.Add(new GridClass()
                {
                    lineHeader = "     Диастолическое давление",
                    result = Convert.ToString(app.Indication[6]),
                    norm = Convert.ToString(NormaDiastDavleniya_M),
                });
                if (app.Indication[6] > NormaDiastDavleniya_M)
                {
                    DD.Visibility = Visibility;
                    red_label[2] = true;
                }
                //--------------------------------------
                Baly[3] = 90 - app.Indication[3];
                if (Baly[3] < 1) { Baly[3] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Пульс в покое",
                    result = Convert.ToString(app.Indication[3]),
                    norm = "60",
                    point = Convert.ToString(Baly[3])
                });
                if (app.Indication[3] > 60)
                {
                    PulsVPokoe.Visibility = Visibility;
                    red_label[3] = true;
                }
                //--------------------------------------
                if (app.Sport == true)          //  кросс
                {

                    Baly[4] = 30;
                    Baly[4] = Baly[4] - Math.Truncate((TableOfNorms_ForMen[AgeToCount, 5] - app.Indication[10]) / 50) * 5;

                    GridList.Add(new GridClass()
                    {
                        lineHeader = "Общая выносливость",
                        result = Convert.ToString(app.Indication[10]),
                        norm = Convert.ToString(TableOfNorms_ForMen[AgeToCount, 5]),
                        point = Convert.ToString(Baly[4])
                    });
                    if (app.Indication[10] < TableOfNorms_ForMen[AgeToCount, 5])
                    {
                        ObshVinos.Visibility = Visibility;
                        red_label[4] = true;
                    }
                }
                else                            //  кол-во тренеровок в неделю
                {
                    app.Indication[10] = Math.Truncate(app.Indication[10]);
                    if (app.Indication[10] >= 7) { Baly[4] = 30; }
                    if (app.Indication[10] == 4) { Baly[4] = 25; }
                    if (app.Indication[10] == 3) { Baly[4] = 20; }
                    if (app.Indication[10] == 2) { Baly[4] = 10; }
                    if (app.Indication[10] == 1) { Baly[4] = 5; }
                    if (app.Indication[10] < 1) { Baly[4] = 0; }
                    GridList.Add(new GridClass()
                    {
                        lineHeader = "Общая выносливость",
                        result = Convert.ToString(app.Indication[10]),
                        norm = "3",
                        point = Convert.ToString(Baly[4])
                    });
                    if (app.Indication[10] < 3)
                    {
                        ObshVinos.Visibility = Visibility;
                        red_label[4] = true;
                    }
                }
                //--------------------------------------
                if (app.Indication[4] >= app.Indication[3] + 20)
                {
                    Baly[5] = -10;
                }
                if (app.Indication[4] < app.Indication[3] + 20)
                {
                    Baly[5] = 10;
                }
                if (app.Indication[4] < app.Indication[3] + 15)
                {
                    Baly[5] = 20;
                }
                if (app.Indication[4] <= app.Indication[3] + 10)      //пульс после == пульс до + 10
                {
                    Baly[5] = 30;
                }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Востанавливваемость пульса",
                    result = Convert.ToString(app.Indication[4]),
                    norm = Convert.ToString(app.Indication[3] + 10),
                    point = Convert.ToString(Baly[5])
                });
                if (app.Indication[3] + 10 < app.Indication[4])
                {
                    VostPulsa.Visibility = Visibility;
                    red_label[5] = true;
                }
                //--------------------------------------
                Baly[6] = app.Indication[7] - TableOfNorms_ForMen[AgeToCount, 0];
                if (Baly[6] < 0) { Baly[6] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Гибкость",
                    result = Convert.ToString(app.Indication[7]),
                    norm = Convert.ToString(TableOfNorms_ForMen[AgeToCount, 0]),
                    point = Convert.ToString(Baly[6])
                });
                if (app.Indication[7] < TableOfNorms_ForMen[AgeToCount, 0])
                {
                    Gibcost.Visibility = Visibility;
                    red_label[6] = true;
                }
                //--------------------------------------
                Baly[7] = (TableOfNorms_ForMen[AgeToCount, 1] - app.Indication[8]) * 2;
                if (Baly[7] < 0) { Baly[7] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Быстрота",
                    result = Convert.ToString(app.Indication[8]),
                    norm = Convert.ToString(TableOfNorms_ForMen[AgeToCount, 1]),
                    point = Convert.ToString(Baly[7])
                });
                if (app.Indication[8] > TableOfNorms_ForMen[AgeToCount, 1])
                {
                    Bistrota.Visibility = Visibility;
                    red_label[7] = true;
                }
                //--------------------------------------
                if ((app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2]) == 0)
                {
                    Baly[8] = 2;
                }
                if ((app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2]) > 0)
                {
                    Baly[8] = 2 + (app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2]) * 2;
                }
                if (app.Indication[9] - TableOfNorms_ForMen[AgeToCount, 2] < 0) { Baly[8] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Динамическая сила",
                    result = Convert.ToString(app.Indication[9]),
                    norm = Convert.ToString(TableOfNorms_ForMen[AgeToCount, 2]),
                    point = Convert.ToString(Baly[8])
                });
                if (app.Indication[9] < TableOfNorms_ForMen[AgeToCount, 2])
                {
                    DinamSila.Visibility = Visibility;
                    red_label[8] = true;
                }
                //--------------------------------------
                if (app.Indication[11] - TableOfNorms_ForMen[AgeToCount, 3] >= 0)
                {
                    Baly[9] = (app.Indication[11] - (TableOfNorms_ForMen[AgeToCount, 3] - 1)) * 3;
                }
                if (app.Indication[11] - TableOfNorms_ForMen[AgeToCount, 3] < 0) { Baly[9] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Скоростная выносливость",
                    result = Convert.ToString(app.Indication[11]),
                    norm = Convert.ToString(TableOfNorms_ForMen[AgeToCount, 3]),
                    point = Convert.ToString(Baly[9])
                });
                if (app.Indication[9] < TableOfNorms_ForMen[AgeToCount, 3])
                {
                    SV.Visibility = Visibility;
                    red_label[9] = true;
                }
                //--------------------------------------
                if (app.Indication[12] - TableOfNorms_ForMen[AgeToCount, 4] >= 0)
                {
                    Baly[10] = (app.Indication[12] - (TableOfNorms_ForMen[AgeToCount, 4] - 1)) * 4;
                }
                if (app.Indication[12] - TableOfNorms_ForMen[AgeToCount, 4] < 0) { Baly[10] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Скоростно-силовая выностивость",
                    result = Convert.ToString(app.Indication[12]),
                    norm = Convert.ToString(TableOfNorms_ForMen[AgeToCount, 4]),
                    point = Convert.ToString(Baly[10])
                });
                if (app.Indication[12] < TableOfNorms_ForMen[AgeToCount, 4])
                {
                    SSV.Visibility = Visibility;
                    red_label[10] = true;
                }
                //--------------------------------------
                string ItogoviyBal = "Ошибка";
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] > 250) { ItogoviyBal = "Высокий"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 250) { ItogoviyBal = "Выше среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 160) { ItogoviyBal = "Средний"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 90) { ItogoviyBal = "Ниже среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] < 50) { ItogoviyBal = "Низкий"; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Ваш уровень физического состояния ",
                    result = "",
                    norm = ItogoviyBal,
                    point = Convert.ToString(Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10])
                });
            }


            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


            else
            {

                double NormaVesa_W = 50 + (app.Indication[2] - 150) * 0.32 + (app.Indication[0] - 21 / 5);
                if (NormaVesa_W <= 0)
                {
                    NormaVesa_W = 0;
                }
                if (app.Indication[1] - NormaVesa_W < 1)
                {
                    Baly[1] = 30;
                }
                else
                {
                    if ((app.Indication[1] - NormaVesa_W) > 30 || NormaVesa_W == 0)
                    {
                        Baly[1] = 0;
                    }
                    else
                    {
                        Baly[1] = 30 - (app.Indication[1] - NormaVesa_W);
                    }
                }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Масса тела",
                    result = Convert.ToString(app.Indication[1]),
                    norm = Convert.ToString(NormaVesa_W),
                    point = Convert.ToString(Baly[1])
                });
                if (app.Indication[1] > NormaVesa_W)
                {
                    Ves.Visibility = Visibility;
                    red_label[0] = true;
                }
                //--------------------------------------
                double NormaSistDavleniya_W = 102 + 0.7 * app.Indication[0] + 0.15 * app.Indication[1];
                double NormaDiastDavleniya_W = 78 + 0.17 * app.Indication[0] + 0.1 * app.Indication[1];

                Baly[2] = 30;
                if (app.Indication[5] - NormaSistDavleniya_W > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.Indication[5] - NormaSistDavleniya_W) / 5);
                }
                if (app.Indication[6] - NormaDiastDavleniya_W > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.Indication[6] - NormaDiastDavleniya_W) / 5);
                }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Системное артериальное давление",
                    result = "",
                    point = Convert.ToString(Baly[2])

                });
                //--------------------------------------
                GridList.Add(new GridClass()
                {
                    lineHeader = "     Систолическое давление",
                    result = Convert.ToString(app.Indication[5]),
                    norm = Convert.ToString(NormaSistDavleniya_W),
                });
                if (app.Indication[5] > NormaSistDavleniya_W)
                {
                    SD.Visibility = Visibility;
                    red_label[1] = true;
                }
                //--------------------------------------
                GridList.Add(new GridClass()
                {
                    lineHeader = "     Диастолическое давление",
                    result = Convert.ToString(app.Indication[6]),
                    norm = Convert.ToString(NormaDiastDavleniya_W),
                });
                if (app.Indication[6] > NormaDiastDavleniya_W)
                {
                    DD.Visibility = Visibility;
                    red_label[2] = true;
                }
                //--------------------------------------
                Baly[3] = 90 - app.Indication[3];
                if (Baly[3] < 1) { Baly[3] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Пульс в покое",
                    result = Convert.ToString(app.Indication[3]),
                    norm = "60",
                    point = Convert.ToString(Baly[3])
                });
                if (app.Indication[3] > 60)
                {
                    PulsVPokoe.Visibility = Visibility;
                    red_label[3] = true;
                }
                //--------------------------------------
                if (app.Sport == true)          //  кросс
                {
                    Baly[4] = 30;
                    Baly[4] = Baly[4] - Math.Truncate((TableOfNorms_ForWomen[AgeToCount, 5] - app.Indication[10]) / 50) * 5;
                    GridList.Add(new GridClass()
                    {
                        lineHeader = "Общая выносливость",
                        result = Convert.ToString(app.Indication[10]),
                        norm = Convert.ToString(TableOfNorms_ForWomen[AgeToCount, 5]),
                        point = Convert.ToString(Baly[4])
                    });
                    if (app.Indication[10] < TableOfNorms_ForWomen[AgeToCount, 5])
                    {
                        ObshVinos.Visibility = Visibility;
                        red_label[4] = true;
                    }
                }
                else                            //  кол-во тренеровок в неделю
                {
                    app.Indication[10] = Math.Truncate(app.Indication[10]);
                    if (app.Indication[10] >= 7) { Baly[4] = 30; }
                    if (app.Indication[10] == 4) { Baly[4] = 25; }
                    if (app.Indication[10] == 3) { Baly[4] = 20; }
                    if (app.Indication[10] == 2) { Baly[4] = 10; }
                    if (app.Indication[10] == 1) { Baly[4] = 5; }
                    if (app.Indication[10] < 1) { Baly[4] = 0; }
                    GridList.Add(new GridClass()
                    {
                        lineHeader = "Общая выносливость",
                        result = Convert.ToString(app.Indication[10]),
                        norm = "3",
                        point = Convert.ToString(Baly[4])
                    });
                    if (app.Indication[10] < 3)
                    {
                        ObshVinos.Visibility = Visibility;
                        red_label[4] = true;
                    }
                }
                //--------------------------------------
                if (app.Indication[4] >= app.Indication[3] + 20)
                {
                    Baly[5] = -10;
                }
                if (app.Indication[4] < app.Indication[3] + 20)
                {
                    Baly[5] = 10;
                }
                if (app.Indication[4] < app.Indication[3] + 15)
                {
                    Baly[5] = 20;
                }
                if (app.Indication[4] <= app.Indication[3] + 10)      //пульс после == пульс до + 10
                {
                    Baly[5] = 30;
                }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Востанавливваемость пульса",
                    result = Convert.ToString(app.Indication[4]),
                    norm = Convert.ToString(app.Indication[3] + 10),
                    point = Convert.ToString(Baly[5])
                });
                if (app.Indication[3] + 10 < app.Indication[4])
                {
                    VostPulsa.Visibility = Visibility;
                    red_label[5] = true;
                }
                //--------------------------------------
                Baly[6] = app.Indication[7] - TableOfNorms_ForWomen[AgeToCount, 0];
                if (Baly[6] < 0) { Baly[6] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Гибкость",
                    result = Convert.ToString(app.Indication[7]),
                    norm = Convert.ToString(TableOfNorms_ForWomen[AgeToCount, 0]),
                    point = Convert.ToString(Baly[6])
                });
                if (app.Indication[7] < TableOfNorms_ForWomen[AgeToCount, 0])
                {
                    Gibcost.Visibility = Visibility;
                    red_label[6] = true;
                }
                //--------------------------------------
                Baly[7] = (TableOfNorms_ForWomen[AgeToCount, 1] - app.Indication[8]) * 2;
                if (Baly[7] < 0) { Baly[7] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Быстрота",
                    result = Convert.ToString(app.Indication[8]),
                    norm = Convert.ToString(TableOfNorms_ForWomen[AgeToCount, 1]),
                    point = Convert.ToString(Baly[7])
                });
                if (app.Indication[8] > TableOfNorms_ForWomen[AgeToCount, 1])
                {
                    Bistrota.Visibility = Visibility;
                    red_label[7] = true;
                }
                //--------------------------------------
                if ((app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2]) == 0)
                {
                    Baly[8] = 2;
                }
                if ((app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2]) > 0)
                {
                    Baly[8] = 2 + (app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2]) * 2;
                }
                if (app.Indication[9] - TableOfNorms_ForWomen[AgeToCount, 2] < 0) { Baly[8] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Динамическая сила",
                    result = Convert.ToString(app.Indication[9]),
                    norm = Convert.ToString(TableOfNorms_ForWomen[AgeToCount, 2]),
                    point = Convert.ToString(Baly[8])
                });
                if (app.Indication[9] < TableOfNorms_ForWomen[AgeToCount, 2])
                {
                    DinamSila.Visibility = Visibility;
                    red_label[8] = true;
                }
                //--------------------------------------
                if (app.Indication[11] - TableOfNorms_ForWomen[AgeToCount, 3] >= 0)
                {
                    Baly[9] = (app.Indication[11] - (TableOfNorms_ForWomen[AgeToCount, 3] - 1)) * 3;
                }
                if (app.Indication[11] - TableOfNorms_ForWomen[AgeToCount, 3] < 0) { Baly[9] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Скоростная выносливость",
                    result = Convert.ToString(app.Indication[11]),
                    norm = Convert.ToString(TableOfNorms_ForWomen[AgeToCount, 3]),
                    point = Convert.ToString(Baly[9])
                });
                if (app.Indication[11] < TableOfNorms_ForWomen[AgeToCount, 3])
                {
                    SV.Visibility = Visibility;
                    red_label[9] = true;
                }
                //--------------------------------------
                if (app.Indication[12] - TableOfNorms_ForWomen[AgeToCount, 4] >= 0)
                {
                    Baly[10] = (app.Indication[12] - (TableOfNorms_ForWomen[AgeToCount, 4] - 1)) * 4;
                }
                if (app.Indication[12] - TableOfNorms_ForWomen[AgeToCount, 4] < 0) { Baly[10] = 0; }
                GridList.Add(new GridClass()
                {
                    lineHeader = "Скоростно-силовая выностивость",
                    result = Convert.ToString(app.Indication[12]),
                    norm = Convert.ToString(TableOfNorms_ForWomen[AgeToCount, 4]),
                    point = Convert.ToString(Baly[10])
                });
                if (app.Indication[12] < TableOfNorms_ForWomen[AgeToCount, 4])
                {
                    SSV.Visibility = Visibility;
                    red_label[10] = true;
                }
                //--------------------------------------
                string ItogoviyBal = "Ошибка";

                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] > 250) { ItogoviyBal = "Высокий"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 250) { ItogoviyBal = "Выше среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 160) { ItogoviyBal = "Средний"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 90) { ItogoviyBal = "Ниже среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] < 50) { ItogoviyBal = "Низкий"; }


                GridList.Add(new GridClass()
                {
                    lineHeader = "Ваш уровень физического состояния ",
                    result = "",
                    norm = ItogoviyBal,
                    point = Convert.ToString(Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10])
                });
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
            Range myRange;

            myRange = (Range)sheet1.Cells[1, 1];
            myRange.Value2 = "Ф.И.О.";
            myRange = (Range)sheet1.Cells[2, 1];
            myRange.Value2 = app.Person[0];
            

            myRange = (Range)sheet1.Cells[2, 2];
            myRange.Value2 = dataGrid.Columns[1].Header;
            myRange = (Range)sheet1.Cells[3, 2];
            myRange.Value2 = dataGrid.Columns[2].Header;

            excel.Visible = true;
            for (int j = 0; j < dataGrid.Items.Count; j++)
            {
                sheet1.Cells[1, j + 1].Font.Bold = true; //Включаем жирный текст
                sheet1.Columns[j + 1].ColumnWidth = 15; //ширина 

            }
            for (int i = 0; i < dataGrid.Columns.Count - 1; i++)    // перебор строк в exel таблице
            {
                for (int j = 0; j < dataGrid.Items.Count + 4; j++)      // перебор столбцов в exel таблице
                {
                    if (j < 3)
                    {
                        if (j < dataGrid.Items.Count)
                        {
                            TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                            myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 3];
                            myRange.Value2 = b.Text;
                        }
                        if (j <= 11 && i > 0)
                            if (red_label[j])
                            {
                                myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 5];
                                myRange.Interior.ColorIndex = 3;
                            }
                    }
                    if (j > 3)
                    {
                        if (j < dataGrid.Items.Count)
                        {
                            TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                            myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 2];
                            myRange.Value2 = b.Text;
                        }
                        if (j <= 11 && i > 0)
                            if (red_label[j - 1])
                            {
                                myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 4];
                                myRange.Interior.ColorIndex = 3;
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
                myRange = (Range)sheet1.Cells[1, 1];
                myRange.Value2 = "Ф.И.О.";
                myRange = (Range)sheet1.Cells[2, 1];
                myRange.Value2 = app.Person[0];
                

                myRange = (Range)sheet1.Cells[2, 2];
                myRange.Value2 = dataGrid.Columns[1].Header;
                myRange = (Range)sheet1.Cells[3, 2];
                myRange.Value2 = dataGrid.Columns[2].Header;


                for (int j = 0; j < dataGrid.Items.Count; j++) //Başlıklar için
                {
                    sheet1.Cells[1, j + 1].Font.Bold = true; //Включаем жирный текст
                    sheet1.Columns[j + 1].ColumnWidth = 15; //ширина 

                }
                for (int i = 0; i < dataGrid.Columns.Count - 1; i++)    // перебор строк в exel таблице
                {
                    for (int j = 0; j < dataGrid.Items.Count + 4; j++)      // перебор столбцов в exel таблице
                    {
                        if (j < 3)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 3];
                                myRange.NumberFormat = "General";
                                myRange.Value2 = b.Text;
                            }
                            if (j <= 11 && i > 0)
                                if (red_label[j])
                                {
                                    myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 5];
                                    myRange.Interior.ColorIndex = 3;
                                }
                        }
                        if (j > 3)
                        {
                            if (j < dataGrid.Items.Count) {
                                TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 2];
                                myRange.NumberFormat = "General";
                                myRange.Value2 = b.Text;
                            }
                            if (j <= 11 && i > 0)
                                if (red_label[j - 1])
                                {
                                    myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 4];
                                    myRange.Interior.ColorIndex = 3;
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
                    myRange = (Range)sheet1.Cells[i + 1, 3];
                    if (myRange.Value == null)
                    {
                        myRange = (Range)sheet1.Cells[2 + i, 3];
                        if (myRange.Value == null)
                        {
                            chek = i + 1;
                        }
                    }
                }
                myRange = (Range)sheet1.Cells[chek, 1];
                myRange.Value2 = app.Person[0];
                myRange = (Range)sheet1.Cells[chek, 2];
                myRange.Value2 = dataGrid.Columns[1].Header;
                myRange = (Range)sheet1.Cells[chek + 1, 2];
                myRange.Value2 = dataGrid.Columns[2].Header;

                for (int i = 0; i < dataGrid.Columns.Count - 2; i++)    // перебор строк в exel таблице
                {
                    for (int j = 0; j < dataGrid.Items.Count; j++)      // перебор столбцов в exel таблице
                    {
                        if (j < 3)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i + 1].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + chek, j + 3];
                                myRange.NumberFormat = "General";
                                myRange.Value2 = b.Text;
                            }
                            if (j <= 11)
                                if (red_label[j])
                                {
                                    myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + chek, j + 5];
                                    myRange.Interior.ColorIndex = 3;
                                }
                        }
                        if (j > 3)
                        {
                            if (j < dataGrid.Items.Count)
                            {
                                TextBlock b = dataGrid.Columns[i + 1].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + chek, j + 2];
                                myRange.NumberFormat = "General";
                                myRange.Value2 = b.Text;
                            }
                            if (j <= 11)
                                if (red_label[j - 1])
                                {
                                    myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + chek, j + 4];
                                    myRange.Interior.ColorIndex = 3;
                                }
                        }
                    }
                }
                workbook.Save();
                workbook.Close();
                excel.Quit();
                System.Windows.MessageBox.Show("Готово!");
            }
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
}

