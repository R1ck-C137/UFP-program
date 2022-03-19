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
            
            if (app.Indication.Count == 0)
            {
                app.Person.Add(tbName.Text);
                app.Person.Add(tbGroup.Text);
                if (tbAge.Text == "") 
                {
                    tbAge.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tbAge.Text));
            }
            else
            {
                app.Person.RemoveAt(0);
                app.Person.Insert(0, tbName.Text);
                app.Person.RemoveAt(1);
                app.Person.Insert(1, tbGroup.Text);
                if (tbAge.Text == "")
                {
                    tbAge.Text = "-1";
                }
                app.Indication.RemoveAt(0);
                app.Indication.Insert(0, Convert.ToDouble(tbAge.Text));
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
            if (app.Indication.Count != 0)
            {
                tbName.Text = app.Person[0];
                tbGroup.Text = app.Person[1];
                
                
                if (app.Indication[0] == -1)
                {
                    tbAge.Text = "";
                }
                else
                {
                    tbAge.Text = Convert.ToString(app.Indication[0]);
                }
                //app.Indication.Add(Convert.ToDouble(tbAge.Text));
            }
        }

        private void tbAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
