using System;
using System.Collections.Generic;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
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
    }
}
