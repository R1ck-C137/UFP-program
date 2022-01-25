using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace Приложение_по_физре
{
    /// <summary>
    /// Логика взаимодействия для Itogi.xaml
    /// </summary>

    public partial class Itogi : Excel.Page
    {
        public Itogi()
        {
            InitializeComponent();
        }

        //static public List<Stat> list = new List<Stat>();
        static public List<GridClass> GridList = new List<GridClass>();
        public int AgeForStat;


        public class GridClass
        {
            public string nadpisi { get; set; }
            public string rezultat { get; set; }
            public string norma { get; set; }
            public string balli { get; set; }
        }
        public double[,] TablicaNorm_M =
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

        public double[,] TablicaNorm_W =
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


        App app = (App)System.Windows.Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Rost.Visibility = Visibility.Hidden;
            Vozrast.Visibility = Visibility.Hidden;
            Ves.Visibility = Visibility.Hidden;
            SAD.Visibility = Visibility.Hidden;
            SD.Visibility = Visibility.Hidden;
            DD.Visibility = Visibility.Hidden;
            PulsVPokoe.Visibility = Visibility.Hidden;
            ObshVinos.Visibility = Visibility.Hidden;
            VostPulsa.Visibility = Visibility.Hidden;
            Gibcost.Visibility = Visibility.Hidden;
            Bistrota.Visibility = Visibility.Hidden;
            DinamSila.Visibility = Visibility.Hidden;
            SV.Visibility = Visibility.Hidden;
            SSV.Visibility = Visibility.Hidden;


            AgeForStat = Convert.ToInt32(app.stata[0]);
            if (app.stata[0] < 19)
            {
                AgeForStat = 19;
            }
            if (app.stata[0] > 29)
            {
                AgeForStat = 29;
            }
            AgeForStat = AgeForStat - 19;

            tb1.Text = "Испытуемый: " + app.Lichnost[0];
            tb2.Text = "Группа: " + app.Lichnost[1];
            GridList.Clear();
            if (app.Gender == true) //мужской пол
            {
                GridList.Add(new GridClass()// добавляем строки в таблицу
                {
                    nadpisi = "Рост",
                    rezultat = Convert.ToString(app.stata[2]),
                });

                Baly[0] = app.stata[0];
                GridList.Add(new GridClass()
                {
                    nadpisi = "Возраст",
                    rezultat = Convert.ToString(app.stata[0]),
                    balli = Convert.ToString(Baly[0])
                });

                double NormaVesa_M = 50 + (app.stata[2] - 150) * 0.75 + ((app.stata[0] - 21) / 4);
                if (NormaVesa_M <= 0)
                {
                    NormaVesa_M = 0;
                }
                if (app.stata[1] - NormaVesa_M < 1)
                {
                    Baly[1] = 30;
                }
                else
                {
                    if ((app.stata[1] - NormaVesa_M) > 30 || NormaVesa_M == 0)
                    {
                        Baly[1] = 0;
                    }
                    else
                    {
                        Baly[1] = 30 - (app.stata[1] - NormaVesa_M);
                    }
                }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Масса тела",
                    rezultat = Convert.ToString(app.stata[1]),
                    norma = Convert.ToString(NormaVesa_M),
                    balli = Convert.ToString(Baly[1])
                });
                if (app.stata[1] > NormaVesa_M)
                {
                    Ves.Visibility = Visibility;
                }

                double NormaSistDavleniya_M = 109 + 0.5 * app.stata[0] + 0.1 * app.stata[1];
                double NormaDiastDavleniya_M = 74 + 0.1 * app.stata[0] + 0.15 * app.stata[1];

                Baly[2] = 30;
                if (app.stata[5] - NormaSistDavleniya_M > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.stata[5] - NormaSistDavleniya_M) / 5);
                }
                if (app.stata[6] - NormaDiastDavleniya_M > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.stata[6] - NormaDiastDavleniya_M) / 5);
                }

                GridList.Add(new GridClass()
                {
                    nadpisi = "Системное артериальное давление",
                    rezultat = "",
                    balli = Convert.ToString(Baly[2])
                });

                GridList.Add(new GridClass()
                {
                    nadpisi = "     Систолическое давление",
                    rezultat = Convert.ToString(app.stata[5]),
                    norma = Convert.ToString(NormaSistDavleniya_M),
                });
                if (app.stata[5] > NormaSistDavleniya_M)
                {
                    SD.Visibility = Visibility;
                }
                GridList.Add(new GridClass()
                {
                    nadpisi = "     Диастолическое давление",
                    rezultat = Convert.ToString(app.stata[6]),
                    norma = Convert.ToString(NormaDiastDavleniya_M),
                });
                if (app.stata[6] > NormaDiastDavleniya_M)
                {
                    DD.Visibility = Visibility;
                }


                Baly[3] = 90 - app.stata[3];
                if (Baly[3] < 1) { Baly[3] = 0; }

                GridList.Add(new GridClass()
                {
                    nadpisi = "Пульс в покое",
                    rezultat = Convert.ToString(app.stata[3]),
                    norma = "60",
                    balli = Convert.ToString(Baly[3])
                });
                if (app.stata[3] > 60)
                {
                    PulsVPokoe.Visibility = Visibility;
                }

                if (app.Sport == true)          //  кросс
                {

                    Baly[4] = 30;
                    Baly[4] = Baly[4] - Math.Truncate((TablicaNorm_M[AgeForStat, 5] - app.stata[10]) / 50) * 5;

                    GridList.Add(new GridClass()
                    {
                        nadpisi = "Общая выносливость",
                        rezultat = Convert.ToString(app.stata[10]),
                        norma = Convert.ToString(TablicaNorm_M[AgeForStat, 5]),
                        balli = Convert.ToString(Baly[4])
                    });
                    if (app.stata[10] < TablicaNorm_M[AgeForStat, 5])
                    {
                        ObshVinos.Visibility = Visibility;
                    }
                }
                else                            //  кол-во тренеровок в неделю
                {
                    app.stata[10] = Math.Truncate(app.stata[10]);
                    if (app.stata[10] >= 7) { Baly[4] = 30; }
                    if (app.stata[10] == 4) { Baly[4] = 25; }
                    if (app.stata[10] == 3) { Baly[4] = 20; }
                    if (app.stata[10] == 2) { Baly[4] = 10; }
                    if (app.stata[10] == 1) { Baly[4] = 5; }
                    if (app.stata[10] < 1) { Baly[4] = 0; }
                    GridList.Add(new GridClass()
                    {
                        nadpisi = "Общая выносливость",
                        rezultat = Convert.ToString(app.stata[10]),
                        norma = "3",
                        balli = Convert.ToString(Baly[4])
                    });
                    if (app.stata[10] < 3)
                    {
                        ObshVinos.Visibility = Visibility;
                    }
                }

                if (app.stata[4] <= app.stata[3])
                {
                    Baly[5] = 30;
                }
                if (app.stata[4] < app.stata[3] + 10 /*&& app.stata[4] > app.stata[3]*/)      //пульс после == пульс до + 10
                {
                    Baly[5] = 30;
                }
                if (app.stata[4] < app.stata[3] + 15 && app.stata[4] > app.stata[3] + 10)
                {
                    Baly[5] = 20;
                }
                if (app.stata[4] < app.stata[3] + 20 && app.stata[4] > app.stata[3] + 15)
                {
                    Baly[5] = 10;
                }
                if (app.stata[4] >= app.stata[3] + 20)
                {
                    Baly[5] = -10;
                }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Востанавливваемость пульса",
                    rezultat = Convert.ToString(app.stata[4]),
                    norma = Convert.ToString(app.stata[3] + 10),
                    balli = Convert.ToString(Baly[5])
                });
                if (app.stata[3] + 10 < app.stata[4])
                {
                    VostPulsa.Visibility = Visibility;
                }

                Baly[6] = app.stata[7] - TablicaNorm_M[AgeForStat, 0];
                if (Baly[6] < 0) { Baly[6] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Гибкость",
                    rezultat = Convert.ToString(app.stata[7]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 0]),
                    balli = Convert.ToString(Baly[6])
                });
                if (app.stata[7] < TablicaNorm_M[AgeForStat, 0])
                {
                    Gibcost.Visibility = Visibility;
                }

                Baly[7] = (TablicaNorm_M[AgeForStat, 1] - app.stata[8]) * 2;
                if (Baly[7] < 0) { Baly[7] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Быстрота",
                    rezultat = Convert.ToString(app.stata[8]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 1]),
                    balli = Convert.ToString(Baly[7])
                });
                if (app.stata[8] > TablicaNorm_M[AgeForStat, 1])
                {
                    Bistrota.Visibility = Visibility;
                }

                if ((app.stata[9] - TablicaNorm_M[AgeForStat, 2]) == 0)
                {
                    Baly[8] = 2;
                }
                if ((app.stata[9] - TablicaNorm_M[AgeForStat, 2]) > 0)
                {
                    Baly[8] = 2 + (app.stata[9] - TablicaNorm_M[AgeForStat, 2]) * 2;
                }
                if (app.stata[9] - TablicaNorm_M[AgeForStat, 2] < 0) { Baly[8] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Динамическая сила",
                    rezultat = Convert.ToString(app.stata[9]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 2]),
                    balli = Convert.ToString(Baly[8])
                });
                if (app.stata[9] < TablicaNorm_M[AgeForStat, 2])
                {
                    DinamSila.Visibility = Visibility;
                }

                if (app.stata[11] - TablicaNorm_M[AgeForStat, 3] >= 0)
                {
                    Baly[9] = (app.stata[11] - (TablicaNorm_M[AgeForStat, 3] - 1)) * 3;
                }
                if (app.stata[11] - TablicaNorm_M[AgeForStat, 3] < 0) { Baly[9] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростная выносливость",
                    rezultat = Convert.ToString(app.stata[11]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 3]),
                    balli = Convert.ToString(Baly[9])
                });
                if (app.stata[9] < TablicaNorm_M[AgeForStat, 3])
                {
                    SV.Visibility = Visibility;
                }

                if (app.stata[12] - TablicaNorm_M[AgeForStat, 4] >= 0)
                {
                    Baly[10] = (app.stata[12] - (TablicaNorm_M[AgeForStat, 4] - 1)) * 4;
                }
                if (app.stata[12] - TablicaNorm_M[AgeForStat, 4] < 0) { Baly[10] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростно-силовая выностивость",
                    rezultat = Convert.ToString(app.stata[12]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 4]),
                    balli = Convert.ToString(Baly[10])
                });
                if (app.stata[12] < TablicaNorm_M[AgeForStat, 4])
                {
                    SSV.Visibility = Visibility;
                }

                string ItogoviyBal = "Ошибка";
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] > 250) { ItogoviyBal = "Высокий"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 250) { ItogoviyBal = "Выше среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 160) { ItogoviyBal = "Средний"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 90) { ItogoviyBal = "Ниже среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] < 50) { ItogoviyBal = "Низкий"; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Ваш уровень физического состояния ",
                    rezultat = "",
                    norma = ItogoviyBal,
                    balli = Convert.ToString(Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10])
                });
            }


            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


            else
            {
                GridList.Add(new GridClass()// добавляем строки в таблицу
                {
                    nadpisi = "Рост",
                    rezultat = Convert.ToString(app.stata[2]),
                    balli = Convert.ToString(0)
                });
                Baly[0] = app.stata[0];
                GridList.Add(new GridClass()
                {
                    nadpisi = "Возраст",
                    rezultat = Convert.ToString(app.stata[0]),
                    balli = Convert.ToString(Baly[0])

                });
                double NormaVesa_W = 50 + (app.stata[2] - 150) * 0.32 + (app.stata[0] - 21 / 5);
                if (NormaVesa_W <= 0)
                {
                    NormaVesa_W = 0;
                }
                if (app.stata[1] - NormaVesa_W < 1)
                {
                    Baly[1] = 30;
                }
                else
                {
                    if ((app.stata[1] - NormaVesa_W) > 30 || NormaVesa_W == 0)
                    {
                        Baly[1] = 0;
                    }
                    else
                    {
                        Baly[1] = 30 - (app.stata[1] - NormaVesa_W);
                    }
                }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Масса тела",
                    rezultat = Convert.ToString(app.stata[1]),
                    norma = Convert.ToString(NormaVesa_W),
                    balli = Convert.ToString(Baly[1])
                });

                double NormaSistDavleniya_W = 102 + 0.7 * app.stata[0] + 0.15 * app.stata[1];
                double NormaDiastDavleniya_W = 78 + 0.17 * app.stata[0] + 0.1 * app.stata[1];

                Baly[2] = 30;
                if (app.stata[5] - NormaSistDavleniya_W > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.stata[5] - NormaSistDavleniya_W) / 5);
                }
                if (app.stata[6] - NormaDiastDavleniya_W > 0)
                {
                    Baly[2] = Baly[2] - Math.Truncate((app.stata[6] - NormaDiastDavleniya_W) / 5);
                }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Системное артериальное давление",
                    rezultat = "",
                    balli = Convert.ToString(Baly[2])

                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "     Систолическое давление",
                    rezultat = Convert.ToString(app.stata[5]),
                    norma = Convert.ToString(NormaSistDavleniya_W),
                    //balli = Convert.ToString(0)
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "     Диастолическое давление",
                    rezultat = Convert.ToString(app.stata[6]),
                    norma = Convert.ToString(NormaDiastDavleniya_W),
                    //balli = Convert.ToString(0)
                });


                Baly[3] = 90 - app.stata[3];
                if (Baly[3] < 1) { Baly[3] = 0; }

                GridList.Add(new GridClass()
                {
                    nadpisi = "Пульс в покое",
                    rezultat = Convert.ToString(app.stata[3]),
                    norma = "60",
                    balli = Convert.ToString(Baly[3])
                });

                if (app.Sport == true)          //  кросс
                {
                    Baly[4] = 30;
                    Baly[4] = Baly[4] - Math.Truncate((TablicaNorm_W[AgeForStat, 5] - app.stata[10]) / 50) * 5;
                    GridList.Add(new GridClass()
                    {
                        nadpisi = "Общая выносливость",
                        rezultat = Convert.ToString(app.stata[10]),
                        norma = Convert.ToString(TablicaNorm_W[AgeForStat, 5]),
                        balli = Convert.ToString(Baly[4])
                    });
                    if (app.stata[10] < TablicaNorm_W[AgeForStat, 5])
                    {
                        ObshVinos.Visibility = Visibility;
                    }
                }
                else                            //  кол-во тренеровок в неделю
                {
                    app.stata[10] = Math.Truncate(app.stata[10]);
                    if (app.stata[10] >= 7) { Baly[4] = 30; }
                    if (app.stata[10] == 4) { Baly[4] = 25; }
                    if (app.stata[10] == 3) { Baly[4] = 20; }
                    if (app.stata[10] == 2) { Baly[4] = 10; }
                    if (app.stata[10] == 1) { Baly[4] = 5; }
                    if (app.stata[10] < 1) { Baly[4] = 0; }
                    GridList.Add(new GridClass()
                    {
                        nadpisi = "Общая выносливость",
                        rezultat = Convert.ToString(app.stata[10]),
                        norma = "3",
                        balli = Convert.ToString(Baly[4])
                    });
                    if (app.stata[10] < 3)
                    {
                        ObshVinos.Visibility = Visibility;
                    }
                }

                if (app.stata[4] <= app.stata[3])
                {
                    Baly[5] = 30;
                }
                if (app.stata[4] < app.stata[3] + 10)      //пульс после == пульс до + 10
                {
                    Baly[5] = 30;
                }
                if (app.stata[4] < app.stata[3] + 15 && app.stata[4] > app.stata[3] + 10)
                {
                    Baly[5] = 20;
                }
                if (app.stata[4] < app.stata[3] + 20 && app.stata[4] > app.stata[3] + 15)
                {
                    Baly[5] = 10;
                }
                if (app.stata[4] >= app.stata[3] + 20)
                {
                    Baly[5] = -10;
                }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Востанавливваемость пульса",
                    rezultat = Convert.ToString(app.stata[4]),
                    norma = Convert.ToString(app.stata[3] + 10),
                    balli = Convert.ToString(Baly[5])
                });

                Baly[6] = app.stata[7] - TablicaNorm_W[AgeForStat, 0];
                if (Baly[6] < 0) { Baly[6] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Гибкость",
                    rezultat = Convert.ToString(app.stata[7]),
                    norma = Convert.ToString(TablicaNorm_W[AgeForStat, 0]),
                    balli = Convert.ToString(Baly[6])
                });

                Baly[7] = (TablicaNorm_W[AgeForStat, 1] - app.stata[8]) * 2;
                if (Baly[7] < 0) { Baly[7] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Быстрота",
                    rezultat = Convert.ToString(app.stata[8]),
                    norma = Convert.ToString(TablicaNorm_W[AgeForStat, 1]),
                    balli = Convert.ToString(Baly[7])
                });

                if ((app.stata[9] - TablicaNorm_W[AgeForStat, 2]) == 0)
                {
                    Baly[8] = 2;
                }
                if ((app.stata[9] - TablicaNorm_W[AgeForStat, 2]) > 0)
                {
                    Baly[8] = 2 + (app.stata[9] - TablicaNorm_W[AgeForStat, 2]) * 2;
                }
                if (app.stata[9] - TablicaNorm_W[AgeForStat, 2] < 0) { Baly[8] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Динамическая сила",
                    rezultat = Convert.ToString(app.stata[9]),
                    norma = Convert.ToString(TablicaNorm_W[AgeForStat, 2]),
                    balli = Convert.ToString(Baly[8])
                });

                if (app.stata[11] - TablicaNorm_W[AgeForStat, 3] >= 0)
                {
                    Baly[9] = (app.stata[11] - (TablicaNorm_W[AgeForStat, 3] - 1)) * 3;
                }
                if (app.stata[11] - TablicaNorm_W[AgeForStat, 3] < 0) { Baly[9] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростная выносливость",
                    rezultat = Convert.ToString(app.stata[11]),
                    norma = Convert.ToString(TablicaNorm_W[AgeForStat, 3]),
                    balli = Convert.ToString(Baly[9])
                });

                if (app.stata[12] - TablicaNorm_W[AgeForStat, 4] >= 0)
                {
                    Baly[10] = (app.stata[12] - (TablicaNorm_W[AgeForStat, 4] - 1)) * 4;
                }
                if (app.stata[12] - TablicaNorm_W[AgeForStat, 4] < 0) { Baly[10] = 0; }
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростно-силовая выностивость",
                    rezultat = Convert.ToString(app.stata[12]),
                    norma = Convert.ToString(TablicaNorm_W[AgeForStat, 4]),
                    balli = Convert.ToString(Baly[10])
                });
                if (app.stata[1] > NormaVesa_W)
                {
                    Ves.Visibility = Visibility;
                }
                if (app.stata[5] > NormaSistDavleniya_W)
                {
                    SD.Visibility = Visibility;
                }
                if (app.stata[6] > NormaDiastDavleniya_W)
                {
                    DD.Visibility = Visibility;
                }
                if (app.stata[3] > 60)
                {
                    PulsVPokoe.Visibility = Visibility;
                }
                if (app.stata[3] + 10 < app.stata[4])
                {
                    VostPulsa.Visibility = Visibility;
                }
                if (app.stata[7] < TablicaNorm_W[AgeForStat, 0])
                {
                    Gibcost.Visibility = Visibility;
                }
                if (app.stata[8] > TablicaNorm_W[AgeForStat, 1])
                {
                    Bistrota.Visibility = Visibility;
                }
                if (app.stata[9] < TablicaNorm_W[AgeForStat, 2])
                {
                    DinamSila.Visibility = Visibility;
                }
                if (app.stata[11] < TablicaNorm_W[AgeForStat, 3])
                {
                    SV.Visibility = Visibility;
                }
                if (app.stata[12] < TablicaNorm_W[AgeForStat, 4])
                {
                    SSV.Visibility = Visibility;
                }

                string ItogoviyBal = "Ошибка";

                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] > 250) { ItogoviyBal = "Высокий"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 250) { ItogoviyBal = "Выше среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 160) { ItogoviyBal = "Средний"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] <= 90) { ItogoviyBal = "Ниже среднего"; }
                if (Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10] < 50) { ItogoviyBal = "Низкий"; }


                GridList.Add(new GridClass()
                {
                    nadpisi = "Ваш уровень физического состояния ",
                    rezultat = "",
                    norma = ItogoviyBal,
                    balli = Convert.ToString(Baly[0] + Baly[1] + Baly[2] + Baly[3] + Baly[4] + Baly[5] + Baly[6] + Baly[7] + Baly[8] + Baly[9] + Baly[10])
                });
            }
            dataGrid.ItemsSource = GridList;

        }

        HeaderFooter Excel.Page.LeftHeader => throw new NotImplementedException();

        HeaderFooter Excel.Page.CenterHeader => throw new NotImplementedException();

        HeaderFooter Excel.Page.RightHeader => throw new NotImplementedException();

        HeaderFooter Excel.Page.LeftFooter => throw new NotImplementedException();

        HeaderFooter Excel.Page.CenterFooter => throw new NotImplementedException();

        HeaderFooter Excel.Page.RightFooter => throw new NotImplementedException();

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dataGrid.Items.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[j + 1, 1];
                myRange = (Range)sheet1.Cells[2, 1];
                myRange.Value2 = app.Lichnost[0];
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange = (Range)sheet1.Cells[1, 1];
                myRange.Value2 = "Ф.И.О.";
                myRange = (Range)sheet1.Cells[j + 1, 2];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Включаем жирный текст
                sheet1.Columns[j + 1].ColumnWidth = 15; //ширина 

                if (j == 1)
                {
                    myRange.Value2 = dataGrid.Columns[1].Header;
                }
                if (j == 2)
                {
                    myRange.Value2 = dataGrid.Columns[2].Header;
                }
            }
            for (int i = 0; i < dataGrid.Columns.Count - 1; i++)    // перебор строк в exel таблице
            {
                for (int j = 0; j < dataGrid.Items.Count; j++)      // перебор столбцов в exel таблице
                {
                    if (j < 3)
                    {
                        TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 3];
                        myRange.Value2 = b.Text;
                    }
                    if (j > 3)
                    {
                        TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 1, j + 2];
                        myRange.Value2 = b.Text;
                    }
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string path = GetPath();
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            //excel.Workbooks.Open(path);
            Workbook workbook = excel.Workbooks.Open(path);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            
            int chek = 0;
            Range myRange;
            for (int i = 0; chek == 0; i++)
            {
                myRange = (Range)sheet1.Cells[i + 1, 1];
                if (myRange.Value == null)
                {
                    myRange = (Range)sheet1.Cells[2 + i, 1];
                    if (myRange.Value == null)
                    {
                        chek = i + 2;
                    }
                }
            }
            myRange = (Range)sheet1.Cells[chek, 1];
            myRange.Value2 = app.Lichnost[0];
            myRange = (Range)sheet1.Cells[chek, 2];
            myRange.Value2 = dataGrid.Columns[1].Header;
            myRange = (Range)sheet1.Cells[chek + 1, 2];
            myRange.Value2 = dataGrid.Columns[2].Header;
            //myRange.Cells[chek, 2].Value2 = dataGrid.Columns[1].Header;
            //myRange.Cells[chek + 1, 2].Value2 = dataGrid.Columns[2].Header;

            for (int i = 0; i < dataGrid.Columns.Count - 2; i++)    // перебор строк в exel таблице
            {
                for (int j = 0; j < dataGrid.Items.Count; j++)      // перебор столбцов в exel таблице
                {
                    if (j < 3)
                    {
                        TextBlock b = dataGrid.Columns[i + 1].GetCellContent(dataGrid.Items[j]) as TextBlock;
                        myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + chek, j + 3];
                        myRange.Value2 = b.Text;
                    }
                    if (j > 3)
                    {
                        TextBlock b = dataGrid.Columns[i + 1].GetCellContent(dataGrid.Items[j]) as TextBlock;
                        myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + chek, j + 2];
                        myRange.Value2 = b.Text;
                    }
                }
            }
            workbook.Save();
            excel.Quit();

        }
        public string GetPath()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (/*dialog.ShowDialog() == DialogResult.OK*/true)
            {
                return dialog.FileName;
            }
            //return null;
        }
    }
}

