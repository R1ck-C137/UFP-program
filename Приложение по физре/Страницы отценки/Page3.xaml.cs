using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace UFP_program.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page4.xaml", UriKind.Relative));

            if (tb_PulseAtRest.Text != "")
                app.person.PulseAtRest = Convert.ToInt32(tb_PulseAtRest.Text);
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page2.xaml", UriKind.Relative));
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tb_PulseAtRest.Text = Convert.ToString(app.person.PulseAtRest);
        }
    }
}
