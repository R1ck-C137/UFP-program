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

            if (app.stata.Count <= 5)
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tb1.Text));  // stata[5]
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tb2.Text));  // stata[6]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.RemoveAt(5);
                app.stata.Insert(5, Convert.ToDouble(tb1.Text));
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.stata.RemoveAt(6);
                app.stata.Insert(6, Convert.ToDouble(tb2.Text));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.stata.Count >= 6)
            {
                if (app.stata[5] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.stata[5]);
                }

                if (app.stata[6] == -1)
                {
                    tb2.Text = "";
                }
                else
                {
                    tb2.Text = Convert.ToString(app.stata[6]);
                }
            }
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
