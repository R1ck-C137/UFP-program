using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace Приложение_по_физре.Страницы_отценки
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        
        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../InitialPage.xaml", UriKind.Relative));
        }
        App app = (App)Application.Current;
        
        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page2.xaml", UriKind.Relative));

            app.person.FIO = tbName.Text;
            app.person.Group = tbGroup.Text;
            if (tbAge.Text != "")
                app.person.Age = Convert.ToInt32(tbAge.Text);

            if (cb.SelectedIndex == 0)
            {
                app.person.Gender = true;
            }
            if (cb.SelectedIndex == 1)
            {
                app.person.Gender = false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.person.Gender)
            {
                cb.SelectedIndex = 0;
            }
            else
            {
                cb.SelectedIndex = 1;
            }

            tbName.Text = app.person.FIO;
            tbGroup.Text = app.person.Group;
            tbAge.Text = Convert.ToString(app.person.Age);
        }

        private void tbAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
