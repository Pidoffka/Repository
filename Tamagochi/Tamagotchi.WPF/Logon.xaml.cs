﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tamagochi.Repository;

namespace Tamagotchi.WPF
{
    /// <summary>
    /// Логика взаимодействия для LogOn.xaml
    /// </summary>
    public partial class LogOn : Window
    {
        private Repository repository = new Repository();
        public LogOn()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
