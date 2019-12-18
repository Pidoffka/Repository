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
            if(LoginBox.Text == "")
            {
                MessageBox.Show("Enter login please", "Error");
                return;
            }
            if (PasswordBox.Password == "")
            {
                MessageBox.Show("Enter password please", "Error");
                return;
            }
            User user = repository.checkUser(LoginBox.Text);
            if (repository.checkUser(LoginBox.Text) != null)
            {
                var window = new Manager(user, repository);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Such username and password do not exist", "Error");
                return;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new RegistrationPage(repository);
            window.ShowDialog();
        }
    }
}
