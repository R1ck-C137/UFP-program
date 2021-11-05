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

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        public Page6()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page5.xaml", UriKind.Relative));
        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page7.xaml", UriKind.Relative));
            if (app.stata.Count <= 7)
            {
                //app.stata.Add(Convert.ToDouble(tb1.Text));
                //app.stata.Add(Convert.ToDouble(tb2.Text));
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tb1.Text));  // stata[7]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.Insert(7, Convert.ToDouble(tb1.Text));
            }
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.stata.Count >= 8)
            {
                if (app.stata[7] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.stata[7]);
                }
            }
        }
    }
}
