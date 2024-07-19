﻿using BusinessObjects;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
    internal static class ConsoleAllocator
    {
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport(@"kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SwHide = 0;
        const int SwShow = 5;


        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, SwShow);
            }
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, SwHide);
        }
    }

    public class ComputerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ICollection<CurrentComputer> CurrentComputers && CurrentComputers.Count > 0)
            {
                var computer = CurrentComputers.FirstOrDefault();
                if (computer?.Computer != null)
                {
                    return computer.Computer.ComputerName;
                }
            }
            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int role)
            {
                return role switch
                {
                    1 => "Admin",
                    2 => "Manager",
                    3 => "Gamer",
                    _ => "Unknown"
                };
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class A_UsersView : UserControl
    {
        private A_UsersViewModel A_UsersViewModel;

        public A_UsersView()
        {
            InitializeComponent();
            A_UsersViewModel = new A_UsersViewModel(new UserService(), new CurrentComputerService(), new ComputerService());
            DataContext = A_UsersViewModel;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is User user)
            {
                DialogResult result = MessageBox.Show("Are you sure?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Confirm,
                                                   MessageBox.MessageBoxButton.Cancel);
                if(result == DialogResult.Yes)
                {
                    A_UsersViewModel.deleteUser(user);
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User();
            var cUser = new CreateNewUser(newUser);
            if(cUser.ShowDialog() == true)
            {
                A_UsersViewModel.createUser(newUser);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is User user)
            {
                var currentUser = user;
                var updateUser = new UpdateUser(currentUser);
                if(updateUser.ShowDialog() == true)
                {
                    A_UsersViewModel.updateUser(currentUser);
                }
            }
        }
    }
}
