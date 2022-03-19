﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page11.xaml
    /// </summary>
    public partial class Page11 : Page
    {
        public Page11()
        {
            InitializeComponent();
        }
        App app = (App)Application.Current;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page10.xaml", UriKind.Relative));

        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            

            if (app.Indication.Count <= 12)
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.Add(Convert.ToDouble(tb1.Text));  // Indication[12]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.Indication.RemoveAt(12);
                app.Indication.Insert(12, Convert.ToDouble(tb1.Text));
            }
            if (app.Indication[0] == -1 || app.Indication[1] == -1 || app.Indication[2] == -1 || app.Indication[3] == -1 || app.Indication[4] == -1 || app.Indication[5] == -1 || app.Indication[6] == -1 || app.Indication[7] == -1 || app.Indication[8] == -1 || app.Indication[9] == -1 || app.Indication[10] == -1 || app.Indication[11] == -1 || app.Indication[12] == -1)
            //if(false)
            {
                if (tb1.Text == "-1")
                {
                    tb1.Text = "";
                }
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                NavigationService.Navigate(new Uri("/../Itogi.xaml", UriKind.Relative));
            }
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.Indication.Count >= 13)
            {
                if (app.Indication[12] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.Indication[12]);
                }
            }
        }
    }
}
