using System;
using System.Collections;
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

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }
        App app = (App)Application.Current;

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page2.xaml", UriKind.Relative));
            
            if (app.stata.Count == 0)
            {
                app.Lichnost.Add(tbName.Text);
                app.Lichnost.Add(tbGroup.Text);
                if (tbAge.Text == "") 
                {
                    tbAge.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tbAge.Text));
            }
            else
            {
                app.Lichnost.RemoveAt(0);
                app.Lichnost.Insert(0, tbName.Text);
                app.Lichnost.RemoveAt(1);
                app.Lichnost.Insert(1, tbGroup.Text);
                if (tbAge.Text == "")
                {
                    tbAge.Text = "-1";
                }
                app.stata.RemoveAt(0);
                app.stata.Insert(0, Convert.ToDouble(tbAge.Text));
            }
            if (cb.Text == "Мужской")
            {
                app.Gender = true;
            }
            if (cb.Text == "Женский")
            {
                app.Gender = false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.stata.Count != 0)
            {
                tbName.Text = app.Lichnost[0];
                tbGroup.Text = app.Lichnost[1];
                
                
                if (app.stata[0] == -1)
                {
                    tbAge.Text = "";
                }
                else
                {
                    tbAge.Text = Convert.ToString(app.stata[0]);
                }
                //app.stata.Add(Convert.ToDouble(tbAge.Text));
            }
        }

        private void tbAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }

    public class Student_M                            // Расчёт итоговых очков у мужчин
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quan { get; set; }
        public int Res { get; set; }                    // Итог по строке
        public Student_M() { }                            // Пустой конструктор
        public Student_M(string n, int p, int q)      // Конструктор с параметрами
        {
            Name = n;
            Price = p;
            Quan = q;
            Res = p * q;                            // Вычисление стоимости строки
        }
    }

    public class Student_W                            // Расчёт итоговых очков у женщин
    {
        public int Age { get; set; }
        public int Res { get; set; }
        public Student_W() { }                            // Пустой конструктор
        public Student_W(int n, int p, int q)      // Конструктор с параметрами
        {
            // Вычисление стоимости строки
        }
    }
}
