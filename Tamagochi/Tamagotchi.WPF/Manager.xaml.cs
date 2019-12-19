using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
        System.Timers.Timer timer = new System.Timers.Timer(10000);
        public Manager(User user, Repository repository)
        {
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerHandler1);
            timer.Enabled = true;
            InitializeComponent();
            _user = user;
            _repository = repository;
            UpdateResources();
        }

        private void FeedButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Move(_repository, _user, "food");
            window.ShowDialog();
            UpdateResources();
        }

        private void TypeOfItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = TypeOfItems.SelectedItem as Item;
            if(item == null)
            {
                MessageBox.Show("Choose Type of items", "error");
                return;
            }
            ListOfItems.ItemsSource = _repository.Items.Where(x => x.Type == item.Type);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateResources();
        }
        private void UpdateResources()
        {
            Level.Value = _user.Exp;
            Level.Maximum = _user.ExpLevel;
            NameBox.Text = _user.Name;
            LevelB.Text = _user.Level.ToString();
            MoneyBox.Text = _user.Money.ToString();
            TypeOfItems.ItemsSource = _repository.TypeOfItems;
            UserItems.ItemsSource = null;
            UserItems.ItemsSource = _user.Items;
            _repository.SaveData();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if(_repository.BuyTheItem(_user, ListOfItems.SelectedItem as Item) == true)
            {
                UpdateResources();
                MessageBox.Show("thank you purchase completed successfully", "Success");
            }
            else
            {
                MessageBox.Show("Sorry, you are short of funds", "Error");
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Move(_repository, _user, "toys");
            window.ShowDialog();
            UpdateResources();
        }
        private void OnTimerHandler1(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new IdleDelegate(MoneyTime), System.Windows.Threading.DispatcherPriority.Normal, null);
            this.Dispatcher.Invoke(new IdleDelegate(UpdateResources), System.Windows.Threading.DispatcherPriority.Normal, null);
        }
        private delegate void IdleDelegate();
        private void MoneyTime()
        {
            _user.Money += 10;
        }
    }
}
