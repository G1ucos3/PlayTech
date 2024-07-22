﻿using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
using Wpf.MVVM.ViewModel;
using Brushes = System.Windows.Media.Brushes;

namespace Wpf.Dialog
{
    /// <summary>
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : Window, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }
        private AcCommand submitCommand;

        public CreateNewUser(User currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            SubmitCommand = new AcCommand(Submit, CanSubmit); 
            DataContext = this;
            CurrentUser.PropertyChanged += CurrentUser_PropertyChanged;
            CurrentUser.ErrorsChanged += CurrentUser_ErrorsChanged;

            passwordBox.PasswordChanged += (s, e) =>
            {
                var passwordBox = s as PasswordBox;
                CurrentUser.UserPassword = passwordBox.Password;
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CurrentUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }
        private void CurrentUser_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public AcCommand SubmitCommand
        {
            get { return submitCommand; }
            set
            {
                submitCommand = value;
                OnPropertyChanged(nameof(SubmitCommand));
            }
        }

        private bool CanSubmit(object obj)
        {
            return (!string.IsNullOrEmpty(CurrentUser.UserName) && 
                !string.IsNullOrEmpty(CurrentUser.UserPassword) &&
                !string.IsNullOrEmpty(CurrentUser.UserEmail) &&
                CurrentUser.UserRoles > 1 && CurrentUser.UserRoles < 4 &&
                !CurrentUser.HasErrors);
        }

        private void Submit(object obj)
        {
            if (!CurrentUser.HasErrors)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please fix validation", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hiddenPasswordBox.Text = passwordBox.Password;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenEmail.Text = email.Text;
            if (email.Text.IsNullOrEmpty()) bdEmail.Background = Brushes.Red;
            else bdEmail.Background = Brushes.White;
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenUsername.Text = txtUsername.Text;
            if (hiddenUsername.Text.IsNullOrEmpty()) bdUsername.Background = Brushes.Red;
            else bdUsername.Background = Brushes.White;
        }

        private void showPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenPasswordBox.Text = showPasswordBox.Text;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            btnHide.Visibility = Visibility.Visible;
            btnShow.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
            showPasswordBox.Visibility = Visibility.Collapsed;
            passwordBox.Password = showPasswordBox.Text;
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            btnHide.Visibility = Visibility.Collapsed;
            btnShow.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Collapsed;
            showPasswordBox.Visibility = Visibility.Visible;
            showPasswordBox.Text = passwordBox.Password;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
