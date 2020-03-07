using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using L08_Exercises.model;
using Microsoft.Win32;

namespace L08_Exercises
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "csv files (*.csv)|*.csv",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _users = User.ReadCsvFile(openFileDialog.FileName);
                UserListBox.ItemsSource = _users;
                StatusBarItemLastTimeLoaded.Content = "Last time loaded: " + DateTime.Now.ToShortTimeString();
                StatusBarItemItemsCount.Content = "Number of items in listbox: " + _users.Count;
            }
        }

        private void UserSelected(object sender, SelectionChangedEventArgs e)
        {
            var i = ((ListBox) sender).SelectedIndex;
            DataContext = _users[i];
            
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserListBox.SelectedItem == null)
            {
                return;
            }
            var o = ((TextBox) sender);
            var user = _users[UserListBox.SelectedIndex];
            var oName = o.Name.Replace("TextBox", "");
            try
            {
                switch (oName)
                {
                    case "Id":
                        user.Id = o.Text;
                        break;
                    case "Name":
                        user.Name = o.Text;
                        break;
                    case "Age":
                        user.Age = int.Parse(o.Text);
                        break;
                    case "Score":
                        user.Score = int.Parse(o.Text);
                        break;
                }

                UserListBox.Items.Refresh();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
    
}
