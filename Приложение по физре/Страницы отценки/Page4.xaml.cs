using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
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
            /*if (app.Indication.Count <= 4)
            {
                //app.Indication.Add(Convert.ToDouble(tb1.Text));
                //app.Indication.Add(Convert.ToDouble(tb2.Text));
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb1.Text));  // Indication[4]

            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.RemoveAt(4);
                app.Indication.Insert(4, Convert.ToDouble(tb1.Text));
            }*/
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
            /*if (app.Indication.Count >= 5)
            {
                if (app.Indication[4] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.Indication[4]);
                }
            }*/
            tb_PulseAfterExercise.Text = Convert.ToString(app.person.PulseAfterExercise);
        }
    }
}
