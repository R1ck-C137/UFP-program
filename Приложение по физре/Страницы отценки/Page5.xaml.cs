using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page4.xaml", UriKind.Relative));
        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page6.xaml", UriKind.Relative));

            /*if (app.Indication.Count <= 5)
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb1.Text));  // Indication[5]
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb2.Text));  // Indication[6]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.RemoveAt(5);
                app.Indication.Insert(5, Convert.ToDouble(tb1.Text));
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.Indication.RemoveAt(6);
                app.Indication.Insert(6, Convert.ToDouble(tb2.Text));
            }*/
            if (tb_SystolicPressure.Text != "")
                app.person.SystolicPressure = Convert.ToInt32(tb_SystolicPressure.Text);
            if (tb_DiastolicPressure.Text != "")
                app.person.DiastolicPressure = Convert.ToInt32(tb_DiastolicPressure.Text);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            /*if (app.Indication.Count >= 6)
            {
                if (app.Indication[5] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.Indication[5]);
                }

                if (app.Indication[6] == -1)
                {
                    tb2.Text = "";
                }
                else
                {
                    tb2.Text = Convert.ToString(app.Indication[6]);
                }
            }*/
            tb_SystolicPressure.Text = Convert.ToString(app.person.SystolicPressure);
            tb_DiastolicPressure.Text = Convert.ToString(app.person.DiastolicPressure);
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
