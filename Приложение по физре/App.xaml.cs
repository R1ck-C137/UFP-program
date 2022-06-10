using System.Collections.Generic;
using System.Windows;

namespace Приложение_по_физре
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<string> Person = new List<string>();
        public List<double> Indication = new List<double>();
        public bool Gender = true;
        public bool Sport = false;
        public bool GroupMode = false;
        public string FilePath;
    }
}
