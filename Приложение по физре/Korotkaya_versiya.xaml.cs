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
        Person person = new Person();
        App app = (App)Application.Current;

        public Korotkaya_versiya()
        {
            InitializeComponent();
        }
        

        private void b_nazad_Click(object sender, RoutedEventArgs e)
        {
            if(app.GroupMode == true)
            {
                app.GroupMode = false;
                app.FilePath = null;
            }
            NavigationService.Navigate(new Uri("/../Nachalnaya.xaml", UriKind.Relative));
        }

        private void b_dalee_Click(object sender, RoutedEventArgs e)
        {
            if (tb_FIO.Text == "" || tb_Group.Text == "" || tb_Height.Text == "" ||
                tb_PulseAtRest.Text == "" || tb_PulseAfterExercise.Text == "" || tb_SystolicPressure.Text == "" ||
                tb_DiastolicPressure.Text == "" || tb_Flexibility.Text == "" || tb_Speed.Text == "" ||
                tb_DynamicForce.Text == "" || tb_OverallEndurance.Text == "" || tb_SpeedEndurance.Text == "" ||
                tb_SpeedAndStrengthEndurance.Text == "" || tb_Weight.Text == "" || tb_Height.Text == "")
            {
                MessageBox.Show("Не все поля заполненны!");
            }
            else
            {
                app.Person.Add(Convert.ToString(tb_FIO.Text));
                app.Person.Add(Convert.ToString(tb_Group.Text));
                app.Indication.Add(Convert.ToDouble(tb_Age.Text));
                app.Indication.Add(Convert.ToDouble(tb_Weight.Text));
                app.Indication.Add(Convert.ToDouble(tb_Height.Text));
                app.Indication.Add(Convert.ToDouble(tb_PulseAtRest.Text));
                app.Indication.Add(Convert.ToDouble(tb_PulseAfterExercise.Text));
                app.Indication.Add(Convert.ToDouble(tb_SystolicPressure.Text));
                app.Indication.Add(Convert.ToDouble(tb_DiastolicPressure.Text));
                app.Indication.Add(Convert.ToDouble(tb_Flexibility.Text));
                app.Indication.Add(Convert.ToDouble(tb_Speed.Text));
                app.Indication.Add(Convert.ToDouble(tb_DynamicForce.Text));
                app.Indication.Add(Convert.ToDouble(tb_OverallEndurance.Text));
                app.Indication.Add(Convert.ToDouble(tb_SpeedEndurance.Text));
                app.Indication.Add(Convert.ToDouble(tb_SpeedAndStrengthEndurance.Text));

                person.FIO = tb_FIO.Text;
                person.Group = tb_Group.Text;
                person.Age = Convert.ToInt32(tb_Age.Text);
                person.Weight = Convert.ToInt32(tb_Weight.Text);
                person.Height = Convert.ToInt32(tb_Height.Text);
                person.PulseAtRest = Convert.ToInt32(tb_PulseAtRest.Text);
                person.PulseAfterExercise = Convert.ToInt32(tb_PulseAfterExercise.Text);
                person.SystolicPressure = Convert.ToInt32(tb_SystolicPressure.Text);
                person.DiastolicPressure = Convert.ToInt32(tb_DiastolicPressure.Text);
                person.Flexibility = Convert.ToInt32(tb_Flexibility.Text);
                person.Speed = Convert.ToInt32(tb_Speed.Text);
                person.DynamicForce = Convert.ToInt32(tb_DynamicForce.Text);
                person.OverallEndurance = Convert.ToInt32(tb_OverallEndurance.Text);
                person.SpeedEndurance = Convert.ToInt32(tb_SpeedEndurance.Text);
                person.SpeedAndStrengthEndurance = Convert.ToInt32(tb_SpeedAndStrengthEndurance.Text);

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

        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            app.Indication.Clear();
            app.Person.Clear();
            person.Clear();
            cb_Gender.SelectedIndex = 0;
        }

        private void tb_Age_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Weight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Height_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_PulseAtRest_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_PulseAfterExercise_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_SystolicPressure_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_DiastolicPressure_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Flexibility_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_Speed_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_DynamicForce_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_OverallEndurance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_SpeedEndurance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb_SpeedAndStrengthEndurance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
