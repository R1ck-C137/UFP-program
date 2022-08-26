using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace UFP_program.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page8.xaml
    /// </summary>
    public partial class Page8 : Page
    {
        public Page8()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page7.xaml", UriKind.Relative));
        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page9.xaml", UriKind.Relative));

            if (tb_DynamicForce.Text != "")
                app.person.DynamicForce = Convert.ToInt32(tb_DynamicForce.Text);
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tb_DynamicForce.Text = Convert.ToString(app.person.DynamicForce);
        }
    }
}
