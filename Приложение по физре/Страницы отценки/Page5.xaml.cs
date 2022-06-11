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

            
            if (tb_SystolicPressure.Text != "")
                app.person.SystolicPressure = Convert.ToInt32(tb_SystolicPressure.Text);
            if (tb_DiastolicPressure.Text != "")
                app.person.DiastolicPressure = Convert.ToInt32(tb_DiastolicPressure.Text);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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
