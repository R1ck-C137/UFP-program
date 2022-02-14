﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Приложение_по_физре.Страницы_отценки
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        App app = (App)Application.Current;
        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page3.xaml", UriKind.Relative));
            if (app.stata.Count <= 1)
            {
                
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tb1.Text));  // stata[1]
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.stata.Add(Convert.ToDouble(tb2.Text));  // stata[2]
            }
            else
            {
                if (tb1.Text == "")
                {
                    tb1.Text = "-1";
                }
                app.stata.RemoveAt(1);
                app.stata.Insert(1, Convert.ToDouble(tb1.Text));
                if (tb2.Text == "")
                {
                    tb2.Text = "-1";
                }
                app.stata.RemoveAt(2);
                app.stata.Insert(2, Convert.ToDouble(tb2.Text));
            }
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Страницы отценки/Page1.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.stata.Count >= 2)
            {
                if (app.stata[1] == -1)
                {
                    tb1.Text = "";
                }
                else
                {
                    tb1.Text = Convert.ToString(app.stata[1]);
                }

                if (app.stata[2] == -1)
                {
                    tb2.Text = "";
                }
                else
                {
                    tb2.Text = Convert.ToString(app.stata[2]);
                }
            }
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void tb2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
