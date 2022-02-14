using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        
        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }
        App app = (App)Application.Current;

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page2.xaml", UriKind.Relative));
            
            if (app.stata.Count == 0)
            {
                app.Lichnost.Add(tbName.Text);
                app.Lichnost.Add(tbGroup.Text);
                if (tbAge.Text == "") 
                {
                    tbAge.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tbAge.Text));
            }
            else
            {
                app.Lichnost.RemoveAt(0);
                app.Lichnost.Insert(0, tbName.Text);
                app.Lichnost.RemoveAt(1);
                app.Lichnost.Insert(1, tbGroup.Text);
                if (tbAge.Text == "")
                {
                    tbAge.Text = "-1";
                }
                app.stata.RemoveAt(0);
                app.stata.Insert(0, Convert.ToDouble(tbAge.Text));
            }
            if (cb.Text == "Мужской")
            {
                app.Gender = true;
            }
            if (cb.Text == "Женский")
            {
                app.Gender = false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.Gender == true)             //Мужской
            {
                cb.SelectedIndex = 0;
            }
            else
            {
                cb.SelectedIndex = 1;
            }
            if (app.stata.Count != 0)
            {
                tbName.Text = app.Lichnost[0];
                tbGroup.Text = app.Lichnost[1];
                
                
                if (app.stata[0] == -1)
                {
                    tbAge.Text = "";
                }
                else
                {
                    tbAge.Text = Convert.ToString(app.stata[0]);
                }
                //app.stata.Add(Convert.ToDouble(tbAge.Text));
            }
        }

        private void tbAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
