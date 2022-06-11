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
            /*if (app.Indication.Count <= 12)
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb1.Text));  // Indication[12]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.RemoveAt(12);
                app.Indication.Insert(12, Convert.ToDouble(tb1.Text));
            }*/
            if (tb_SpeedAndStrengthEndurance.Text != "")
                app.person.SpeedAndStrengthEndurance = Convert.ToInt32(tb_SpeedAndStrengthEndurance.Text);

            if (app.person.CheckingTheFullness())
            {
                NavigationService.Navigate(new Uri("/../Itogi.xaml", UriKind.Relative));
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
            /*if (app.Indication.Count >= 13)
            {
                if (app.Indication[12] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.Indication[12]);
                }
            }*/
            tb_SpeedAndStrengthEndurance.Text = Convert.ToString(app.person.SpeedAndStrengthEndurance);
        }
    }
}
