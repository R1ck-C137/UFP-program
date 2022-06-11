using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        App app = (App)Application.Current;
        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Weight.Text != "")
                app.person.Weight = Convert.ToInt32(tb_Weight.Text);

            if (tb_Height.Text != "")
                app.person.Height = Convert.ToInt32(tb_Height.Text);

            NavigationService.Navigate(new Uri("/../Страницы отценки/Page3.xaml", UriKind.Relative));
            /*if (app.Indication.Count <= 1)
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb1.Text));  // Indication[1]
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb2.Text));  // Indication[2]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.RemoveAt(1);
                app.Indication.Insert(1, Convert.ToDouble(tb1.Text));
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.Indication.RemoveAt(2);
                app.Indication.Insert(2, Convert.ToDouble(tb2.Text));
            }*/

        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page1.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            /*if (app.Indication.Count >= 2)
            {
                if (app.Indication[1] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.Indication[1]);
                }

                if (app.Indication[2] == -1)
                {
                    tb2.Text = "";
                }
                else
                {
                    tb2.Text = Convert.ToString(app.Indication[2]);
                }
            }*/
            tb_Weight.Text = Convert.ToString(app.person.Weight);
            tb_Height.Text = Convert.ToString(app.person.Height);
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
