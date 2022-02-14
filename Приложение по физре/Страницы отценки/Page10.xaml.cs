using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page10.xaml
    /// </summary>
    public partial class Page10 : Page
    {
        public Page10()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;
        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page9.xaml", UriKind.Relative));
        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page11.xaml", UriKind.Relative));

            if (app.stata.Count <= 11)
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tb1.Text));  // stata[11]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.RemoveAt(11);
                app.stata.Insert(11, Convert.ToDouble(tb1.Text));
            }
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.stata.Count >= 12)
            {
                if (app.stata[11] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.stata[11]);
                }
            }
        }
    }
}
