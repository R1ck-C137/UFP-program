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
        public class Stat                            // Расчёт итоговых очков у мужчин
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public double Age { get; set; }
            public Stat() { }                            // Пустой конструктор
            public Stat(string n, string g, double a)      // Конструктор с параметрами
            {
                Name = n;
                Group = g;
                Age = a;
            }
        }
        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            list.Clear();
            if (list.Count == 0)
            {
                list.Add(new Stat
                {
                    Name = app.Lichnost[0],
                    Group = app.Lichnost[1],
                    Age = app.stata[0]
                });
            }
            dataGrid.ItemsSource = list;
        }
    }
}
