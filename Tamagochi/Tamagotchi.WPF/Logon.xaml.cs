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
    
    public partial class Logon : Page
    {
            public Logon()
            {
                InitializeComponent();
            }

            private void RegisterButton_Click(object sender, RoutedEventArgs e)
            {
                MainWindow.Current.Content = new RegisterPage(MainWindow.Current.Content);
            }

            private void LoginButton_Click(object sender, RoutedEventArgs e)
            {
#if NO_LOGIN
            MainWindow.Current.Content = new SessionsPage(App.Current.ParkingManager.Users.FirstOrDefault());
#else
                var user = App.Current.Repositiry.Users.SingleOrDefault(u => u.Login == LoginBox.Text && u.Password == PasswordBox.Password);
                if (user is null)
                {
                    MessageBox.Show("Wrong phone or password", "Logon failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MainWindow.Current.Content = new SessionsPage(user);
                }

#endif
            }
        
    }
}
