using BusinessObjects;
using Microsoft.VisualBasic.ApplicationServices;
using Service;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.MVVM.ViewModel;
using Button = System.Windows.Controls.Button;
using UserControl = System.Windows.Controls.UserControl;
using User = BusinessObjects.User;

namespace Wpf.MVVM.View
{
    public class OrderStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status)
            {
                if (!status)
                {
                    return "Unpurchased";
                }
            }
            return "Purchased";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OrderUserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is User user)
            {
                return user.UserName;
            }
            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OrderProductConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Product product)
            {
                return product.ProductName;
            }
            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for M_OrdersView.xaml
    /// </summary>
    public partial class M_OrdersView : UserControl
    {
        private M_OrdersViewModel M_OrdersViewModel;

        public M_OrdersView()
        {
            InitializeComponent();
            M_OrdersViewModel = new M_OrdersViewModel(new UserService(), new OrderService(), new ProductService());
            DataContext = M_OrdersViewModel;
            cboFilter.SelectedValue = 2;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.DataContext is Order order /*&& order.OrderStatus == false*/)
            {
                DialogResult result = MessageBox.Show("Check order?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Confirm,
                                                   MessageBox.MessageBoxButton.Cancel);
                if (result == DialogResult.Yes)
                {
                    order.OrderStatus = !order.OrderStatus;
                    M_OrdersViewModel.updateOrder(order);
                    cboFilter.SelectedValue = 2;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Order order && order.OrderStatus == true)
            {
                DialogResult result = MessageBox.Show("Delete order?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Confirm,
                                                   MessageBox.MessageBoxButton.Cancel);
                if (result == DialogResult.Yes)
                {
                    M_OrdersViewModel.deleteOrder(order);
                    cboFilter.SelectedValue = 2;
                }
            }
            else MessageBox.Show("This order has not been purchased", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Confirm,
                                                   MessageBox.MessageBoxButton.Cancel);
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 2)
            {
                M_OrdersViewModel.loadOrder();
            }
            else if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 1)
            {
                M_OrdersViewModel.loadPurchased();
            }
            else if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 0)
            {
                M_OrdersViewModel.loadUnpurchased();
            }
        }
    }
}
