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
    /// Логика взаимодействия для Nachalnaya.xaml
    /// </summary>
    public partial class Nachalnaya : Page
    {
        public Nachalnaya()
        {
            InitializeComponent();
        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page1.xaml", UriKind.Relative));
        }
    }
}
