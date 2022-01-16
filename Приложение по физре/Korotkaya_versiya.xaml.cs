using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Приложение_по_физре
{
    /// <summary>
    /// Логика взаимодействия для Korotkaya_versiya.xaml
    /// </summary>
    public partial class Korotkaya_versiya : Page
    {
        public Korotkaya_versiya()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void b_nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void b_dalee_Click(object sender, RoutedEventArgs e)
        {
            if (tb_stata0.Text == "" || tb_stata1.Text == "" || tb_stata2.Text == "" || tb_stata3.Text == "" || tb_stata4.Text == "" || tb_stata5.Text == "" || tb_stata6.Text == "" || tb_stata7.Text == "" || tb_stata8.Text == "" || tb_stata9.Text == "" || tb_stata10.Text == "" || tb_stata11.Text == "" || tb_stata12.Text == "" || tb_Lichnost0.Text == "" || tb_Lichnost1.Text == "")
            {
                MessageBox.Show("Не все поля заполненны!");
            }
            else
            {
                NavigationService.Navigate(new Uri("/../Itogi.xaml", UriKind.Relative));

                app.stata.Add(Convert.ToDouble(tb_stata0.Text));
                app.stata.Add(Convert.ToDouble(tb_stata1.Text));
                app.stata.Add(Convert.ToDouble(tb_stata2.Text));
                app.stata.Add(Convert.ToDouble(tb_stata3.Text));
                app.stata.Add(Convert.ToDouble(tb_stata4.Text));
                app.stata.Add(Convert.ToDouble(tb_stata5.Text));
                app.stata.Add(Convert.ToDouble(tb_stata6.Text));
                app.stata.Add(Convert.ToDouble(tb_stata7.Text));
                app.stata.Add(Convert.ToDouble(tb_stata8.Text));
                app.stata.Add(Convert.ToDouble(tb_stata9.Text));
                app.stata.Add(Convert.ToDouble(tb_stata10.Text));
                app.stata.Add(Convert.ToDouble(tb_stata11.Text));
                app.stata.Add(Convert.ToDouble(tb_stata12.Text));

                app.Lichnost.Add(Convert.ToString(tb_Lichnost0.Text));
                app.Lichnost.Add(Convert.ToString(tb_Lichnost1.Text));

                if (cb_Gender.Text == "Мужской")
                {
                    app.Gender = true;
                }
                if (cb_Gender.Text == "Женский")
                {
                    app.Gender = false;
                }
            }
        }

        private void tb_stata0_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata5_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata6_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata7_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata8_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata9_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata10_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata11_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_stata12_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
