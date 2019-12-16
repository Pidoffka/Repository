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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tamagotchi.WPF
{
    public partial class RegisterPage : Page
    {
        private object _parentContent;
        public RegisterPage()
        {
            InitializeComponent();
            _parentContent = parent;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Current.Content = _parentContent;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.Current.ParkingManager.Users.FindIndex(u => u.Login == LoginBox.Text) != -1)
            {
                Helpers.ShowError("User with same login already exists", "Registration failed");
                return;
            }

            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                Helpers.ShowError("Name must not be empty", "Registration failed");
                return;
            }

            if (string.IsNullOrWhiteSpace(LoginBox.Text))
            {
                Helpers.ShowError("Login must not be empty", "Registration failed");
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                Helpers.ShowError("Password must not be empty", "Registration failed");
                return;
            }

            var registered = new User
            {
                Id = App.Current.ParkingManager.Users.Max(u => u.Id) + 1,
                CarPlateNumber = CarPlateBox.Text,
                Name = NameBox.Text,
                Password = PasswordBox.Password,
                Phone = PhoneBox.Text,
            };
            App.Current.ParkingManager.Users.Add(registered);
            App.Current.ParkingManager.SaveUsers();
            MainWindow.Current.Content = new SessionsPage(registered);
        }
    }
}
