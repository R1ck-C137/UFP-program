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
using System.Windows.Navigation;

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
            if(app.Gruppa == true)
            {
                app.Gruppa = false;
                app.path = null;
            }
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void b_dalee_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Indication0.Text == "" || tb_Indication1.Text == "" || tb_Indication2.Text == "" || tb_Indication3.Text == "" || tb_Indication4.Text == "" || tb_Indication5.Text == "" || tb_Indication6.Text == "" || tb_Indication7.Text == "" || tb_Indication8.Text == "" || tb_Indication9.Text == "" || tb_Indication10.Text == "" || tb_Indication11.Text == "" || tb_Indication12.Text == "" || tb_Lichnost0.Text == "" || tb_Lichnost1.Text == "")
            {
                MessageBox.Show("Не все поля заполненны!");
            }
            else
            {
                app.Indication.Add(Convert.ToDouble(tb_Indication0.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication1.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication2.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication3.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication4.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication5.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication6.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication7.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication8.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication9.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication10.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication11.Text));
                app.Indication.Add(Convert.ToDouble(tb_Indication12.Text));

                app.Person.Add(Convert.ToString(tb_Lichnost0.Text));
                app.Person.Add(Convert.ToString(tb_Lichnost1.Text));

                if (cb_Gender.Text == "Мужской")
                {
                    app.Gender = true;
                }
                if (cb_Gender.Text == "Женский")
                {
                    app.Gender = false;
                }
                
                NavigationService.Navigate(new Uri("/../Itogi.xaml", UriKind.Relative));
            }
        }

        private void tb_Indication0_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication5_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication6_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication7_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication8_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication9_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication10_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication11_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Indication12_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            app.Indication.Clear();
            app.Person.Clear();
            cb_Gender.SelectedIndex = 0;
        }


    }
}
