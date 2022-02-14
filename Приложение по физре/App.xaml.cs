using System.Collections.Generic;
using System.Windows;

namespace Приложение_по_физре
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<string> Lichnost = new List<string>();
        public List<double> stata = new List<double>();
        public bool Gender = true;
        public bool Sport = false;
        public bool Gruppa = false;
        public string path;
    }
}
