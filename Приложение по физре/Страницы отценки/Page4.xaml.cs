using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace UFP_program.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page5.xaml", UriKind.Relative));
            
            if (tb_PulseAfterExercise.Text != "")
                app.person.PulseAfterExercise = Convert.ToInt32(tb_PulseAfterExercise.Text);
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page3.xaml", UriKind.Relative));
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tb_PulseAfterExercise.Text = Convert.ToString(app.person.PulseAfterExercise);
        }
    }
}
