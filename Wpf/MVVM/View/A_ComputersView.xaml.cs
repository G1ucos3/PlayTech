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
using Wpf.Dialog;
using Wpf.MVVM.ViewModel;
using Button = System.Windows.Controls.Button;
using UserControl = System.Windows.Controls.UserControl;

namespace Wpf.MVVM.View
{
    public class UserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<CurrentComputer> CurrentComputers && CurrentComputers.Count > 0)
            {
                var user = CurrentComputers.FirstOrDefault();
                if (user?.User != null)
                {
                    return user.User.UserName;
                }
            }
            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status)
            {
                if (!status) return "Online";
            }
            return "Offline";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class A_ComputersView : UserControl
    {
        private A_ComputersViewModel A_ComputersViewModel;

        public A_ComputersView()
        {
            InitializeComponent();
            A_ComputersViewModel = new A_ComputersViewModel(new UserService(), new CurrentComputerService(), new ComputerService());
            DataContext = A_ComputersViewModel;
            cboFilter.SelectedValue = 1;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Computer newComputer = new Computer();
            var createComputer = new CreateNewComputer(newComputer);
            if(createComputer.ShowDialog() == true)
            {
                A_ComputersViewModel.createComputer(newComputer);
                cboFilter.SelectedValue = 1;
            }
            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Computer computer)
            {
                var currentComputer = computer;
                var updateComputer = new UpdateComputer(currentComputer);
                if (updateComputer.ShowDialog() == true)
                {
                    A_ComputersViewModel.updateComputer(currentComputer);
                    cboFilter.SelectedValue = 1;
                }
                else
                {
                    A_ComputersViewModel.loadComputer();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Computer computer)
            {
                DialogResult result = MessageBox.Show("Are you sure?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Confirm,
                                                   MessageBox.MessageBoxButton.Cancel);
                if (result == DialogResult.Yes)
                {
                    A_ComputersViewModel.deleteComputer(computer);
                    cboFilter.SelectedValue = 1;
                }
            }
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 1)
            {
                A_ComputersViewModel.loadComputer();
            }
            else if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 2)
            {
                A_ComputersViewModel.loadOnlineComputer();
            }
            else if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 3)
            {
                A_ComputersViewModel.loadOfflineComputer();
            }
        }
    }
}
