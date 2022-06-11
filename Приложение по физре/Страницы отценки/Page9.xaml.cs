using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page9.xaml
    /// </summary>
    public partial class Page9 : Page
    {
        public Page9()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page8.xaml", UriKind.Relative));
        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page10.xaml", UriKind.Relative));

            /*if (app.Indication.Count <= 10)
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb1.Text));  // Indication[10]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.RemoveAt(10);
                app.Indication.Insert(10, Convert.ToDouble(tb1.Text));
            }*/
            if (tb_OverallEndurance.Text != "")
                app.person.OverallEndurance = Convert.ToInt32(tb_OverallEndurance.Text);

            if (rb1.IsChecked == true)
            {
                //app.Sport = true;
                app.person.Sport = true;
            }
            if (rb2.IsChecked == true)
            {
                //app.Sport = false;
                app.person.Sport = false;
            }
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            /*if (app.Sport == true)
            {
                rb1.IsChecked = true;
            }
            if (app.Sport == false)
            {
                rb2.IsChecked = true;
            }*/
            /*if (app.Indication.Count >= 11)
            {
                if (app.Indication[10] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.Indication[10]);
                }
            }*/
            if (app.person.Sport == true)
            {
                rb1.IsChecked = true;
            }
            if (app.person.Sport == false)
            {
                rb2.IsChecked = true;
            }
            
            tb_OverallEndurance.Text = Convert.ToString(app.person.OverallEndurance);
        }
    }
}
