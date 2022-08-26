using System.Collections.Generic;
using System.Windows;

namespace UFP_program
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public bool GroupMode = false;
        public string FilePath;
        public Person person = new Person();
        public Point point = new Point();
        //public string TotalScore = "Ошибка";
    }
}
