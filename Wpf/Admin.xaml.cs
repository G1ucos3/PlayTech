using BusinessObjects;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Dialog;

namespace Wpf
{
    public partial class Admin : Window, INotifyPropertyChanged
    {
        private IUserService _userService;

        public Admin()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            btnUsers.Background = new SolidColorBrush(Colors.DimGray);
            btnComputers.Background = new SolidColorBrush(Colors.Transparent);
            btnProducts.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void btnComputers_Click(object sender, RoutedEventArgs e)
        {
            btnUsers.Background = new SolidColorBrush(Colors.Transparent);
            btnComputers.Background = new SolidColorBrush(Colors.DimGray);
            btnProducts.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            btnUsers.Background = new SolidColorBrush(Colors.Transparent);
            btnComputers.Background = new SolidColorBrush(Colors.Transparent);
            btnProducts.Background = new SolidColorBrush(Colors.DimGray);
        }

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult result = MessageBox.Show("Are you sure?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Yes,
                                                    MessageBox.MessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                var login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = _userService.GetUserById(Int32.Parse(txtUserID.Text));
            var editProfile = new EditProfile(currentUser);
            if(editProfile.ShowDialog() == true)
            {
                _userService.UpdateUser(currentUser);
                txtUsername.Text = currentUser.UserName;
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                avatar.ImageSource = new BitmapImage(new Uri(projectDirectory + currentUser.UserAvatar, UriKind.Absolute));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
