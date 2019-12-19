using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Move.xaml
    /// </summary>
    public partial class Move : Window
    {
        private Repository _repository;
        private User _user;

        public Move(Repository repository, User user, string type)
        {
            InitializeComponent();
            _repository = repository;
            _user = user;
            MoveItems.ItemsSource = _user.Items.Where(x => x.Type == type);
        }

        private void Use_Click(object sender, RoutedEventArgs e)
        {
            if(MoveItems.SelectedItem == null)
            {
                MessageBox.Show("Choose the item, pls", "error");
                return;
            }
            _repository.UseItems(_user, MoveItems.SelectedItem as Item);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
