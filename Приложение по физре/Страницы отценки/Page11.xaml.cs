using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page11.xaml
    /// </summary>
    public partial class Page11 : Page
    {
        public Page11()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page10.xaml", UriKind.Relative));

        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            if (tb_SpeedAndStrengthEndurance.Text != "")
                app.person.SpeedAndStrengthEndurance = Convert.ToInt32(tb_SpeedAndStrengthEndurance.Text);

            if (app.person.CheckingTheFullness())
            {
                NavigationService.Navigate(new Uri("/../Results.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tb_SpeedAndStrengthEndurance.Text = Convert.ToString(app.person.SpeedAndStrengthEndurance);
        }
    }
}
