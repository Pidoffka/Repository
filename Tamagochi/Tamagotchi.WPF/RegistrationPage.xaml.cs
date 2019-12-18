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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        private Repository _repository;
        private User _user;
        public RegistrationPage(Repository repository)
        {
            InitializeComponent();
            _repository = repository;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                MessageBox.Show("Enter Name please", "Error");
                return;
            }
            if (LoginBox.Text == "")
            {
                MessageBox.Show("Enter login please", "Error");
                return;
            }
            if (PasswordBox.Password == "")
            {
                MessageBox.Show("Enter password please", "Error");
                return;
            }
            _user = _repository.checkUser<string>(LoginBox.Text, null);
            if(_user != null)
            {
                MessageBox.Show("This user is already exist", "Error");
                return;
            }
            _repository.newUser(NameBox.Text, LoginBox.Text, PasswordBox.Password);
            MessageBox.Show("Registration is completed", "Success");
        }
    }
}
