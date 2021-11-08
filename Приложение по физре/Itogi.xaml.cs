using System;
using System.Collections.Generic;
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

namespace Приложение_по_физре
{
    /// <summary>
    /// Логика взаимодействия для Itogi.xaml
    /// </summary>
    public partial class Itogi : Page
    {
        public Itogi()
        {
            InitializeComponent();
        }

        static public List<Stat> list = new List<Stat>();
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
            { 8, 18, 33, 12, 17, 1670, 10.47},  //29
        };

        public class Stat                            // Расчёт итоговых очков у мужчин
        {
            public bool Gender { get; set; }
            public double Age { get; set; }
            public Stat() { }                            // Пустой конструктор
            public Stat(string n, string g, double a)      // Конструктор с параметрами
            {
                
            }
        }

        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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
                    //balli = 
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Возраст",
                    rezultat = Convert.ToString(app.stata[0]),
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Масса тела",
                    rezultat = Convert.ToString(app.stata[1]),
                    norma = Convert.ToString(50 + (app.stata[2] - 150) * 0.75 + (app.stata[0] - 21 / 4)),
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Системное артериальное давление",
                    rezultat = "",
                    //balli =
                }); 
                GridList.Add(new GridClass()
                {
                    nadpisi = "     Систолическое давление",
                    rezultat = Convert.ToString(app.stata[5]),
                    norma = Convert.ToString( 109 + 0.5 * app.stata[0] + 0.1 * app.stata[1] ),
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "     Диастолическое давление",
                    rezultat = Convert.ToString(app.stata[6]),
                    norma = Convert.ToString(74 + 0.1 * app.stata[0] + 0.15 * app.stata[1]),
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Пульс в покое",
                    rezultat = Convert.ToString(app.stata[3]),
                    norma = "60",
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Общая выносливость",
                    rezultat = Convert.ToString(app.stata[10]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Востанавливваемость пульса",
                    rezultat = Convert.ToString(app.stata[4]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Гибкость",
                    rezultat = Convert.ToString(app.stata[7]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat,0]),
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Быстрота",
                    rezultat = Convert.ToString(app.stata[8]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 1])
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Динамическая сила",
                    rezultat = Convert.ToString(app.stata[9]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 2])
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростная выносливость",
                    rezultat = Convert.ToString(app.stata[11]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 3])
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростно-силовая выностивость",
                    rezultat = Convert.ToString(app.stata[12]),
                    norma = Convert.ToString(TablicaNorm_M[AgeForStat, 4])
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Ваш уровень физического состояния ",
                    rezultat = "",
                    //norma = ,
                    //balli =
                });
            }

            //-----------------------------------------------------------------------------------------------

            else
            {
                GridList.Add(new GridClass()// добавляем строки в таблицу
                {
                    nadpisi = "Рост",
                    rezultat = Convert.ToString(app.stata[2]),
                    //balli = 
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Возраст",
                    rezultat = Convert.ToString(app.stata[0]),
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Масса тела",
                    rezultat = Convert.ToString(app.stata[1]),
                    norma = Convert.ToString(50 + (app.stata[2] - 150) * 0.32 + (app.stata[0] - 21 / 5)),
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Системное артериальное давление",
                    rezultat = "",
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "   Систолическое давление",
                    rezultat = Convert.ToString(app.stata[5]),
                    norma = Convert.ToString(102 + 0.7 * app.stata[0] + 0.15 * app.stata[1]),
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "   Диастолическое давление",
                    rezultat = Convert.ToString(app.stata[6]),
                    norma = Convert.ToString(78 + 0.17 * app.stata[0] + 0.1 * app.stata[1]),
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Пульс в покое",
                    rezultat = Convert.ToString(app.stata[3]),
                    norma = "60",
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Общая выносливость",
                    rezultat = Convert.ToString(app.stata[10]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Востанавливваемость пульса",
                    rezultat = Convert.ToString(app.stata[4]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Гибкость",
                    rezultat = Convert.ToString(app.stata[7]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Быстрота",
                    rezultat = Convert.ToString(app.stata[8]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Динамическая сила",
                    rezultat = Convert.ToString(app.stata[9]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростная выносливость",
                    rezultat = Convert.ToString(app.stata[11]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Скоростно-силовая выностивость",
                    rezultat = Convert.ToString(app.stata[12]),
                    //norma = ,
                    //balli =
                });
                GridList.Add(new GridClass()
                {
                    nadpisi = "Ваш уровень физического состояния ",
                    rezultat = "",
                    //norma = ,
                    //balli =
                });
            }
            dataGrid.ItemsSource = GridList;
        }
    }
}
