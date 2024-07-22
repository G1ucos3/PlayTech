using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Wpf
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window, INotifyPropertyChanged
    {
        private IUserService _userService;
        public Manager()
        {
            InitializeComponent();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            btnUsers.Background = new SolidColorBrush(Colors.DimGray);
            btnOrders.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            btnUsers.Background = new SolidColorBrush(Colors.Transparent);
            btnOrders.Background = new SolidColorBrush(Colors.DimGray);
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

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
