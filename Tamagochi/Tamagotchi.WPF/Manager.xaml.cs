using System;
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
using Tamagochi.Repository.Data;

namespace Tamagotchi.WPF
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        private Repository _repository;
        private User _user;
        public Manager(User user, Repository repository)
        {
            InitializeComponent();
            _user = user;
            _repository = repository;
            Level.Value = _user.Exp;
            Level.Maximum = _user.ExpLevel;
            NameBox.Text = _user.Name;
            LevelB.Text = _user.Level.ToString();
            MoneyBox.Text = _user.Money.ToString();
            TypeOfItems.ItemsSource = _repository.TypeOfItems;
        }

        private void FeedButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
